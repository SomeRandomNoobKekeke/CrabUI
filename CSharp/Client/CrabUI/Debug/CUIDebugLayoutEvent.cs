#define CUIDEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace CrabUI
{
  public class CUIDebugEvent
  {
    public CUIComponent Host;
    public CUIComponent Target;
    public string Method;
    public string SProp;
    public string TProp;
    public string Value;
    public CUIDebugEvent(CUIComponent host, CUIComponent target, string method, string sprop, string tprop, string value)
    {
      Host = host;
      Target = target;
      Method = method ?? "";
      SProp = sprop ?? "";
      TProp = tprop ?? "";
      Value = value ?? "";
    }
  }


  public class CUIDebugEventComponent : CUIComponent
  {
    public static Dictionary<int, Color> CapturedIDs = new Dictionary<int, Color>();



    private CUIDebugEvent _value; public CUIDebugEvent Value
    {
      get => _value;
      set
      {
        _value = value;

        Revealed = value != null;
        if (value != null)
        {
          LastUpdate = Timing.TotalTime;
          AssignColor();
        }
        MakeText();
      }
    }

    public void Flush() => Value = null;

    private void MakeText()
    {
      if (Value == null)
      {
        Line1 = "";
        Line2 = "";
      }
      else
      {
        Line1 = $"  {Value.Target} in {Value.Host}.{Value.Method}";
        Line2 = $"  {Value.SProp} -> {Value.TProp} {Value.Value}";
      }
    }

    public static Random random = new Random();

    private static float NextColor;
    private static float ColorShift = 0.05f;
    private void AssignColor()
    {
      if (Value.Target == null) return;

      if (CapturedIDs.ContainsKey(Value.Target.ID))
      {
        BackgroundColor = CapturedIDs[Value.Target.ID];
      }
      else
      {
        // float r = random.NextSingle();
        // float scale = 20;
        // r = (float)Math.Round(r * scale) / scale;

        CapturedIDs[Value.Target.ID] = GetColor(NextColor);

        NextColor += ColorShift;
        if (NextColor > 1) NextColor = 0;

        BackgroundColor = CapturedIDs[Value.Target.ID];
      }
    }


    public string Line1 = "";
    public string Line2 = "";

    public float UpdateTimer;
    public double LastUpdate;

    public Color GetColor(float d)
    {
      return ToolBox.GradientLerp(d,
        Color.Cyan * 0.5f,
        Color.Red * 0.5f,
        Color.Green * 0.5f,
        Color.Blue * 0.5f,
        Color.Magenta * 0.5f,
        Color.Yellow * 0.5f,
        Color.Cyan * 0.5f
      );
    }
    public Color GetColor2(float d)
    {
      return ToolBox.GradientLerp(Math.Min(0.8f, d),
        CapturedIDs[Value.Target.ID],
        Color.Black * 0.5f
      );
    }


    public override void Draw(SpriteBatch spriteBatch)
    {
      BackgroundColor = GetColor2((float)(Timing.TotalTime - LastUpdate));

      base.Draw(spriteBatch);

      GUIStyle.Font.Value.DrawString(spriteBatch, Line1, Real.Position, Color.White, rotation: 0, origin: Vector2.Zero, 0.9f, se: SpriteEffects.None, layerDepth: 0.1f);

      GUIStyle.Font.Value.DrawString(spriteBatch, Line2, Real.Position + new Vector2(0, 20), Color.White, rotation: 0, origin: Vector2.Zero, 0.9f, se: SpriteEffects.None, layerDepth: 0.1f);
    }


    public CUIDebugEventComponent(CUIDebugEvent value = null) : base()
    {
      Value = value;
      IgnoreDebug = true;
      BackgroundColor = Color.Green;
      Absolute = new CUINullRect(null, null, null, 40);


    }
  }
}