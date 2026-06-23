using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIDebugger
  {
    public class GatesPageComponent : CUIPage
    {
      public class GateButton : CUIButton
      {
        public string Name;

        public void UpdateColor()
        {
          MasterColor = CUI.DebugHub.Gates[Name].IsOpen ? new Color(0, 200, 0) : Color.Blue;
        }

        public GateButton(string name) : base()
        {
          Name = name;

          Text = Name;
          TextAnchor = CUIAnchor.LeftCenter;
          IsDebugTool = true;
          UpdateColor();

          MouseDown += (c, e) =>
          {
            CUI.DebugHub.Gates[Name].Toggle();
            UpdateColor();
          };
        }
      }


      public CUIVerticalList GateList;

      public void Refresh()
      {
        GateList.Clear();

        foreach (string name in CUI.DebugHub.Gates.Names)
        {
          GateList.Append(new GateButton(name));
        }
      }

      public GatesPageComponent() : base()
      {
        OnOpen.Add(Refresh);

        Background.Color = new Color(0, 255, 200);

        this["list"] = GateList = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Scrollable = true,
        };
      }
    }
  }
}