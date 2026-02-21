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
  //BRUH it's used by CUIProp, CUIMainComponent and LayoutMarker, but do i really need to split it? 
  public interface ILayoutHost
  {
    public void UpdateChildren();
    public void UpdateParent();

    public void MarkLayout(LayoutMarkPattern pattern);
    public void MarkAsRequireChildrenUpdate();
    public void MarkAsRequireParentUpdate();
  }
}