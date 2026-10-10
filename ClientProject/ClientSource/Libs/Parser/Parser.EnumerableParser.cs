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
        BeforeArray, Reading, LookingForNextPart, InsideQuotes, InNestedCollection, Done
      }

      public enum CollectionType { List, Dict }

      public Settings_Part.WhenParsing_Part Setting { get; } = settings;

      private States State;
      private int partStart;
      private char quoteChar;

      private bool IsQuoteChar(char c) => c == '\'' || c == '\"' || c == '`';

      public List<string> Split(string raw)
      {
        if (string.IsNullOrEmpty(raw)) return [];

        List<string> parts = [];

        Stack<CollectionType> nestedCollections = new();


        State = States.BeforeArray;

        for (int i = 0; i < raw.Length; i++)
        {
          switch (State)
          {
            case States.BeforeArray:
              if (raw[i] == Setting.ListStart)
              {
                State = States.Reading;
                partStart = i + 1;
              }

              break;

            case States.Reading:
              if (raw[i] == Setting.ListSeparator)
              {
                parts.Add(raw.Substring(partStart, i - partStart));
                partStart = i + 1;
                break;
              }

              if (raw[i] == Setting.ListStart)
              {
                State = States.InNestedCollection;
                nestedCollections.Push(CollectionType.List);
                partStart = i;
                break;
              }

              if (raw[i] == Setting.DictStart)
              {
                State = States.InNestedCollection;
                nestedCollections.Push(CollectionType.Dict);
                partStart = i;
                break;
              }

              if (nestedCollections.Count != 0) break;

              if (IsQuoteChar(raw[i]))
              {
                State = States.InsideQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                break;
              }

              if (raw[i] == Setting.ListEnd)
              {
                State = States.Done;
                parts.Add(raw.Substring(partStart, i - partStart));
                break;
              }

              break;

            case States.LookingForNextPart:
              if (raw[i] == Setting.ListSeparator)
              {
                State = States.Reading;
                partStart = i + 1;
                break;
              }

              break;

            case States.InNestedCollection:
              if (
                (raw[i] == Setting.DictEnd && nestedCollections.Peek() == CollectionType.Dict) ||
                (raw[i] == Setting.ListEnd && nestedCollections.Peek() == CollectionType.List)
              )
              {
                nestedCollections.Pop();

                if (nestedCollections.Count == 0)
                {
                  parts.Add(raw.Substring(partStart, i - partStart + 1));
                  State = States.LookingForNextPart;
                }
                break;
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
