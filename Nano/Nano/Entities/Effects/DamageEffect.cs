using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine;
using System.Globalization;

namespace Nano.Entities.Effects
{
	class DamageEffect : FadeEffect
	{
		string text;
		public DamageEffect(float hp)
			: base(Vector2.UnitY * -1, 2000, 1000)
		{
			text = hp.ToString("-0.#", CultureInfo.InvariantCulture);
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			spriteBatch.DrawString(NanoGame.DamageFont, text, new Color(1f, 0f, 0f, Opacity), Transform, transform);
		}
	}
}
