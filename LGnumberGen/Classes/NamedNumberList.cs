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

namespace LGnumberGen.Classes
{
    [Serializable]
    public class NamedNumberList
    {
        public Guid ListId { get; set; }
        public string ListName { get; set; }
        public int DefPickNo { get; set; }
        public List<NameListItem> namelistitem { get; set; }
        public bool saved { get; set; }
        public List<string> NamedIdList { get; set; }
    }
}