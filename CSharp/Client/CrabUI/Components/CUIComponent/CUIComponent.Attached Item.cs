using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;
using HarmonyLib;

namespace CrabUI
{
  public partial class CUIComponent : IDisposable
  {
    public void AttachTo(Item item, Action<Item, CUIComponent> callback = null) => AttachedItems.Connect(item, this, callback);
    public AttachedItemHandle AttachedItemHandle { get; set; }
  }
}