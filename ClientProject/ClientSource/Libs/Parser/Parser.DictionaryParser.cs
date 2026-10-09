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
    public class DictionaryParser
    {
      public enum States
      {
        BeforeDict, ReadingKey, ReadingValue, LookingForNextPart, InsideQuotes, Done
      }

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


      private bool IsQuoteChar(char c) => c == '\'' || c == '\"' || c == '`';

      public Dictionary<string, string> Split(string raw)
      {
        if (raw == null || raw == "") return [];

        Dictionary<string, string> dict = [];


        int partStart = 0;
        char quoteChar = '\'';

        string key = "";
        string value = "";

        State = States.BeforeDict;

        for (int i = 0; i < raw.Length; i++)
        {
          switch (State)
          {
            case States.BeforeDict:
              if (raw[i] == '{')
              {
                State = States.ReadingKey;
                partStart = i + 1;
                continue;
              }
              break;

            case States.ReadingKey:
              if (raw[i] == ':')
              {
                State = States.ReadingValue;
                key = raw.Substring(partStart, i - partStart);
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
              break;

            case States.ReadingValue:
              if (raw[i] == ',')
              {
                State = States.ReadingKey;
                value = raw.Substring(partStart, i - partStart);
                dict[key] = value;
                partStart = i + 1;
                continue;
              }

              if (raw[i] == '}')
              {
                State = States.Done;
                value = raw.Substring(partStart, i - partStart);
                dict[key] = value;
                continue;
              }

              if (IsQuoteChar(raw[i]))
              {
                State = States.InsideQuotes;
                quoteChar = raw[i];
                partStart = i + 1;
                continue;
              }
              break;

            case States.InsideQuotes:
              if (IsQuoteChar(raw[i]) && raw[i] == quoteChar)
              {
                State = States.LookingForNextPart;

                if (PrevState == States.ReadingKey)
                {
                  key = raw.Substring(partStart, i - partStart);
                }

                if (PrevState == States.ReadingValue)
                {
                  value = raw.Substring(partStart, i - partStart);
                  dict[key] = value;
                }
                continue;
              }
              break;

            case States.LookingForNextPart:
              if (raw[i] == ',')
              {
                State = States.ReadingKey;
                partStart = i + 1;
              }

              if (raw[i] == ':')
              {
                State = States.ReadingValue;
                partStart = i + 1;
              }
              break;

          }
        }


        return dict;
      }
    }



  }
}
