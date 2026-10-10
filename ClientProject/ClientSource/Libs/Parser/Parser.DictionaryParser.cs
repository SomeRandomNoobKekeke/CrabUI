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
        BeforeDict, ReadingKey, ReadingValue, LookingForNextPart, InsideQuotes, Done
      }

      public Settings_Part.WhenParsing_Part Setting { get; } = settings;

      private States PrevState;
      private States _State; private States State
      {
        get => _State;
        set
        {
          PrevState = _State;
          _State = value;
        }
      }

      [Conditional("DebugParser")]
      private void DebugLog(string msg) => CUILibs.Logger.Default.Log(msg);

      private bool IsQuoteChar(char c) => c == '\'' || c == '\"' || c == '`';

      public Dictionary<string, string> Split(string raw)
      {
        DebugLog($"\nSplitting [{raw}]");

        if (string.IsNullOrEmpty(raw)) return [];

        Dictionary<string, string> dict = [];


        int partStart = 0;
        char quoteChar = '\'';

        string key = "";
        string value = "";

        State = States.BeforeDict;

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
                DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
                continue;
              }
              break;

            case States.ReadingKey:
              if (raw[i] == Setting.DictKVSeparator)
              {
                State = States.ReadingValue;
                key = raw.Substring(partStart, i - partStart);
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
                continue;
              }

              if (IsQuoteChar(raw[i]))
              {
                State = States.InsideQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
                continue;
              }
              break;

            case States.ReadingValue:
              if (raw[i] == Setting.DictSeparator)
              {
                State = States.ReadingKey;
                value = raw.Substring(partStart, i - partStart);
                dict[key] = value;
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
                continue;
              }

              if (raw[i] == Setting.DictEnd)
              {
                State = States.Done;
                value = raw.Substring(partStart, i - partStart);
                dict[key] = value;
                DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
                continue;
              }

              if (IsQuoteChar(raw[i]))
              {
                State = States.InsideQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
                continue;
              }
              break;

            case States.InsideQuotes:
              if (IsQuoteChar(raw[i]) && raw[i] == quoteChar)
              {
                if (PrevState == States.ReadingKey)
                {
                  key = raw.Substring(partStart, i - partStart);
                }

                if (PrevState == States.ReadingValue)
                {
                  value = raw.Substring(partStart, i - partStart);
                  dict[key] = value;
                }

                State = States.LookingForNextPart;

                DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
                continue;
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

              DebugLog($"hit [ {raw[i]} ] State: {State} PrevState: {PrevState}");
              break;

          }
        }


        return dict;
      }
    }



  }
}
