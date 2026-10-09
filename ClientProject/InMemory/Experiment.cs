using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using System.Xml.Linq;
using Barotrauma.LuaCs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class Mod : IAssemblyPlugin
  {

    public class EnumerableParser
    {
      public enum States
      {
        beforeArray, reading, lookingForNextPart, insideQuotes, done
      }



      private States State;
      private int partStart;
      private char quoteChar;

      private bool IsQuoteChar(char c) => c == '\'' || c == '\"' || c == '`';

      public List<string> ParseEnumerable(string raw)
      {
        if (raw == null || raw == "") return [];

        List<string> parts = [];

        for (int i = 0; i < raw.Length; i++)
        {
          switch (State)
          {
            case States.beforeArray:
              if (raw[i] == '[')
              {
                State = States.reading;
                partStart = i + 1;
                continue;
              }

              break;

            case States.reading:
              if (raw[i] == ',')
              {
                parts.Add(raw.Substring(partStart, i - partStart));
                partStart = i + 1;
                continue;
              }

              if (IsQuoteChar(raw[i]))
              {
                State = States.insideQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                continue;
              }

              if (raw[i] == ']')
              {
                State = States.done;
                parts.Add(raw.Substring(partStart, i - partStart));
                continue;
              }

              break;

            case States.lookingForNextPart:
              if (raw[i] == ',')
              {
                State = States.reading;
                partStart = i + 1;
                continue;
              }

              break;

            case States.insideQuotes:
              if (IsQuoteChar(raw[i]))
              {
                if (quoteChar == raw[i])
                {
                  State = States.lookingForNextPart;
                  parts.Add(raw.Substring(partStart, i - partStart));
                }
                continue;
              }

              break;
          }
        }


        return parts;
      }
    }



    public void Experiment()
    {
      EnumerableParser parser = new();

      Logger.Log(Logger.Wrap.IEnumerable(
        parser.ParseEnumerable("['123',qwe,'321']")
      ));

      // new OverridingVirtualProps().Run();
    }
  }
}