using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CursedUIUser
{
  public partial class E2ETestPack
  {
    public partial class RadialMenu : IE2ETest
    {
      public partial class RadialMenuUI : CUIComponent
      {
        public CUIComponent[] Parts { get; } = new CUIComponent[4];

        public void ExecuteCommand(string command)
        {
          CUI.Logger.Log(command);
          RemoveSelf();
        }

        public RadialMenuUI()
        {
          Absolute = new CUINullRect(w: 400, h: 400);
          Anchor = CUIAnchor.Center;
          // Background.Color = Color.Pink;

          for (int i = 0; i < Parts.Length; i++)
          {
            CUIComponent part = Parts[i] = new CUIComponent()
            {
              Relative = CUINullRect.One,
              Background = {
                Sprite = CUISprite.Get($"Assets\\PNG\\For testing\\Radial menu\\{i + 1}.png"),
                Color = Color.Gray,
              },
              ConsumeMouseEvents = true,
              IgnoretransparentPixels = true,
            };

            TypedAnimation<Color> animation = new TypedAnimation<Color>()
            {
              Duration = 1.0,
              StartValue = Color.Gray,
              EndValue = Color.White,
              OnChanged = (cl) => part.Background.Color = cl,
            };

            part.MouseEnter += (e) => animation.RunForward();
            part.MouseLeave += (e) => animation.RunBackward();

            int bruh = i + 1;
            part.MouseDown += (e) => ExecuteCommand($"run script {bruh}");

            this[$"part {i + 1}"] = part;
          }


        }
      }

    }
  }
}