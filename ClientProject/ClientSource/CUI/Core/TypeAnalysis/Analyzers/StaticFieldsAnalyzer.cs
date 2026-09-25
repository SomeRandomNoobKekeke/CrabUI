using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public static class StaticFieldsAnalyzer
  {
    public static void PrintAllStaticFields(bool skipClosures = true)
    {
      PrintStaticDelegateFields(skipClosures);
      PrintOtherStaticFields(skipClosures);
    }

    public static void PrintStaticDelegateFields(bool skipClosures = true)
    {
      bool IsDelegate(Type T) => T.IsAssignableTo(typeof(Delegate));
      bool IsClosure(Type T) => T.Name.StartsWith("<>");

      CUI.Logger.Print($"\nAll static delegate fields in CUI:", Color.Lime);
      foreach (Type T in typeof(CUI).Assembly.GetTypes())
      {
        if (skipClosures && IsClosure(T)) continue;
        if (T.Namespace != typeof(CUI).Namespace) continue;

        List<FieldInfo> fields = [];

        foreach (FieldInfo fi in T.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
        {
          if (!IsDelegate(fi.FieldType)) continue;
          if (skipClosures && IsClosure(fi.FieldType)) continue;
          fields.Add(fi);
        }

        if (fields.Count > 0)
        {
          CUI.Logger.Log($"Static delegates in {T.GetFullName()}:");
          foreach (FieldInfo fi in fields)
          {
            CUI.Logger.Log($" - {fi}");
          }
        }
      }
    }


    public static void PrintOtherStaticFields(bool skipClosures = true)
    {
      bool IsDelegate(Type T) => T.IsAssignableTo(typeof(Delegate));
      bool IsClosure(Type T) => T.Name.StartsWith("<>");

      CUI.Logger.Print($"\nOther static fields in CUI:", Color.Lime);
      foreach (Type T in typeof(CUI).Assembly.GetTypes())
      {
        if (T.Namespace != typeof(CUI).Namespace) continue;

        List<FieldInfo> fields = [];

        foreach (FieldInfo fi in T.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
        {
          if (IsDelegate(fi.FieldType)) continue;
          if (skipClosures && IsClosure(fi.FieldType)) continue;
          fields.Add(fi);
        }

        if (fields.Count > 0)
        {
          CUI.Logger.Log($"Static fields in {T.GetFullName()}:");
          foreach (FieldInfo fi in fields)
          {
            CUI.Logger.Log($" - {fi}");
          }
        }
      }
    }

    public static void PrintStaticFieldsInType(Type T)
    {
      CUI.Logger.Log($"Static fields in {T.GetFullName()}:");
      foreach (FieldInfo fi in T.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
      {
        CUI.Logger.Log($" - {fi}");
      }
    }
  }
}