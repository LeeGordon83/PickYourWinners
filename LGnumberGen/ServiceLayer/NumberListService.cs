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
using System.Xml.Serialization;
using System.IO;
using LGnumberGen.Repository;

namespace LGnumberGen.ServiceLayer
{
    public class NumberListService
    {

       //Method to Generate Random Number

        public static string numGen(int minNum, int MaxNum, int pickNum)
              
        {
            
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            var ranNumStr = new StringBuilder();
            ranNumStr.Append("");
            MaxNum = MaxNum + 1;

            if (pickNum > 1)
            {
                
                List<int> listNumbers = new List<int>();


                do
                {
                    int ranNum = rnd.Next(minNum, MaxNum);
                    if (!listNumbers.Contains(ranNum))
                    {
                        listNumbers.Add(ranNum);
                        ranNumStr.Append(ranNum + "\n");
                    }
                } while (listNumbers.Count < pickNum);

            }
            else
            {
                int ranNum = rnd.Next(minNum, MaxNum);
                ranNumStr.Append(ranNum.ToString());
            }
                return ranNumStr.ToString();
        }

        //Method to Save Number List

        public static Guid SaveNum(string saveName, string genNum, int maxNum, int minNum, int pickNum)
        {
            NumberList numlist = new NumberList();

            NumberListXML numberListXML = new NumberListXML();

            numlist.Id = Guid.NewGuid();
            numlist.ListName = saveName;
            numlist.MinNumber = minNum;
            numlist.MaxNumber = maxNum;
            numlist.DefPickNo = pickNum;
            numlist.GenNum = genNum;
            numlist.Saved = true;

            NumberListData numberListData = numberListXML.DeSerialize();

            numberListData.NumberLists.Add(numlist);

            numberListXML.Serialize(numberListData);

            return numlist.Id;
            
        }

        //Method to save updates to existing List

        public static void SaveExisting(string existingID, string genNum, int maxNum, int minNum, int pickNum)
        {
            NumberList numlist = new NumberList();

            NumberListXML numberListXML = new NumberListXML();

            Guid ListID = Guid.Parse(existingID);

            NumberListData numListData = numberListXML.DeSerialize();

            NumberList exactList = numListData.NumberLists.Where(x => x.Id == ListID).FirstOrDefault();

            exactList.GenNum = genNum;
            exactList.MaxNumber = maxNum;
            exactList.MinNumber = minNum;
            exactList.DefPickNo = pickNum;

            numberListXML.Serialize(numListData);
        }

        //Method to get All Saved Number Lists

        public static List<NumberList> GetAllSaved()
        {
           
            NumberListXML numberListXML = new NumberListXML();

            NumberListData numListData = numberListXML.DeSerialize();

            List<NumberList> numberLists = numListData.NumberLists;

            return numberLists;
                     
        }

        //Method to Load a Number List

        public static NumberList LoadList(string ListID)
        {

            Guid ListG = Guid.Parse(ListID);

            NumberListXML numberListXML = new NumberListXML();

            NumberListData numListData = numberListXML.DeSerialize();

            NumberList exactList = numListData.NumberLists.Where(x => x.Id == ListG).FirstOrDefault();

            return exactList;

        }

        //Method to Delete a Number List

        public static void DeleteList(string ListID)
        {
            Guid ListG = Guid.Parse(ListID);

            NumberListXML numberListXML = new NumberListXML();

            NumberListData numListData = numberListXML.DeSerialize();

            NumberList exactList = numListData.NumberLists.Where(x => x.Id == ListG).FirstOrDefault();

            numListData.NumberLists.Remove(exactList);

            numberListXML.Serialize(numListData);


        }

        //Check if list is already saved

        internal static bool ExistsCheck(string listId)
        {
            bool existsCheck = false;

            Guid ListID = Guid.Parse(listId);

            NumberListXML numberListXML = new NumberListXML();

            NumberListData numListData = numberListXML.DeSerialize();

            NumberList exactList = numListData.NumberLists.Where(x => x.Id == ListID).FirstOrDefault();

            if (exactList.Saved == true)
            {
                existsCheck = true;
            }
            return existsCheck;
        }

    }
}