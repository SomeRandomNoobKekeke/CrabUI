using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  //TODO don't know how to name it
  public class ChainDrawerStateMachine
  {
    public DebugNode<Rectangle> Debug_ScissorRectChanged { get; } = new(
      DebugCategory.ScissorRectSet, CUI.DebugHub,
      (rect) => $"ScissorRect = {rect}"
    )
    { IsOpen = true, };

    public record State(Rectangle ScissorRect, SamplerState SamplerState);

    public Stack<State> States { get; } = new();

    public State OriginalState { get; private set; }

    public State CurrentState { get; private set; }


    public void StopStart(CUISpriteBatch spriteBatch, State state)
    {
      spriteBatch.StopStart(state.ScissorRect, state.SamplerState);
      Debug_ScissorRectChanged.Send(state.ScissorRect);
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

    public void Enter(CUISpriteBatch spriteBatch, VisualBounds bounds)
    {
      State newState = new State(
        bounds.ScissorRect is null ? CurrentState.ScissorRect : bounds.ScissorRect.Value,
        bounds.SamplerState is null ? CurrentState.SamplerState : bounds.SamplerState
      );

      if (newState != CurrentState) StopStart(spriteBatch, newState);

      CurrentState = newState;
      States.Push(CurrentState);
    }

    public void Exit(CUISpriteBatch spriteBatch, VisualBounds bounds)
    {
      State prevState = States.Pop();
      if (prevState != CurrentState) StopStart(spriteBatch, prevState);
      CurrentState = prevState;
    }

    public void Finalize(CUISpriteBatch spriteBatch)
    {
      if (CurrentState != OriginalState) StopStart(spriteBatch, OriginalState);
    }


  }
}