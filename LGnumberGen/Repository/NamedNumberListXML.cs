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
using System.IO;
using System.Xml.Serialization;

namespace LGnumberGen.Repository
{
    public class NamedNumberListXML
    {

        internal static string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        internal string fileName = Path.Combine(path, "NamedNumberList.xml");

        internal void Create()
        {
            if (!File.Exists(fileName))
            {
                using (TextWriter writer = new StreamWriter(fileName))
                {
                    string content = "<?xml version='1.0' encoding='UTF-8'?><NamedNumberListData><NumberLists></NumberLists></NamedNumberListData>";

                    writer.WriteLine(content);
                }
            }
        }

        internal void Serialize(NamedNumberListData NamednumberlistData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NamedNumberListData));

            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, NamednumberlistData);
            }
        }

        internal NamedNumberListData DeSerialize()
        {
            NamedNumberListData Namednumberlists = new NamedNumberListData();

            XmlSerializer serializer = new XmlSerializer(typeof(NamedNumberListData));

            using (TextReader reader = new StreamReader(fileName))
            {
                Namednumberlists = (NamedNumberListData)serializer.Deserialize(reader);
            }
            return Namednumberlists;
        }

    }
}
    
