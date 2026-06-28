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
      //No, i'm not gonna create fully functional settings manager here, this isn't the point of this test
      public class MicroSettingsManager
      {
        public Settings Settings { get; set; } = new();

        public static T MicroParser<T>(string raw) => (T)MicroParser(raw, typeof(T));
        public static object MicroParser(string raw, Type T)
        {
          if (T == typeof(string)) return raw;
          if (T == typeof(int)) return int.Parse(raw);
          if (T == typeof(float)) return float.Parse(raw);

          return null;
        }

        public void SetValue(string key, string value)
        {
          if (key == "String Prop") Settings.StringProp = value;
          if (key == "Int Prop") Settings.IntProp = MicroParser<int>(value);
          if (key == "Nested String Prop") Settings.Nested.StringProp = value;
          if (key == "Nested Int Prop") Settings.Nested.IntProp = MicroParser<int>(value);
        }

        public void Print()
        {
          Logger.Default.Log(JsonSerializer.Serialize(Settings));
        }

      }
    }
  }
}