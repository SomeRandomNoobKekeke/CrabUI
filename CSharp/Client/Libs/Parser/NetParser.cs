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
using Barotrauma.Networking;

namespace BaroJunk
{
  public class NetParser
  {
    private static NetParser _Default;
    public static NetParser Default => _Default ??= new NetParser(SimpleParser.Default);

    public static string NullTerm = "[null]";//HACK bruh

    public static Dictionary<Type, Action<IWriteMessage, object>> EncodeTable = new()
    {
      [typeof(bool)] = (IWriteMessage msg, object data) => msg.WriteBoolean((bool)data),
      [typeof(byte)] = (IWriteMessage msg, object data) => msg.WriteByte((byte)data),
      [typeof(UInt16)] = (IWriteMessage msg, object data) => msg.WriteUInt16((UInt16)data),
      [typeof(Int16)] = (IWriteMessage msg, object data) => msg.WriteInt16((Int16)data),
      [typeof(UInt32)] = (IWriteMessage msg, object data) => msg.WriteUInt32((UInt32)data),
      [typeof(Int32)] = (IWriteMessage msg, object data) => msg.WriteInt32((Int32)data),
      [typeof(UInt64)] = (IWriteMessage msg, object data) => msg.WriteUInt64((UInt64)data),
      [typeof(Int64)] = (IWriteMessage msg, object data) => msg.WriteInt64((Int64)data),
      [typeof(Single)] = (IWriteMessage msg, object data) => msg.WriteSingle((Single)data),
      [typeof(Double)] = (IWriteMessage msg, object data) => msg.WriteDouble((Double)data),
      [typeof(string)] = (IWriteMessage msg, object data) =>
      {
        data ??= NullTerm;
        msg.WriteString((string)data);
      },
      [typeof(Identifier)] = (IWriteMessage msg, object data) => msg.WriteIdentifier((Identifier)data),
      [typeof(Color)] = (IWriteMessage msg, object data) => msg.WriteColorR8G8B8A8((Color)data),
    };

    public static Dictionary<Type, Func<IReadMessage, object>> DecodeTable = new()
    {
      [typeof(bool)] = (IReadMessage msg) => msg.ReadBoolean(),
      [typeof(byte)] = (IReadMessage msg) => msg.ReadByte(),
      [typeof(UInt16)] = (IReadMessage msg) => msg.ReadUInt16(),
      [typeof(Int16)] = (IReadMessage msg) => msg.ReadInt16(),
      [typeof(UInt32)] = (IReadMessage msg) => msg.ReadUInt32(),
      [typeof(Int32)] = (IReadMessage msg) => msg.ReadInt32(),
      [typeof(UInt64)] = (IReadMessage msg) => msg.ReadUInt64(),
      [typeof(Int64)] = (IReadMessage msg) => msg.ReadInt64(),
      [typeof(Single)] = (IReadMessage msg) => msg.ReadSingle(),
      [typeof(Double)] = (IReadMessage msg) => msg.ReadDouble(),
      [typeof(string)] = (IReadMessage msg) =>
      {
        string s = msg.ReadString();
        return s == NullTerm ? null : s;
      },
      [typeof(Identifier)] = (IReadMessage msg) => msg.ReadIdentifier(),
      [typeof(Color)] = (IReadMessage msg) => msg.ReadColorR8G8B8A8(),
    };

    public SimpleParser Parser { get; set; } = new SimpleParser();

    public SimpleResult Encode(IWriteMessage msg, object data) => Encode(msg, data, data.GetType());
    public SimpleResult Encode(IWriteMessage msg, object data, Type dataType)
    {
      if (EncodeTable.ContainsKey(dataType))
      {
        try
        {
          EncodeTable[dataType](msg, data);
          return SimpleResult.Success();
        }
        catch (Exception e)
        {
          return SimpleResult.Failure($"-- NetParser couldn't encode [{dataType}] into IWriteMessage because {Parser.Custom.ExceptionMessage(e)}", e);
        }
      }
      else
      {
        if (!dataType.IsPrimitive)
        {
          MethodInfo encode = dataType.GetMethod("NetEncode", BindingFlags.Public | BindingFlags.Instance);
          if (encode is not null)
          {
            try
            {
              encode.Invoke(data, new object[] { msg });
              return SimpleResult.Success();
            }
            catch (Exception e)
            {
              return SimpleResult.Failure($"-- NetParser couldn't encode [{dataType}] into IWriteMessage because {Parser.Custom.ExceptionMessage(e)}", e);
            }
          }

          // try Encode as string
          try
          {
            SimpleResult result = Parser.Serialize(data);
            if (result.Ok)
            {
              EncodeTable[typeof(string)].Invoke(msg, result.Result);
              return SimpleResult.Success();
            }
            else
            {
              return SimpleResult.Failure($"-- NetParser couldn't encode [{dataType}] into IWriteMessage because {result.Details}", result.Exception);
            }
          }
          catch (Exception e)
          {
            return SimpleResult.Failure($"-- NetParser couldn't encode [{dataType}] into IWriteMessage because {Parser.Custom.ExceptionMessage(e)}", e);
          }

          return SimpleResult.Failure($"-- NetParser couldn't encode [{dataType}] into IWriteMessage because it doesn't have {Parser.Custom.WrapInColor($"public void NetEncode(IWriteMessage msg)", "white")} method");
        }
        else
        {
          return SimpleResult.Failure($"-- NetParser couldn't encode primitive [{dataType}] into IWriteMessage because lazy dev forgor to add it to EncodeTable");
        }
      }
    }

    public SimpleResult Decode(IReadMessage msg, Type T)
    {
      if (DecodeTable.ContainsKey(T))
      {
        try
        {
          return SimpleResult.Success(DecodeTable[T](msg));
        }
        catch (Exception e)
        {
          return new SimpleResult()
          {
            Ok = false,
            Result = Parser.DefaultFor(T),
            Details = $"-- NetParser couldn't decode [{T}] from IReadMessage because {Parser.Custom.ExceptionMessage(e)}",
            Exception = e,
          };
        }
      }
      else
      {
        MethodInfo decode = T.GetMethod("NetDecode", BindingFlags.Public | BindingFlags.Static);
        if (decode is not null)
        {
          try
          {
            return SimpleResult.Success(decode.Invoke(null, new object[] { msg }));
          }
          catch (Exception e)
          {
            return new SimpleResult()
            {
              Ok = false,
              Result = Parser.DefaultFor(T),
              Details = $"-- NetParser couldn't decode [{T}] from IReadMessage because {Parser.Custom.ExceptionMessage(e)}",
              Exception = e,
            };
          }
        }

        // try Decode as string
        try
        {
          SimpleResult result = Parser.Parse((string)DecodeTable[typeof(string)].Invoke(msg), T);
          if (result.Ok)
          {
            return SimpleResult.Success(result.Result);
          }
          else
          {
            return SimpleResult.Failure($"-- NetParser couldn't decode [{T}] from IReadMessage because {result.Details}", result.Exception);
          }

        }
        catch (Exception e)
        {
          return SimpleResult.Failure($"-- NetParser couldn't decode [{T}] from IReadMessage because {Parser.Custom.ExceptionMessage(e)}", e);
        }

        return new SimpleResult()
        {
          Ok = false,
          Result = Parser.DefaultFor(T),
          Details = $"-- NetParser couldn't decode [{T}] from IReadMessage because [{T}] doesn't have {Parser.Custom.WrapInColor($"public static {T.Name} NetDecode(IReadMessage msg)", "white")} method",
        };
      }
    }

    public NetParser() { }
    public NetParser(SimpleParser parser) => Parser = parser;

  }
}

