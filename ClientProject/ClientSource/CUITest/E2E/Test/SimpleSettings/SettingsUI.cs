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

        public Dictionary<Type, Func<PropertyInfo, CUIComponent>> FieldCatalog { get; } = new()
        {
          [typeof(string)] = (pi) => new TextField(pi),
        };

        public void Sync()
        {
          FieldList.Clear();

          foreach (PropertyInfo pi in Settings.GetType().GetProperties())
          {
            if (FieldCatalog.ContainsKey(pi.PropertyType))
            {
              FieldList.Add(FieldCatalog[pi.PropertyType](pi));
            }
          }
        }


        public void CreateUI()
        {
          Absolute = new CUINullRect(w: 300, h: 400);
          BackgroundColor = Color.Brown;
          Anchor = CUIAnchor.Center;

          this["layout"] = new CUIVerticalList()
          {
            Relative = new CUINullRect(0, 0, 1, 1),
          };

          this["layout"]["header"] = new CUIHorizontalList()
          {
            Direction = CUIDirection.Reverse,
            BackgroundColor = new Color(32, 32, 32),
            Absolute = new CUINullRect(h: 30),
          };

          this["layout"]["header"]["close"] = new CUIButton()
          {
            Text = "X",
            MasterColor = Color.Red,
            Absolute = new CUINullRect(w: 30, h: 30),
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