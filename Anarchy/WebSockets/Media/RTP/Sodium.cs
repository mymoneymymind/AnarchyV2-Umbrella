using System.Runtime.InteropServices;

namespace Discord.Media
{
    /// <summary>
    /// Voice/media encryption helpers.
    ///
    /// Discord deprecated xsalsa20_poly1305* and aead_aes256_gcm (Nov 2024) and now
    /// requires aead_xchacha20_poly1305_rtpsize, which is AEAD XChaCha20-Poly1305 using
    /// the 12-byte RTP header as the nonce.
    ///
    /// NOTE: This path is compile-verified but NOT runtime-verified against a live voice
    /// session in this environment. The libsodium build must expose the
    /// crypto_aead_xchacha20poly1305_ietf_* symbols for the new mode to work at runtime.
    /// </summary>
    internal static unsafe class Sodium
    {
        // The mode we advertise/select. Update this to negotiate a different mode.
        public static string EncryptionMode = "aead_xchacha20_poly1305_rtpsize";

        // Poly1305 tag length (shared by both secretbox and aead modes)
        public static int LengthDifference = 16;

        #region legacy xsalsa20_poly1305 (kept for reference / fallback)
        [DllImport("libsodium", EntryPoint = "crypto_secretbox_easy", CallingConvention = CallingConvention.Cdecl)]
        private static extern int SecretBoxEasy(byte* output, byte* input, long inputLength, byte[] nonce, byte[] secret);

        [DllImport("libsodium", EntryPoint = "crypto_secretbox_open_easy", CallingConvention = CallingConvention.Cdecl)]
        private static extern int SecretBoxOpenEasy(byte* output, byte* input, long inputLength, byte[] nonce, byte[] secret);
        #endregion

        #region aead xchacha20 poly1305 (required mode)
        [DllImport("libsodium", EntryPoint = "crypto_aead_xchacha20poly1305_ietf_encrypt", CallingConvention = CallingConvention.Cdecl)]
        private static extern int AeadEncrypt(byte* output, out ulong outputLength, byte* input, ulong inputLength, byte[] ad, ulong adLength, byte[] nsec, byte[] nonce, byte[] secret);

        [DllImport("libsodium", EntryPoint = "crypto_aead_xchacha20poly1305_ietf_decrypt", CallingConvention = CallingConvention.Cdecl)]
        private static extern int AeadDecrypt(byte* output, out ulong outputLength, byte[] nsec, byte* input, ulong inputLength, byte[] ad, ulong adLength, byte[] nonce, byte[] secret);
        #endregion

        private const int NonceLength = 12; // RTP header length for rtpsize mode

        public static bool IsAeadMode => EncryptionMode.StartsWith("aead_");

        public static int Encrypt(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, byte[] nonce, byte[] secret)
        {
            if (IsAeadMode)
                return AeadEncrypt(input, inputOffset, inputLength, output, outputOffset, nonce, secret);
            else
                return LegacyEncrypt(input, inputOffset, inputLength, output, outputOffset, nonce, secret);
        }

        public static int Decrypt(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, byte[] nonce, byte[] secret)
        {
            if (IsAeadMode)
                return AeadDecrypt(input, inputOffset, inputLength, output, outputOffset, nonce, secret);
            else
                return LegacyDecrypt(input, inputOffset, inputLength, output, outputOffset, nonce, secret);
        }

        #region AEAD (aead_xchacha20_poly1305_rtpsize)
        private static int AeadEncrypt(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, byte[] nonce, byte[] secret)
        {
            fixed (byte* inPtr = input)
            fixed (byte* outPtr = output)
            {
                int status = AeadEncrypt(outPtr + outputOffset, out ulong outLen,
                    inPtr + inputOffset, (ulong) inputLength,
                    null, 0, null, nonce, secret);
                if (status != 0)
                    throw new SodiumException();
                return (int) outLen;
            }
        }

        private static int AeadDecrypt(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, byte[] nonce, byte[] secret)
        {
            fixed (byte* inPtr = input)
            fixed (byte* outPtr = output)
            {
                int status = AeadDecrypt(outPtr + outputOffset, out ulong outLen,
                    null, inPtr + inputOffset, (ulong) inputLength,
                    null, 0, nonce, secret);
                if (status != 0)
                    throw new SodiumException();
                return (int) outLen;
            }
        }
        #endregion

        #region Legacy (xsalsa20_poly1305) — retained for fallback only
        private static int LegacyEncrypt(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, byte[] nonce, byte[] secret)
        {
            fixed (byte* inPtr = input)
            fixed (byte* outPtr = output)
            {
                int status = SecretBoxEasy(outPtr + outputOffset, inPtr + inputOffset, inputLength, nonce, secret);
                if (status != 0)
                    throw new SodiumException();
                return inputLength + LengthDifference;
            }
        }

        private static int LegacyDecrypt(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, byte[] nonce, byte[] secret)
        {
            fixed (byte* inPtr = input)
            fixed (byte* outPtr = output)
            {
                int status = SecretBoxOpenEasy(outPtr + outputOffset, inPtr + inputOffset, inputLength, nonce, secret);
                if (status != 0)
                    throw new SodiumException();
                return inputLength - LengthDifference;
            }
        }
        #endregion
    }
}
