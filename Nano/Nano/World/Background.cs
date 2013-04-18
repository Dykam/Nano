using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Nano.World
{
	class Background : GameObject
	{
		public Background()
			: base()
		{
			Texture = NanoGame.Engine.ResourceManager.GetSprite("sprites/Background");
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			Rectangle rect = new Rectangle(0, 0, 1000000, 1000000);
			var pos = Transform.Position - new Vector2(100, 100);
			pos = Vector2.Transform(pos, transform);
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone);
			spriteBatch.Draw(
				Texture,
				pos,
				rect,
				new Color(1f, 1f, 1f, Opacity),
				0,
				Vector2.Zero,
				Transform.Scale,
				SpriteEffects.None,
				0);
			spriteBatch.End();
		}
	}
}
