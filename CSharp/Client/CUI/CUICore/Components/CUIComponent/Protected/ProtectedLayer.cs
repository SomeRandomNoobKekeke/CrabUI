// using System;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using System.Linq;
// using System.Reflection;
// using System.Diagnostics;
// using Barotrauma;
// using Microsoft.Xna.Framework;

// namespace CrabUI
// {
//   public partial class CUIComponent
//   {
//     protected MainComponentTracker MainComponentTracker { get; } = new();
//     protected LayoutSlot LayoutSlot { get; } = new();
//     protected LayoutMarker LayoutMarker { get; } = new();
//     protected TreeModule Tree { get; } = new();

//     protected bool TreeChanged
//     {
//       get => Tree.TreeChanged;
//       set => Tree.TreeChanged = value;
//     }

//     protected void NotifyMainComponent()
//     {
//       MainComponentTracker.MainComponent?.LayoutChanged();
//     }
//   }
// }