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



    private CUIMainComponent _mainComponent;
    public CUIMainComponent MainComponent
    {
      get => _mainComponent;
      private set
      {
        void setRec(CUIComponent component)
        {
          component._mainComponent = value;
          foreach (CUIComponent child in component.children)
          {
            setRec(child);
          }
        }

        setRec(this);
      }
    }

    protected virtual void InitModules() { }

    public DragHandle DragHandle;

    public CUIComponent()
    {
      ID = MaxID++;
      Layout = new PlainLayout(this, new ListProxy<IBasicLayoutElement>(Children));

      InitModules();
      InjectProps();
      WireUpProps();

      DragHandle = new DragHandle()
      {
        Host = this,
        Active = true,
      };
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