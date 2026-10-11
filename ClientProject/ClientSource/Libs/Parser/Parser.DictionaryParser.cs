// #define DebugParser

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
using System.Diagnostics;


namespace CUILibs
{
  public partial class Parser
  {
    public class DictionaryParser(Settings_Part.WhenParsing_Part settings)
    {
      public enum States
      {
        BeforeDict,
        ReadingKey,
        ReadingValue,
        LookingForNextPart,
        InKeyNestedCollection,
        InValueNestedCollection,
        InKeyQuotes,
        InValueQuotes,
        Done
      }

      public enum CollectionType { List, Dict }

      public Settings_Part.WhenParsing_Part Setting { get; } = settings;



      [Conditional("DebugParser")]
      private void DebugLog(string msg) => CUILibs.Logger.Default.Log(msg);

      private bool IsQuoteChar(char c) => c == '\'' || c == '\"' || c == '`';

      public Dictionary<string, string> Split(string raw)
      {
        DebugLog($"\nSplitting [{raw}]");

        if (string.IsNullOrEmpty(raw)) return [];

        Dictionary<string, string> dict = [];
        Stack<CollectionType> nestedCollections = new();


        void AddKVP(string key, string value)
        {
          dict[key.Trim()] = value.Trim();
        }

        int partStart = 0;
        char quoteChar = '\'';

        string key = "";
        string value = "";

        States State = States.BeforeDict;

        for (int i = 0; i < raw.Length; i++)
        {
          DebugLog($"processing [ {raw[i]} ]");

          switch (State)
          {
            case States.BeforeDict:
              if (raw[i] == Setting.DictStart)
              {
                State = States.ReadingKey;
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }
              break;

            case States.ReadingKey:
              if (raw[i] == Setting.DictKVSeparator)
              {
                State = States.ReadingValue;
                key = raw.Substring(partStart, i - partStart);
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }

              if (IsQuoteChar(raw[i]))
              {
                State = States.InKeyQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }
              break;

            case States.ReadingValue:

              if (raw[i] == Setting.ListStart)
              {
                State = States.InValueNestedCollection;
                nestedCollections.Push(CollectionType.List);
                partStart = i;
                DebugLog($"hit [ {raw[i]} ] State: {State} nestedCollections: {nestedCollections.Count}");
                break;
              }

              if (raw[i] == Setting.DictStart)
              {
                State = States.InValueNestedCollection;
                nestedCollections.Push(CollectionType.Dict);
                partStart = i;
                DebugLog($"hit [ {raw[i]} ] State: {State} nestedCollections: {nestedCollections.Count}");
                break;
              }

              if (nestedCollections.Count != 0) break;

              if (raw[i] == Setting.DictSeparator)
              {
                State = States.ReadingKey;
                value = raw.Substring(partStart, i - partStart);
                AddKVP(key, value);
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }

              if (raw[i] == Setting.DictEnd)
              {
                State = States.Done;
                value = raw.Substring(partStart, i - partStart);
                AddKVP(key, value);
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }

              if (IsQuoteChar(raw[i]))
              {
                State = States.InValueQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }
              break;

            case States.InValueNestedCollection:
              if (raw[i] == Setting.ListStart)
              {
                nestedCollections.Push(CollectionType.List);
                DebugLog($"hit [ {raw[i]} ] State: {State} nestedCollections: {nestedCollections.Count}");
                break;
              }

              if (raw[i] == Setting.DictStart)
              {
                nestedCollections.Push(CollectionType.Dict);
                DebugLog($"hit [ {raw[i]} ] State: {State} nestedCollections: {nestedCollections.Count}");
                break;
              }

              if (
                (raw[i] == Setting.DictEnd && nestedCollections.Peek() == CollectionType.Dict) ||
                (raw[i] == Setting.ListEnd && nestedCollections.Peek() == CollectionType.List)
              )
              {
                nestedCollections.Pop();

                if (nestedCollections.Count == 0)
                {
                  value = raw.Substring(partStart, i - partStart + 1);
                  AddKVP(key, value);
                  State = States.LookingForNextPart;
                }
                DebugLog($"hit [ {raw[i]} ] State: {State} nestedCollections: {nestedCollections.Count}");
                break;
              }
              break;

            case States.InKeyQuotes:
              if (IsQuoteChar(raw[i]) && raw[i] == quoteChar)
              {
                key = raw.Substring(partStart, i - partStart);
                State = States.LookingForNextPart;
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }
              break;

            case States.InValueQuotes:
              if (IsQuoteChar(raw[i]) && raw[i] == quoteChar)
              {
                value = raw.Substring(partStart, i - partStart);
                AddKVP(key, value);
                State = States.LookingForNextPart;
                DebugLog($"hit [ {raw[i]} ] State: {State}");
                break;
              }
              break;

            case States.LookingForNextPart:
              if (raw[i] == Setting.DictSeparator)
              {
                State = States.ReadingKey;
                partStart = i + 1;
              }

              if (raw[i] == Setting.DictKVSeparator)
              {
                State = States.ReadingValue;
                partStart = i + 1;
              }

              DebugLog($"hit [ {raw[i]} ] State: {State}");
              break;

          }
        }


        return dict;
      }
    }



  }
}
