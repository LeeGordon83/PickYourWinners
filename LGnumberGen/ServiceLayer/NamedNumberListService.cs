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
    public class NamedNumberListService
    {
        //method to set up a new named number list on entry
        public static NamedNumberList NewList()
        {
            NamedNumberList newList = new NamedNumberList();

            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

            newList.ListId = Guid.NewGuid();
            newList.saved = false;

            NamednumberListXML.Create();

            NamedNumberListData NamednumberListData = NamednumberListXML.DeSerialize();

            NamednumberListData.NamedNumberLists.RemoveAll(x => x.saved == false);

            NamednumberListData.NamedNumberLists.Add(newList);

            NamednumberListXML.Serialize(NamednumberListData);

            return newList;
        }

        ////method to delete a name

        internal static void DeleteName(string deleteName, string listID)
        {
            if (deleteName != null)
            {
                Guid ListID = Guid.Parse(listID);

                Guid NameID = Guid.Parse(deleteName);

                NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

                NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

                NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();

                NameListItem existingList = exactList.namelistitem.Where(x => x.Id == NameID).FirstOrDefault();

                exactList.namelistitem.Remove(existingList);

                NamednumberListXML.Serialize(NamednumListData);
            }
        }

        //Method to generate a random list of names

        public static List <NameListItem> GenerateNamedNumList(string listDetailsId)
        {
            List<NameListItem> ListGenerated = new List<NameListItem>();

            Guid ListID = Guid.Parse(listDetailsId);

            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

            NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

            NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();

            ListGenerated = exactList.namelistitem.Where(x => x.Active == true).ToList();

            List<NameListItem> SelectedList = new List<NameListItem>();

            Random rnd = new Random();
            int PickNum = exactList.DefPickNo;

            List<int> listNumbers = new List<int>();


            do
            {
                int ranNum = rnd.Next(ListGenerated.Count);
                if (!listNumbers.Contains(ranNum))
                {
                    SelectedList.Add(ListGenerated[ranNum]);
                    listNumbers.Add(ranNum);
                }
            } while (listNumbers.Count < PickNum);

            return SelectedList;
        }

        //Method to delete a list

        internal static void DeleteList(string deleteItem)
        {
            Guid ListG = Guid.Parse(deleteItem);

            NamedNumberListXML namedNumberListXML = new NamedNumberListXML();

            NamedNumberListData namedNumListData = namedNumberListXML.DeSerialize();

            NamedNumberList exactList = namedNumListData.NamedNumberLists.Where(x => x.ListId == ListG).FirstOrDefault();

            namedNumListData.NamedNumberLists.Remove(exactList);

            namedNumberListXML.Serialize(namedNumListData);
        }


        //Method to add a name to list

        public static NamedNumberList listAdd(string nameToAdd, string listId)
        {

            Guid ListID = Guid.Parse(listId);

            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

            NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

            NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();

            List<NameListItem> existingList = new List<NameListItem>();

            NameListItem newListitem = new NameListItem();
            newListitem.Name = nameToAdd;
            newListitem.Active = true;
            exactList.namelistitem.Add(newListitem);

            NamednumberListXML.Serialize(NamednumListData);

            return exactList;

        }


        //Method to get all existing listitems

        public static List<NameListItem> listGen(string listId)
        {

            List<NameListItem> ListGenerated = new List<NameListItem>();

            Guid ListID = Guid.Parse(listId);

            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

            NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

            NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();

            ListGenerated = exactList.namelistitem;

            return ListGenerated;
        }

        //Method to Save Named Number List

        public static void SaveNum(string existingID, string saveName)
        {
            NamedNumberList numlist = new NamedNumberList();

            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

            Guid ListID = Guid.Parse(existingID);

            NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

            NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();

            exactList.saved = true;
            exactList.ListName = saveName;

            NamednumberListXML.Serialize(NamednumListData);
        }

        //Get all Saved Named Number Lists


        public static List<NamedNumberList> GetAllSaved()
        {

            NamedNumberListXML namedNumberListXML = new NamedNumberListXML();

            NamedNumberListData namedNumListData = namedNumberListXML.DeSerialize();

            List<NamedNumberList> NamedNumberLists = namedNumListData.NamedNumberLists.Where(x => x.saved == true).ToList();

            return NamedNumberLists;

        }


        //Method to Load a Named Number List

        public static NamedNumberList LoadNamedList(string ListID)
        {

            Guid ListG = Guid.Parse(ListID);

            NamedNumberListXML namedNumberListXML = new NamedNumberListXML();

            NamedNumberListData namedNumListData = namedNumberListXML.DeSerialize();

            NamedNumberList exactList = namedNumListData.NamedNumberLists.Where(x => x.ListId == ListG).FirstOrDefault();

            return exactList;

        }

        //Method to Save number of picks

        internal static void PickNumbers(int numtoGenint, string listId, List<string> idList)
        {
            Guid ListID = Guid.Parse(listId);

            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

            NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

            NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();


            exactList.DefPickNo = numtoGenint;
            exactList.NamedIdList = idList;

            NamednumberListXML.Serialize(NamednumListData);

        }

        //Check if list is already saved

        internal static bool ExistsCheck(string listId)
        {
            bool existsCheck = false;

            Guid ListID = Guid.Parse(listId);

            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

            NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

            NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();

            if( exactList.saved == true)
            {
                existsCheck = true;
            }
            return existsCheck;
        }

        //Method to update whether or not item is checked

        public static void UpdateActive (string nameId, string listID, bool isActive)
        {
            if (nameId != null)
            {
                Guid ListID = Guid.Parse(listID);

                Guid NameID = Guid.Parse(nameId);

                NamedNumberListXML NamednumberListXML = new NamedNumberListXML();

                NamedNumberListData NamednumListData = NamednumberListXML.DeSerialize();

                NamedNumberList exactList = NamednumListData.NamedNumberLists.Where(x => x.ListId == ListID).FirstOrDefault();

                NameListItem existingListItem = exactList.namelistitem.Where(x => x.Id == NameID).FirstOrDefault();

                existingListItem.Active = isActive;

                NamednumberListXML.Serialize(NamednumListData);
            }
        }


    }
}