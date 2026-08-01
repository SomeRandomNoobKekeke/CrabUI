using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public partial class TextureBuilder
  {
    /// <summary>
    /// Have no idea how it works
    /// </summary>
    public TextureBuilder Damage(float aCutoff = 0.0f, float cCutoff = 0.05f)
    {
      // DamageEffect.CurrentTechnique = DamageEffect.Techniques["StencilShader"];
      CUICore.GraphicEffects.DamageEffect.Parameters["aCutoff"].SetValue(aCutoff);
      CUICore.GraphicEffects.DamageEffect.Parameters["cCutoff"].SetValue(cCutoff);
      // DamageEffect.CurrentTechnique.Passes[0].Apply();

      Redraw(effect: CUICore.GraphicEffects.DamageEffect);

      return this;
    }

    /// <summary>
    /// Have no idea how to use it, found it in legacy
    /// </summary>
    public TextureBuilder Blur(float amount = 0.002f)
    {
      CUICore.GraphicEffects.BlurEffect.SetParameters(amount, amount);
      Redraw(effect: CUICore.GraphicEffects.BlurEffect.Effect);

      CUICore.GraphicEffects.BlurEffect.SetParameters(amount, -amount);
      Redraw(effect: CUICore.GraphicEffects.BlurEffect.Effect);

      return this;
    }

  }
}
