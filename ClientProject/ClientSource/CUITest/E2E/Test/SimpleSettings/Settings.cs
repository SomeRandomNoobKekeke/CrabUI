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
using System.IO;
using System.Text.Json;

namespace CrabUIUser
{
  public partial class E2ETestPack
  {
    public partial class SimpleSettings : IE2ETest
    {

      public class Settings
      {
        public static object MicroParser(string raw, Type T)
        {
          if (T == typeof(string)) return raw;
          if (T == typeof(int)) return int.Parse(raw);
          if (T == typeof(float)) return float.Parse(raw);

          return null;
        }

        public void SetValue(string key, string value)
        {
          PropertyInfo pi = typeof(Settings).GetProperty(key);
          if (pi is null) return;

          pi.SetValue(this, MicroParser(value, pi.PropertyType));
        }

        public void Print()
        {
          Logger.Default.Log(JsonSerializer.Serialize(this));
        }

        public string Name { get; set; } = "bruh";
        public int Count { get; set; } = 123;
      }

    }
  }
}