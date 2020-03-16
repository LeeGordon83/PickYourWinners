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
    public class SettingsMain
    {
        public Guid settingsId { get; set; }
        public string backgroundColour { get; set; }
    }
}