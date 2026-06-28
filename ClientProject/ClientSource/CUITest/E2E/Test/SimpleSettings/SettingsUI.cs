using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUIUser
{
  public partial class E2ETestPack
  {
    public partial class SimpleSettings : IE2ETest
    {
      public partial class SettingsUI : CUIDefault.Frame
      {
        public Settings Settings { get; }
        public CUIVerticalList FieldList { get; private set; }

        public Dictionary<Type, Func<string, string, CUIComponent>> FieldCatalog { get; } = new()
        {
          [typeof(string)] = (key, value) => new CUIDefault.TextField() { Key = key, RawValue = value },
          [typeof(int)] = (key, value) => new CUIDefault.IntField() { Key = key, RawValue = value },
        };

        public void Sync()
        {
          FieldList.Clear();

          foreach (PropertyInfo pi in Settings.GetType().GetProperties())
          {
            if (FieldCatalog.ContainsKey(pi.PropertyType))
            {
              FieldList.Add(FieldCatalog[pi.PropertyType](pi.Name, pi.GetValue(Settings).ToString()));
            }
          }
        }

        public SettingsUI(Settings settings) : base()
        {
          Settings = settings;

          Caption.Text = "Some Settings, bruh";

          Commands.ListenFor<string[]>(
            "setvalue",
            (args) => Settings.SetValue(args[0], args[1])
          );

          this["layout"]["header"] = new CUIHorizontalList()
          {
            FitContent = new CUIBool2(false, true),
          };

          this["layout"]["header"]["printSettings"] = new CUIButton("Print Settings")
          {
            OnMouseDown = (c, e) => Settings.Print(),
          };

          this["layout"]["main"] = FieldList = new CUIVerticalList()
          {
            Flex = 1,
          };
        }




      }


    }
  }
}