using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{


  public partial class HowLayoutsShouldGenerallyWork : Experiment
  {
    public class MainComponent
    {
      public List<Component> Children;
      public void Update()
      {
        foreach (Component child in Children)
        {
          child.Layout.UpdateChildren();
        }
      }
    }



  }
}