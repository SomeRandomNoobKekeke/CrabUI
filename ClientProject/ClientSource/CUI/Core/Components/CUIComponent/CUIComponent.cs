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
    public class Part : IPart { public CUIComponent Self { get; set; } }

    public static Dictionary<int, WeakReference<CUIComponent>> ComponentsById = new();
    public static IEnumerable<CUIComponent> AllComponents => ComponentsById.Values
      .Select(wr =>
      {
        wr.TryGetTarget(out CUIComponent component);
        return component;
      }).Where(c => c != null);

    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIComponent>((c) =>
    {
      c.Background.Color = Color.Transparent;
    });


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
      this.Inject();
      SetupLayout();

      if (CUICore.Styles.HasStylesFor(this.GetType()))
      {
        foreach (ICUIStyle style in CUICore.Styles.GetAllStylesFor(this.GetType()))
        {
          style.Apply(this);
        }
      }
    }

    public override string ToString() => $"{this.GetType().Name}:{ID}:{AKA}";
  }
}