using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CursedUI
{
  public static class Random_Extensions
  {
    public const string chars = "ABCDEFGHJKMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz123456789";
    public static string RandomString(this Random rng, int length)
      => new string(rng.GetItems<char>(chars, length));


    // Addition to https://github.com/FakeFishGames/Barotrauma/blob/master/Libraries/BarotraumaLibs/BarotraumaCore/Extensions/RngExtensions.cs
    public static int Range(this Random rng, int minimum, int maximum)
      => (rng.Next() % (maximum - minimum)) + minimum;

    public static Vector2 Vector2(this Random rng)
      => new Vector2(rng.NextSingle(), rng.NextSingle());
  }
}