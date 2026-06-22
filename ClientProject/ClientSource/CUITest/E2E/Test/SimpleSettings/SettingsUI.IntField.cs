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
        //TODO it feels stupid to create new class just for ValidationFunc
        //TODO probably should create CUIPrefabs with commonly used elements
        public class IntField : CUIHorizontalList
        {
          public IntField(string key, string value)
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
              Flex = 1,
              Text = InitialValue,
              FocusedColor = new Color(0, 255, 255, 128),
              BluredColor = new Color(0, 32, 0),
              ValidationFunc = (s) => int.TryParse(s, out int _),
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