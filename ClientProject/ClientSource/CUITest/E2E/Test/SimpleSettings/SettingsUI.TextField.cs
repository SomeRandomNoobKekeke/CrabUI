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
      public partial class SettingsUI
      {
        public class TextField : CUIHorizontalList
        {
          public TextField(string key, string value)
          {
            Key = key;
            InitialValue = value;
            CreateUI();
          }

          public string Key { get; }
          public string InitialValue { get; }

          public void CreateUI()
          {
            FitContent = new CUIBool2(false, true);

            this["label"] = new CUITextBlock(Key)
            {
              Absolute = new CUINullRect(w: 50),
            };

            this["input"] = new CUITextInput()
            {
              Flex = new CUINullVector2(1, 1),
              Text = InitialValue,
              FocusedColor = new Color(0, 255, 255, 128),
              BluredColor = new Color(0, 32, 0),

              AddOnValidInput = (value) => this["input"].Commands.SendUp("setvalue", new string[]
              {
                Key,value,
              })
            };
          }
        }

      }
    }
  }
}