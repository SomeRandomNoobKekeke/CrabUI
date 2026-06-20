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
          public TextField(PropertyInfo pi)
          {
            Property = pi;
            CreateUI();
          }

          public PropertyInfo Property { get; }

          public void CreateUI()
          {
            FitContent = new CUIBool2(false, true);
            Background.Color = Color.Red;

            this["label"] = new CUITextBlock(Property.Name);
            this["input"] = new CUITextInput()
            {
              Flex = 1,
              Text = "123",
            };
          }
        }

      }
    }
  }
}