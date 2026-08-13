using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  //TODO don't know how to name it
  public class ChainDrawerStateMachine
  {
    public record State(Rectangle ScissorRect, SamplerState SamplerState);

    public Stack<State> States { get; } = new();
    public State OriginalState { get; private set; }
    public State CurrentState { get; private set; }


    public void StopStart(CUISpriteBatch spriteBatch, State state)
    {
      if (CurrentState == state) return;
      CurrentState = state;

      spriteBatch.StopStart(state.ScissorRect, samplerState: state.SamplerState);
    }

    public void Init(CUISpriteBatch spriteBatch)
    {
      OriginalState = new State(
        CUICore.GraphicsDevice.ScissorRect,
        CUICore.SamplerState
      );

      States.Clear();
      States.Push(OriginalState);
      CurrentState = OriginalState;
    }

    public void Enter(CUISpriteBatch spriteBatch, State state)
    {
      States.Push(state);
      StopStart(spriteBatch, state);
    }

    public void Enter(CUISpriteBatch spriteBatch, VisualBounds bounds)
    {
      State newState = new State(
        bounds.ScissorRect is null ?
          CurrentState.ScissorRect :
          Rectangle.Intersect(CurrentState.ScissorRect, bounds.ScissorRect.Value),
        bounds.SamplerState is null ? CurrentState.SamplerState : bounds.SamplerState
      );

      States.Push(CurrentState);
      StopStart(spriteBatch, newState);
    }

    public void Exit(CUISpriteBatch spriteBatch, VisualBounds bounds)
    {
      State prevState = States.Pop();
      StopStart(spriteBatch, prevState);
    }

    public void Finalize(CUISpriteBatch spriteBatch)
    {
      StopStart(spriteBatch, OriginalState);
    }


  }
}