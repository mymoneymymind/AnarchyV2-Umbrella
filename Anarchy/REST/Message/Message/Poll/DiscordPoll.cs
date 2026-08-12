using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// A message poll (API vX). Present on a message via <see cref="DiscordMessage.Poll"/>.
    /// </summary>
    public class DiscordPoll : Controllable
    {
        [JsonProperty("question")]
        public PollMedia Question { get; private set; }

        [JsonProperty("answers")]
        public DiscordPollAnswer[] Answers { get; private set; }

        [JsonProperty("expiry")]
        public DateTime? Expiry { get; private set; }

        [JsonProperty("allow_multiselect")]
        public bool AllowMultiselect { get; private set; }

        [JsonProperty("layout_type")]
        public PollLayoutType LayoutType { get; private set; }

        [JsonProperty("results")]
        public PollResults Results { get; private set; }
    }

    public enum PollLayoutType
    {
        Default = 1
    }

    /// <summary>
    /// Media (text/emoji) for a poll question or answer.
    /// </summary>
    public class PollMedia
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("emoji")]
        public DiscordEmoji Emoji { get; set; }
    }

    /// <summary>
    /// A single answer option in a poll.
    /// </summary>
    public class DiscordPollAnswer
    {
        [JsonProperty("answer_id")]
        public uint AnswerId { get; private set; }

        [JsonProperty("poll_media")]
        public PollMedia Media { get; private set; }
    }

    /// <summary>
    /// Poll results, including per-answer vote counts and the current user's votes.
    /// </summary>
    public class PollResults
    {
        [JsonProperty("is_finalized")]
        public bool IsFinalized { get; private set; }

        [JsonProperty("answer_counts")]
        public PollAnswerCount[] AnswerCounts { get; private set; }

        [JsonProperty("finalized_at")]
        public DateTime? FinalizedAt { get; private set; }
    }

    public class PollAnswerCount
    {
        [JsonProperty("id")]
        public uint Id { get; private set; }

        [JsonProperty("count")]
        public uint Count { get; private set; }

        [JsonProperty("me_voted")]
        public bool MeVoted { get; private set; }
    }

    /// <summary>
    /// Payload for creating a poll on a message (POST /channels/{channel.id}/messages).
    /// Used as the `poll` field of a message send.
    /// </summary>
    public class PollProperties
    {
        [JsonProperty("question")]
        public PollMedia Question { get; set; }

        [JsonProperty("answers")]
        public List<PollMedia> Answers { get; set; }

        [JsonProperty("duration")]
        public uint? Duration { get; set; }

        [JsonProperty("allow_multiselect")]
        public bool AllowMultiselect { get; set; }

        [JsonProperty("layout_type")]
        public PollLayoutType LayoutType { get; set; } = PollLayoutType.Default;
    }
}
