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
    public class NameListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
        public int Number { get; set;  }

        public NameListItem()
        {
            Id = Guid.NewGuid();
            Name = "";
            Active = true;
        }
    }


}