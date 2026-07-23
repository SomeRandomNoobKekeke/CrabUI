using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
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
        public MicroSettingsManager Manager { get; }
        public Settings Settings => Manager.Settings;

        public CUIVerticalList FieldList { get; private set; }


        public void Refresh()
        {
          FieldList.Clear();

          FieldList.Add(new CUIDefault.TextField()
          {
            Label = { AbsoluteMin = new CUINullRect(w: 100) },
            Key = "String Prop",
            Value = Settings.StringProp,
          });

          FieldList.Add(new CUIDefault.IntField()
          {
            Label = { AbsoluteMin = new CUINullRect(w: 100) },
            Key = "Int Prop",
            Value = Settings.IntProp,
          });

          CUIVerticalList NestedWrapper = new CUIVerticalList()
          {
            FitContent = new CUIBool2(false, true),
            Padding = new CUISizes(0, 0, 0, 30),
          };

          NestedWrapper.Add(new CUIDefault.TextField()
          {
            Label = { AbsoluteMin = new CUINullRect(w: 150) },
            Key = "Nested String Prop",
            Value = Settings.Nested.StringProp,
          });


          NestedWrapper.Add(new CUIDefault.IntField()
          {
            Label = { AbsoluteMin = new CUINullRect(w: 150) },
            Key = "Nested Int Prop",
            Value = Settings.Nested.IntProp,
          });

          FieldList.Add(NestedWrapper);
        }

        public SettingsUI(MicroSettingsManager manager) : base()
        {
          Manager = manager;

          OnOpen += (self) => Refresh();

          Caption.Text = "Some Settings, bruh";

          Commands.ListenFor<string[]>(
            "setvalue",
            (args) => Manager.SetValue(args[0], args[1])
          );

          this["layout"]["header"] = new CUIHorizontalList()
          {
            FitContent = new CUIBool2(false, true),
          };

          this["layout"]["header"]["printSettings"] = new CUIButton("Print Settings")
          {
            OnMouseDown = (c, e) => Manager.Print(),
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