using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent : IFocusable
  {
    protected FocusStuffTemp_Part focusStuffTemp { get; } = new();
    //TODO another stupid init part, i need init methods now
    public class FocusStuffTemp_Part : IPart
    {
      public void Init()
      {

      }
    }


    public bool Focused { get; set; }
    public bool Focusable { get; set; }




    //TODO should these take this CUIComponent as first arg?
    public Action AddOnFocus { set { OnFocus += value; } }
    public event Action OnFocus
    {
      add => FocusHandle.OnFocus.Add(value);
      remove => FocusHandle.OnFocus.Remove(value);
    }

    public Action AddOnFocusLost { set { OnFocusLost += value; } }
    public event Action OnFocusLost
    {
      add => FocusHandle.OnFocusLost.Add(value);
      remove => FocusHandle.OnFocusLost.Remove(value);
    }
  }
}