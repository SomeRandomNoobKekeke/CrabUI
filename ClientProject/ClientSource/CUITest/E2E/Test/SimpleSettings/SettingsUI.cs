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
      public partial class SettingsUI : CUIFrame
      {
        public Settings Settings { get; }

        public SettingsUI(Settings settings) : base()
        {
          Settings = settings;
          CreateUI();
        }

        public CUIVerticalList FieldList { get; private set; }

        public Dictionary<Type, Func<string, string, CUIComponent>> FieldCatalog { get; } = new()
        {
          [typeof(string)] = (key, value) => new TextField(key, value),
          [typeof(int)] = (key, value) => new IntField(key, value),
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


        public void CreateUI()
        {
          Absolute = new CUINullRect(w: 300, h: 400);
          BackgroundColor = Color.Brown;

          Commands.ListenFor("setvalue", (o) =>
          {
            if (o is not string[] args) return;
            Settings.SetValue(args[0], args[1]);
          });

          this["layout"] = new CUIVerticalList()
          {
            Relative = new CUINullRect(0, 0, 1, 1),
          };

          this["layout"]["handle"] = new CUIHorizontalList()
          {
            Direction = CUIDirection.Reverse,
            BackgroundColor = new Color(32, 32, 32),
            FitContent = new CUIBool2(false, true),
          };

          this["layout"]["handle"]["close"] = new CUICloseButton()
          {
            Absolute = new CUINullRect(w: 30, h: 30),
          };

          this["layout"]["header"] = new CUIHorizontalList()
          {
            BackgroundColor = Color.Blue,
            FitContent = new CUIBool2(false, true),
          };

          this["layout"]["header"]["printSettings"] = new CUIButton("Print Settings")
          {
            MasterColor = Color.Yellow,
            OnMouseDown = (c, e) => Settings.Print(),
          };

          this["layout"]["main"] = FieldList = new CUIVerticalList()
          {
            Flex = 1,
            BackgroundColor = Color.Green,
          };
        }

      }


    }
  }
}