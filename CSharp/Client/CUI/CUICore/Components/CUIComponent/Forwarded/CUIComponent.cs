using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  //CRINGE (mega cringe)
  public partial class CUIComponent
  {

    protected virtual void NotifyThatLayoutHasChanged() { }
  }
}