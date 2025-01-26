using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// A Grid containing children in its cells
  /// </summary>
  public class CUIGrid : CUIComponent
  {
    public override CUILayout Layout
    {
      get => layout;
      set
      {
        layout = new CUILayoutGrid();
        layout.Host = this;
      }
    }



    public CUILayoutGrid GridLayout => (CUILayoutGrid)Layout;



    public CUIGrid() : base()
    {
      //Layout = new CUILayoutGrid();
    }

  }
}