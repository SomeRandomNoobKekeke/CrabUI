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
    public class SimpleSettings : IE2ETest
    {

      public class Settings
      {
        public string Name { get; set; } = "bruh";
      }

      public CUIFrame SettingsUI { get; set; }
      public CUIVerticalList FieldList { get; set; }

      public Settings ModSettings { get; set; } = new();

      public void CreateUI()
      {
        SettingsUI = new CUIFrame()
        {
          Absolute = new CUINullRect(w: 300, h: 400),
          BackgroundColor = Color.Brown,
          Anchor = CUIAnchor.Center,
        };

        SettingsUI["layout"] = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
        };

        SettingsUI["layout"]["header"] = new CUIHorizontalList()
        {
          Direction = CUIDirection.Reverse,
          BackgroundColor = new Color(32, 32, 32),
          Absolute = new CUINullRect(h: 30),
        };

        SettingsUI["layout"]["header"]["close"] = new CUIButton()
        {
          Text = "X",
          MasterColor = Color.Red,
          Absolute = new CUINullRect(w: 30, h: 30),
        };

        SettingsUI["layout"]["main"] = FieldList = new CUIVerticalList()
        {
          Flex = 1,
          BackgroundColor = Color.Green,
        };
      }

      public void OpenSettings()
      {
        SettingsUI.Open();
        FieldList.Clear();

        foreach (PropertyInfo pi in ModSettings.GetType().GetProperties())
        {
          FieldList.Add(new CUITextBlock(pi.Name)
          {
            Absolute = new CUINullRect(h: 30),
          });
        }
      }

      public void Initialize()
      {
        CreateUI();
        OpenSettings();
      }

      public void Dispose()
      {
        SettingsUI.Close();
      }
    }
  }
}