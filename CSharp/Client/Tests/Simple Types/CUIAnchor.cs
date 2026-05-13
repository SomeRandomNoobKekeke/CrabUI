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
    public override void CreateTests()
    {
      Tests.Add(new UTest(
        CUIAnchor.AnchorPosIn(new CUIRect(0, 0, 100, 100), new Vector2(0.5f, 0.5f)),
        new Vector2(50, 50)
      ));

      Tests.Add(new UTest(
        CUIAnchor.AnchorPosIn(new CUIRect(0, 0, 100, 100), new Vector2(2.0f, 2.0f)),
        new Vector2(200, 200)
      ));

      Tests.Add(new UTest(
        CUIAnchor.AnchorPosIn(new CUIRect(300, 300, 100, 100), new Vector2(0.5f, 0.5f)),
        new Vector2(350, 350)
      ));

      Tests.Add(new UTest(
        CUIAnchor.AnchorFromPos(new CUIRect(300, 300, 100, 100), new Vector2(350, 350)),
        new Vector2(0.5f, 0.5f)
      ));

      // Same size
      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          new Vector2(0.5f, 0.5f),
          new Vector2(100, 100)
        ),
        new Vector2(300, 300)
      ));

      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          new Vector2(0.0f, 0.0f),
          new Vector2(100, 100)
        ),
        new Vector2(300, 300)
      ));

      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          new Vector2(1.0f, 1.0f),
          new Vector2(100, 100)
        ),
        new Vector2(300, 300)
      ));

      // Half size
      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          new Vector2(0.0f, 0.0f),
          new Vector2(50, 50)
        ),
        new Vector2(300, 300)
      ));

      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          new Vector2(1.0f, 1.0f),
          new Vector2(50, 50)
        ),
        new Vector2(350, 350)
      ));

      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          new Vector2(1.0f, 1.0f),
          new Vector2(50, 50)
        ),
        new Vector2(350, 350)
      ));

      // Double anchors
      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          CUIAnchor.LeftTop,
          new Vector2(50, 50),
          CUIAnchor.RightCenter
        ),
        new Vector2(250, 275)
      ));

      Tests.Add(new UTest(
        CUIAnchor.ChildPosIn(
          new CUIRect(300, 300, 100, 100),
          CUIAnchor.RightBottom,
          new Vector2(50, 50),
          CUIAnchor.LeftCenter
        ),
        new Vector2(400, 375)
      ));
    }
  }
}