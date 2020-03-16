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
using System.IO;
using LGnumberGen.Classes;
using System.Xml.Serialization;

namespace LGnumberGen.Repository
{
    public class SettingsXML
    {
        internal static string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        internal string fileName = Path.Combine(path, "Settings.xml");

        internal void Create()
        {
            if (!File.Exists(fileName))
            {
                using (TextWriter writer = new StreamWriter(fileName))
                {
                    string content = "<?xml version='1.0' encoding='UTF-8'?><SettingsData></SettingsData>";

                    writer.WriteLine(content);
                }
            }
        }

        internal void Serialize(SettingsData settingsData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SettingsData));

            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, settingsData);
            }
        }

        internal SettingsData DeSerialize()
        {
            SettingsData settingsData = new SettingsData();

            XmlSerializer serializer = new XmlSerializer(typeof(SettingsData));

            using (TextReader reader = new StreamReader(fileName))
            {
                settingsData = (SettingsData)serializer.Deserialize(reader);
            }
            return settingsData;
        }

    }
}
