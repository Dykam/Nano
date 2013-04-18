using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano.Entities.Effects
{
	class ShockwaveEffect : FadeEffect
	{
		public ShockwaveEffect(float radius)
			: base(Vector2.Zero, 1000, 500)
		{
			Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/Shockwave");
			Transform.LocalScale = new Vector2(128 / Math.Max(Texture.Width, Texture.Height) * radius * 2);
			Transform.LocalPosition -= new Vector2(radius - 0.5f);
		}
	}
}
