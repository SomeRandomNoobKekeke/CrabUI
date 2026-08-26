using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIGrid : CUIComponent, IComponent
  {

    protected partial class CUIGridLayout_Host_Adapter_Part : Adapters_Part.CUIPlainLayout_Host_Part, CUIGridLayout.Host
    {
      private CUIGrid _Self; public new CUIGrid Self
      {
        get => _Self;
        set
        {
          _Self = value;
          base.Self = value;
        }
      }

      ICollection<CUIGridLayout.Line> CUIGridLayout.Host.RowSizes => Self.RowSizes;
      ICollection<CUIGridLayout.Line> CUIGridLayout.Host.ColumnSizes => Self.ColumnSizes;
    }


    //TODO [CUISerializableProp]
    public List<CUIGridLayout.Line> RowSizes { get; set; } = new();
    public List<CUIGridLayout.Line> ColumnSizes { get; set; } = new();

    protected CUIGridLayout GridLayout;


    [InitMethod]
    protected override void InitLayout()
    {
      GridLayout = new();
      Layout = GridLayout;
      Layout.ConnectTo(new CUIGridLayout_Host_Adapter_Part() { Self = this });
    }
  }
}