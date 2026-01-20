using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public static int MaxID { get; private set; }
    public int ID { get; set; }

    public CUIMainComponent MainComponent { get; private set; }

    protected virtual void InitModules() { }

    public CUIComponent()
    {
      ID = MaxID++;
      Layout = new PlainLayout(this, new ListProxy<IBasicLayoutElement>(Children));

      InitModules();
      InjectProps();
      WireUpProps();
      LayoutMarker = new LayoutMarker(this);
    }

    //CRINGE
    private void InjectProps()
    {
      AllCUIProps.Clear();
      foreach (PropertyInfo pi in typeof(CUIPropsWrapper).GetProperties())
      {
        CUIProp prop = (CUIProp)pi.GetValue(CUIProps);
        AllCUIProps.Add(prop);
        prop.Host = this;
        prop.Name = pi.Name;

        if (prop is ICUILayoutProp layoutProp)
        {
          layoutProp.LayoutHost = this;

        }


      }
    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}