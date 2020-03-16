using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LGnumberGen.Classes;
using LGnumberGen.Repository;

namespace LGnumberGen.ServiceLayer
{
    public class SettingsService
    {
        public static void saveColour(string colour)
        {
            SettingsMain settings = new SettingsMain();

            SettingsXML settingsXML = new SettingsXML();

            settings.settingsId = Guid.NewGuid();
            settings.backgroundColour = colour;

            SettingsData settingsData = settingsXML.DeSerialize();

            settingsData.SettingsList.RemoveAll(x => x.settingsId != null);

            settingsData.SettingsList.Add(settings);

            settingsXML.Serialize(settingsData);
        }

        public static string loadColour()
        {
            SettingsXML settingsXML = new SettingsXML();

            SettingsData settingsData = settingsXML.DeSerialize();

            SettingsMain exactSettings = settingsData.SettingsList.Where(x => x.settingsId != null).FirstOrDefault();

            string SettingsString = "";

            if (exactSettings != null)
            {
                SettingsString = exactSettings.backgroundColour;
            }

            return SettingsString;
        }
    }
}