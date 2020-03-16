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
    public class NumberList
    {
        public Guid Id { get; set; }
        public string ListName { get; set; }
        public int DefPickNo { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public string GenNum { get; set; }
        public bool Saved { get; set; }

    }
}