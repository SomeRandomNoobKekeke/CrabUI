using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public class CUIAnchorTest : UTestPack
  {
    public List<UTest> PosFromAnchor()
    {
      return new List<UTest>()
      {
        new UTest(
          CUIAnchor.PosFromAnchor(new CUIRect(0, 0, 100, 100), new Vector2(0.5f, 0.5f)),
          new Vector2(50, 50)
        ),
        new UTest(
          CUIAnchor.PosFromAnchor(new CUIRect(0, 0, 100, 100), new Vector2(2.0f, 2.0f)),
          new Vector2(200, 200)
        ),
        new UTest(
          CUIAnchor.PosFromAnchor(new CUIRect(300, 300, 100, 100), new Vector2(0.5f, 0.5f)),
          new Vector2(350, 350)
        )
      };
    }

    public List<UTest> AnchorFromPos()
    {
      return new List<UTest>()
      {
        new UTest(
          CUIAnchor.AnchorFromPos(new CUIRect(300, 300, 100, 100), new Vector2(350, 350)),
          new Vector2(0.5f, 0.5f)
        )
      };
    }

    public List<UTest> ChildPosIn()
    {
      return new List<UTest>()
      {
        // Same size
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            new Vector2(0.5f, 0.5f),
            new Vector2(100, 100)
          ),
          new Vector2(300, 300)
        ),
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            new Vector2(0.0f, 0.0f),
            new Vector2(100, 100)
          ),
          new Vector2(300, 300)
        ),
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            new Vector2(1.0f, 1.0f),
            new Vector2(100, 100)
          ),
          new Vector2(300, 300)
        ),
        // Half size
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            new Vector2(0.0f, 0.0f),
            new Vector2(50, 50)
          ),
          new Vector2(300, 300)
        ),
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            new Vector2(1.0f, 1.0f),
            new Vector2(50, 50)
          ),
          new Vector2(350, 350)
        ),
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            new Vector2(1.0f, 1.0f),
            new Vector2(50, 50)
          ),
          new Vector2(350, 350)
        ),
        // Double anchors
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            CUIAnchor.LeftTop,
            new Vector2(50, 50),
            CUIAnchor.RightCenter
          ),
          new Vector2(250, 275)
        ),
        new UTest(
          CUIAnchor.ChildPosIn(
            new CUIRect(300, 300, 100, 100),
            CUIAnchor.RightBottom,
            new Vector2(50, 50),
            CUIAnchor.LeftCenter
          ),
          new Vector2(400, 375)
        )
      };
    }

    public List<UTest> RectFrom2PointsWithAnchors()
    {
      return new List<UTest>()
      {
        new UTest(
          CUIAnchor.RectFrom2PointsWithAnchors(
            new Vector2(100,100), new Vector2(0,0),
            new Vector2(200,200), new Vector2(1,1)
          ),
          new CUIRect(100,100,100,100)
        ),
        new UTest(
          CUIAnchor.RectFrom2PointsWithAnchors(
            new Vector2(200,100), new Vector2(1,0),
            new Vector2(100,200), new Vector2(0,1)
          ),
          new CUIRect(100,100,100,100)
        ),
        new UTest(
          CUIAnchor.RectFrom2PointsWithAnchors(
            new Vector2(125,175), new Vector2(0.25f, 0.75f),
            new Vector2(175,125), new Vector2(0.75f, 0.25f)
          ),
          new CUIRect(100,100,100,100)
        ),
      };
    }


  }
}