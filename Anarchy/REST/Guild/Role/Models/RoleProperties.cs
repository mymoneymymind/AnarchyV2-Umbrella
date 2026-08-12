using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Options for creating/modifying a role
    /// </summary>
    public class RoleProperties
    {
        private readonly DiscordParameter<string> NameProperty = new DiscordParameter<string>();
        [JsonProperty("name")]
        public string Name
        {
            get { return NameProperty; }
            set { NameProperty.Value = value; }
        }

        public bool ShouldSerializeName()
        {
            return NameProperty.Set;
        }

        private readonly DiscordParameter<DiscordPermission> PermissionsProperty = new DiscordParameter<DiscordPermission>();
        [JsonProperty("permissions")]
        public DiscordPermission Permissions
        {
            get { return PermissionsProperty; }
            set { PermissionsProperty.Value = value; }
        }

        public bool ShouldSerialize_permissions()
        {
            return PermissionsProperty.Set;
        }

        private readonly DiscordParameter<uint> ColorProperty = new DiscordParameter<uint>();
        [JsonProperty("color")]
#pragma warning disable IDE1006, IDE0051
        private uint _color
        {
            get { return ColorProperty; }
            set { ColorProperty.Value = value; }
        }
#pragma warning restore IDE1006, IDE0051
        public Color Color
        {
            get { return Color.FromArgb((int) _color); }
            set { _color = (uint) Color.FromArgb(0, value.R, value.G, value.B).ToArgb(); }
        }

        public bool ShouldSerialize_color()
        {
            return ColorProperty.Set;
        }

        private readonly DiscordParameter<List<uint>> ColorsProperty = new DiscordParameter<List<uint>>();
        [JsonProperty("colors")]
        private List<uint> _colors
        {
            get { return ColorsProperty; }
            set { ColorsProperty.Value = value; }
        }
        public List<Color> Colors
        {
            get
            {
                if (_colors == null)
                    return null;
                return _colors.ConvertAll(c => Color.FromArgb((int) c));
            }
            set
            {
                if (value == null)
                    _colors = null;
                else
                    _colors = value.ConvertAll(c => (uint) Color.FromArgb(0, c.R, c.G, c.B).ToArgb());
            }
        }

        public bool ShouldSerialize_colors()
        {
            return ColorsProperty.Set;
        }

        private readonly DiscordParameter<bool> SeperatedProperty = new DiscordParameter<bool>();
        [JsonProperty("hoist")]
        public bool Seperated
        {
            get { return SeperatedProperty; }
            set { SeperatedProperty.Value = value; }
        }

        public bool ShouldSerializeSeperated()
        {
            return SeperatedProperty.Set;
        }

        private readonly DiscordParameter<bool> MentionableProperty = new DiscordParameter<bool>();
        [JsonProperty("mentionable")]
        public bool Mentionable
        {
            get { return MentionableProperty; }
            set { MentionableProperty.Value = value; }
        }

        public bool ShouldSerializeMentionable()
        {
            return MentionableProperty.Set;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}