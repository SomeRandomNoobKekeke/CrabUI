using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  /// <summary>
  /// WIP, can animate any property on any object
  /// Can run back and forth in [0..1] interval and 
  /// interpolate any property between StartValue and EndValue
  /// </summary>
  public class CUIAnimation
  {
    internal static void InitStatic()
    {
      CUI.OnDispose += () => ActiveAnimations.Clear();
    }

    public static HashSet<CUIAnimation> ActiveAnimations = new();
    /// <summary>
    /// This is called in CUIUpdate
    /// </summary>
    internal static void UpdateAllAnimations(double time)
    {
      foreach (CUIAnimation animation in ActiveAnimations)
      {
        animation.Step(time);
      }
    }

    public bool Debug { get; set; }
    public static float StartLambda = 0.0f;
    public static float EndLambda = 1.0f;


    private object target;
    /// <summary>
    /// Object containing animated property
    /// </summary>
    public object Target
    {
      get => target;
      set
      {
        target = value;
        UpdateSetter();
      }
    }
    private bool active;
    public bool Active
    {
      get => active;
      set
      {
        if (Blocked || active == value) return;
        active = value;

        if (active) ActiveAnimations.Add(this);
        else ActiveAnimations.Remove(this);
        ApplyValue();
      }
    }

    /// <summary>
    /// In seconds
    /// </summary>
    public double Duration
    {
      get => 1.0 / Speed * Timing.Step;
      set
      {
        double steps = value / Timing.Step;
        Speed = 1.0 / steps;
      }
    }

    public double ReverseDuration
    {
      get => 1.0 / (BackSpeed ?? 0) * Timing.Step;
      set
      {
        double steps = value / Timing.Step;
        BackSpeed = 1.0 / steps;
      }
    }

    /// <summary>
    /// Will prevent it from starting
    /// </summary>
    public bool Blocked { get; set; }
    /// <summary>
    /// Progress of animation [0..1]
    /// </summary>
    public double Lambda { get; set; }
    /// <summary>
    /// Lambda increase per update step, calculated when you set Duration
    /// </summary>
    public double Speed { get; set; } = 0.01;
    public double? BackSpeed { get; set; }
    /// <summary>
    /// If true animation won't stop when reaching end, it will change direction
    /// </summary>
    public bool Bounce { get; set; }
    /// <summary>
    /// Straight, Reverse
    /// </summary>
    public CUIDirection Direction { get; set; }
    /// <summary>
    /// Value will be interpolated between these values 
    /// </summary>
    public object StartValue { get; set; }
    public object EndValue { get; set; }

    private string property;
    private Action<object> setter;
    private Type propertyType;
    /// <summary>
    /// Property name that is animated
    /// </summary>
    public string Property
    {
      get => property;
      set
      {
        property = value;
        UpdateSetter();
      }
    }

    public event Action<CUIDirection> OnStop;
    /// <summary>
    /// You can set custon Interpolate function
    /// </summary>
    public Func<float, object> Interpolate
    {
      get => interpolate;
      set
      {
        customInterpolate = value;
        UpdateSetter();
      }
    }
    private Func<float, object> customInterpolate;
    private Func<float, object> interpolate;
    //...
    public Action<object> Convert<T>(Action<T> myActionT)
    {
      if (myActionT == null) return null;
      else return new Action<object>(o => myActionT((T)o));
    }


    private void UpdateSetter()
    {
      if (Target != null && Property != null)
      {
        PropertyInfo pi = Target.GetType().GetProperty(Property);
        if (pi == null)
        {
          CUI.Warning($"CUIAnimation couldn't find {Property} in {Target}");
          return;
        }

        propertyType = pi.PropertyType;

        interpolate = customInterpolate ?? ((l) => CUIInterpolate.Interpolate[propertyType].Invoke(StartValue, EndValue, l));


        // https://coub.com/view/1mast0
        if (propertyType == typeof(float))
        {
          setter = Convert<float>(pi.GetSetMethod()?.CreateDelegate<Action<float>>(Target));
        }

        if (propertyType == typeof(Color))
        {
          setter = Convert<Color>(pi.GetSetMethod()?.CreateDelegate<Action<Color>>(Target));
        }
      }
    }


    /// <summary>
    /// Resumes animation in the same direction
    /// </summary>
    public void Start() => Active = true;
    public void Stop()
    {
      Active = false;
      OnStop?.Invoke(Direction);
    }
    /// <summary>
    /// Set Direction to Straight and Start
    /// </summary>
    public void Forward()
    {
      Direction = CUIDirection.Straight;
      Active = true;
    }
    /// <summary>
    /// Set Direction to Reverse and Start
    /// </summary>
    public void Back()
    {
      Direction = CUIDirection.Reverse;
      Active = true;
    }

    /// <summary>
    /// Set Lambda to 0
    /// </summary>
    public void SetToStart() => Lambda = StartLambda;
    /// <summary>
    /// Set Lambda to 1
    /// </summary>
    public void SetToEnd() => Lambda = EndLambda;


    private void UpdateState()
    {
      if (Direction == CUIDirection.Straight && Lambda >= EndLambda)
      {
        Lambda = EndLambda;
        if (Bounce) Direction = CUIDirection.Reverse;
        else Stop();
      }

      if (Direction == CUIDirection.Reverse && Lambda <= StartLambda)
      {
        Lambda = StartLambda;
        if (Bounce) Direction = CUIDirection.Straight;
        else Stop();
      }
    }

    public void ApplyValue()
    {
      if (interpolate == null) return;
      object value = interpolate.Invoke((float)Lambda);
      setter?.Invoke(value);
    }

    public void Step(double time)
    {
      UpdateState();
      ApplyValue();
      Lambda += Direction == CUIDirection.Straight ? Speed : -(BackSpeed ?? Speed);
      if (Debug) LogStatus();
    }

    public void LogStatus() => CUI.Log($"Active:{Active} Direction:{Direction} Lambda:{Lambda}");

  }
}