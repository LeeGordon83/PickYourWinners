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
    public class NamedNumberListData
    {
        public List<NamedNumberList> NamedNumberLists { get; set; }
    }
}