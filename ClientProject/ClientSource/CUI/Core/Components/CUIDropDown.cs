using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CrabUI
{

  public partial class CUIDropDown : CUIComponent, IComponent
  {
    public CUIButton Selected { get; }
    public CUIComponent OptionBox { get; }



    public bool Open
    {
      get => OptionBox.Displayed;
      set => OptionBox.Displayed = value;
    }

    public CUIDropDown()
    {
      FitContent = new CUIBool2(true, true);

      this["pin"] = new CUIComponent()
      {
        Anchor = CUIAnchor.CenterBottom,
        Relative = new CUINullRect(w: 1),
      };

      this["pin"]["optionbox"] = OptionBox = new CUIVerticalList()
      {
        FitContent = new(true, true),
        Anchor = CUIAnchor.CenterTop,
      };

      this["textbox"] = Selected = new CUIButton("bruh");
      Selected.MouseDown += (c, e) => Open = !Open;




      OptionBox["option1"] = new CUITextBlock("123");
      OptionBox["option2"] = new CUITextBlock("123");
      OptionBox["option3"] = new CUITextBlock("123");
    }
  }
}