using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{

  [GeneratedComponent]
  public partial class CUIComponent : CUIVisualComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIComponent>((c) =>
    {
      c.RightResizeHandle.Background.Color = c.Palette.Colors["accent"];
    });

    protected virtual void InitStyle()
    {
      Background.Color = Color.Transparent;
    }


    public class Part : IPart { public CUIComponent Self { get; set; } }

    public static Dictionary<int, WeakReference<CUIComponent>> ComponentsById = new();
    public static IEnumerable<CUIComponent> AllComponents => ComponentsById.Values
      .Select(wr =>
      {
        wr.TryGetTarget(out CUIComponent component);
        return component;
      }).Where(c => c != null);

    private CUIComponentInfo _Info; public CUIComponentInfo Info
    {
      get
      {
        if (_Info is null) _Info = CUI.CUITypes.GetInfo(this.GetType());
        return _Info;
      }
    }

    public CUIComponent() : base()
    {
      SetupLayout();//HACK 
      this.Inject();
    }

    public override string ToString() => $"{this.GetType().Name}:{ID}:{AKA}";
  }
}