using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;

namespace BaroJunk
{
  /// <summary>
  /// Parse primitive types, can be extended with ExtraParsingMethods
  /// </summary>
  public class SimpleParser
  {
    static SimpleParser() => ProjectInfo.Add(new PackageInfo()
    {
      Name = "SimpleParser",
      Version = new Version(0, 0, 0)
      {
        Branch = "BaroJunk"
      },
      Dependencies = new List<PackageInfo>(){
        new PackageInfo(){
          Name = "SimpleResult",
          Version = new Version(0, 0, 0){ Branch = "BaroJunk", },
        }
      }
    });

    private static SimpleParser _Default;
    public static SimpleParser Default => _Default ??= new SimpleParser();

    public IExtraParsingMethods ExtraParsingMethods { get; set; } = new BasicExtraParsingMethods();
    public CustomSerializeMethods Custom { get; set; } = new CustomSerializeMethods();

    public object DefaultFor(Type T)
    {
      if (T == typeof(string)) return null;
      return Activator.CreateInstance(T);
    }

    /// <summary>
    /// Null is serialized into this, so you could distinguish null and empty string
    /// </summary>
    public string NullTerm = "{{null}}";

    public SimpleResult Parse(string raw, Type T)
    {
      if (raw == null) return SimpleResult.Success(null);
      if (raw == NullTerm) return SimpleResult.Success(null);
      if (T == typeof(string)) return SimpleResult.Success(raw);

      if (T.IsPrimitive)
      {
        MethodInfo parse = T.GetMethod(
          "Parse",
          BindingFlags.Public | BindingFlags.Static,
          new Type[] { typeof(string) }
        );

        try
        {
          return SimpleResult.Success(
            parse.Invoke(null, new object[] { raw })
          );
        }
        catch (Exception e)
        {
          return new SimpleResult()
          {
            Ok = false,
            Details = $"-- Parser couldn't parse [{raw}] into primitive type [{T}] because {Custom.ExceptionMessage(e)}",
            Exception = e,
            Result = DefaultFor(T),
          };
        }
      }

      if (T.IsEnum)
      {
        try
        {
          return SimpleResult.Success(
            Enum.Parse(T, raw)
          );
        }
        catch (Exception e)
        {
          return new SimpleResult()
          {
            Ok = false,
            Details = $"-- Parser couldn't parse [{raw}] into Enum [{T}] because {Custom.ExceptionMessage(e)}",
            Exception = e,
            Result = DefaultFor(T),
          };
        }
      }

      if (!T.IsPrimitive)
      {
        try
        {
          if (ExtraParsingMethods.Parse.ContainsKey(T))
          {
            return SimpleResult.Success(
              ExtraParsingMethods.Parse[T].Invoke(raw)
            );
          }

          MethodInfo parse = T.GetMethod(
            "Parse",
            BindingFlags.Public | BindingFlags.Static,
            new Type[] { typeof(string) }
          );

          if (parse == null)
          {
            return new SimpleResult()
            {
              Ok = false,
              Details = $"-- Parser couldn't parse [{raw}] into [{T}] because it doesn't have the Parse method",
              Result = DefaultFor(T),
            };
          }
          else
          {
            return SimpleResult.Success(
              parse.Invoke(null, new object[] { raw })
            );
          }
        }
        catch (Exception e)
        {
          return new SimpleResult()
          {
            Ok = false,
            Details = $"-- Parser couldn't parse [{raw}] into [{T}] because {Custom.ExceptionMessage(e)}",
            Exception = e,
            Result = DefaultFor(T),
          };
        }
      }

      return SimpleResult.Success(
        DefaultFor(T)
      );
    }

    public SimpleResult Serialize(object o)
    {
      if (o is null) return SimpleResult.Success(NullTerm);
      if (o.GetType() == typeof(string)) return SimpleResult.Success((string)o);

      try
      {
        if (ExtraParsingMethods.Serialize.ContainsKey(o.GetType()))
        {
          return SimpleResult.Success(ExtraParsingMethods.Serialize[o.GetType()].Invoke(o));
        }

        return SimpleResult.Success(o.ToString());
      }
      catch (Exception e)
      {
        return new SimpleResult()
        {
          Ok = false,
          Details = $"-- Parser couldn't serialize object of [{o.GetType()}] type because {Custom.ExceptionMessage(e)}",
          Exception = e,
          Result = NullTerm,
        };
      }
    }
  }
}
