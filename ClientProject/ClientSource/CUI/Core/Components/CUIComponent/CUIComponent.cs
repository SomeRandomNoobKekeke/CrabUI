using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{

  [GeneratedComponent]
  public partial class CUIComponent : CUIVisualComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIComponent>((c) =>
    {
      c.RightResizeHandle.Background.Color = c.Palette.Colors["outercontrols"];
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


    public CUIComponentInfo Info { get; }

    public CUIComponent() : base()
    {
      Info = CUI.CUITypes.GetInfo(GetType());
      SetupLayout();//HACK 
      this.Inject();
    }

    public override string ToString() => $"{this.GetType().Name}:{ID}:{AKA}";
  }
}