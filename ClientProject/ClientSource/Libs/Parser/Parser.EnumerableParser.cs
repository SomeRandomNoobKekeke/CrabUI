using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using System.IO;
using System.Text;


namespace CUILibs
{
  public partial class Parser
  {
    public class EnumerableParser(Settings_Part.WhenParsing_Part settings)
    {
      public enum States
      {
        BeforeArray, Reading, LookingForNextPart, InsideQuotes, Done
      }

      public Settings_Part.WhenParsing_Part Setting { get; } = settings;

      private States State;
      private int partStart;
      private char quoteChar;

      private bool IsQuoteChar(char c) => c == '\'' || c == '\"' || c == '`';

      public List<string> Split(string raw)
      {
        if (string.IsNullOrEmpty(raw)) return [];

        List<string> parts = [];


        State = States.BeforeArray;

        for (int i = 0; i < raw.Length; i++)
        {
          switch (State)
          {
            case States.BeforeArray:
              if (raw[i] == Setting.EnumerableStart)
              {
                State = States.Reading;
                partStart = i + 1;
                continue;
              }

              break;

            case States.Reading:
              if (raw[i] == Setting.EnumerableSeparator)
              {
                parts.Add(raw.Substring(partStart, i - partStart));
                partStart = i + 1;
                continue;
              }

              if (IsQuoteChar(raw[i]))
              {
                State = States.InsideQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                continue;
              }

              if (raw[i] == Setting.EnumerableEnd)
              {
                State = States.Done;
                parts.Add(raw.Substring(partStart, i - partStart));
                continue;
              }

              break;

            case States.LookingForNextPart:
              if (raw[i] == Setting.EnumerableSeparator)
              {
                State = States.Reading;
                partStart = i + 1;
                continue;
              }

              break;

            case States.InsideQuotes:
              if (IsQuoteChar(raw[i]))
              {
                if (quoteChar == raw[i])
                {
                  State = States.LookingForNextPart;
                  parts.Add(raw.Substring(partStart, i - partStart));
                }
              }

              break;
          }
        }


        return parts;
      }
    }



  }
}
