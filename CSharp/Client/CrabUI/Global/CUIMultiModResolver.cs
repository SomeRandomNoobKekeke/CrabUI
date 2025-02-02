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

namespace CrabUI
{
  public class CUIMultiModResolver
  {
    internal static void InitStatic()
    {
      CUI.OnInit += () =>
      {
        //FindOtherInputs();
      };
      CUI.OnDispose += () =>
      {
        CUIInputs.Clear();
        CUIs.Clear();
        MouseInputHandledMethods.Clear();
      };
    }

    public static List<object> CUIInputs = new();
    public static List<object> CUIs = new();
    public static List<Action<bool>> MouseInputHandledMethods = new();

    public static void MarkOtherInputsAsHandled()
    {
      //MouseInputHandledMethods.ForEach(action => action(true));

      foreach (object input in CUIInputs)
      {
        try
        {
          PropertyInfo setAsHandled = input.GetType().GetProperty("MouseInputHandled");
          setAsHandled.SetValue(input, true);
          CUI.Log($"setAsHandled.SetValue(input, true) for {input}");
        }
        catch (Exception e)
        {
          CUI.Warning($"Couldn't find MouseInputHandled in CUIInput in CUI from other mod ({input.GetType()})");
          continue;
        }
      }
    }

    public static void FindOtherInputs()
    {
      AppDomain currentDomain = AppDomain.CurrentDomain;

      foreach (Assembly asm in currentDomain.GetAssemblies())
      {
        foreach (Type T in asm.GetTypes())
        {
          if (T.Name == "CUI")
          {
            try
            {
              FieldInfo InstanceField = T.GetField("Instance", BindingFlags.Static | BindingFlags.Public);
              object CUIInstance = InstanceField.GetValue(null);
              if (CUIInstance != null && CUIInstance != CUI.Instance)
              {
                CUIs.Add(CUIInstance);
                FieldInfo inputField = T.GetField("input", AccessTools.all);

                object input = inputField.GetValue(CUIInstance);
                if (input != null) CUIInputs.Add(input);
              }
            }
            catch (Exception e)
            {
              CUI.Warning($"Couldn't find CUIInputs in CUI from other mod ({T})");
              continue;
            }
          }
        }
      }

      foreach (object input in CUIInputs)
      {
        try
        {
          PropertyInfo setAsHandled = input.GetType().GetProperty("MouseInputHandled");
          MouseInputHandledMethods.Add(setAsHandled.SetMethod.CreateDelegate<Action<bool>>(input));
        }
        catch (Exception e)
        {
          CUI.Warning($"Couldn't find MouseInputHandled in CUIInput in CUI from other mod ({input.GetType()})");
          continue;
        }
      }
    }


  }

}
