using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using CUILibs;
namespace CursedUI
{
  public record struct TextureSource(
    CUITexture2D Texture,
    Rectangle? SourceRectangle = null
  ) : ITextureSource;
}