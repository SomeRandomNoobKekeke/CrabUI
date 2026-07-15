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
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUITextInput
  {
    public bool Valid { get; private set; } = true;

    public Func<string, bool> ValidationFunc { get; set; } = (text) => true;

    public void Validate(string text)
    {
      Valid = ValidationFunc?.Invoke(text) ?? false;
    }
  }
}