using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

namespace CrabUI
{
  public partial class CUICore
  {
    private InputProviderSlot InputProviderSlot { get; set; } = new();

    public CUIMainComponent Main { get; }
    public CUIInput Input { get; }
    public InputSettings InputSettings { get; } = new();
    public LifeCyclePart LifeCycle { get; }

    public IInputProvider InputProvider
    {
      get => InputProviderSlot.InputProvider;
      set => InputProviderSlot.InputProvider = value;
    }



    public CUICore()
    {
      Main = new CUIMainComponent(this);
      LifeCycle = new LifeCyclePart(this);
      Input = new CUIInput(InputProviderSlot, InputSettings);
    }
  }
}