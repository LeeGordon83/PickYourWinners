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
    public class NumberListXML
    {
        internal static string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        internal string fileName = Path.Combine(path, "NumberList.xml");

        internal void Create()
        {
            if(!File.Exists(fileName))
            {
                using (TextWriter writer = new StreamWriter(fileName))
                {
                    string content = "<?xml version='1.0' encoding='UTF-8'?><NumberListData><NumberLists></NumberLists></NumberListData>";

                    writer.WriteLine(content);
                }
            }
        }

        internal void Serialize(NumberListData numberlistData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NumberListData));

            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, numberlistData);
            }
        }

        internal NumberListData DeSerialize()
        {            
            NumberListData numberlists = new NumberListData();

            XmlSerializer serializer = new XmlSerializer(typeof(NumberListData));

            using (TextReader reader = new StreamReader(fileName))
            {
                numberlists = (NumberListData)serializer.Deserialize(reader);
            }
            return numberlists;
        }

    }
}