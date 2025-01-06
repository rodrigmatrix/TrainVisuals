using System.Collections.Generic;
using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI.Widgets;

namespace TrainVisuals;

[FileLocation("ModsSettings/" + nameof(TrainVisuals) + "/" + nameof(TrainVisuals))]
    [SettingsUIGroupOrder(MAIN_GROUP)]
    [SettingsUIShowGroupName(MAIN_GROUP)]
    public class Settings : ModSetting
    {
        public const string MAIN_GROUP = "Main";

        public const string DESTINATION_TYPE_DROPDOWN = "DESTINATION_TYPE_DROPDOWN";

        public Settings(IMod mod) : base(mod)
        {

        }

        [SettingsUISection(MAIN_GROUP, DESTINATION_TYPE_DROPDOWN)]
        public DestinationType DestinationTypeDropdown { get; set; } = DestinationType.FinalDestination;

        public override void SetDefaults()
        {
            
        }

        public enum DestinationType
        {
            FinalDestination,
            NextStation,
            LineName,
        }
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleEN(Settings setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Train Visuals" },
                { m_Setting.GetOptionTabLocaleID(Settings.MAIN_GROUP), "Settings" },
                
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DestinationTypeDropdown)), "Destination Type" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DestinationTypeDropdown)), $"Choose what should be displayed as a destination in front of trains" },

                { m_Setting.GetEnumValueLocaleID(Settings.DestinationType.FinalDestination), "Final Destination" },
                { m_Setting.GetEnumValueLocaleID(Settings.DestinationType.NextStation), "Next Station" },
                { m_Setting.GetEnumValueLocaleID(Settings.DestinationType.LineName), "Line Name" },

            };
        }

        public void Unload()
        {

        }
    }