using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Entities.Effects
{
	abstract class FadeEffect : EffectEntity
	{
		/// <summary>
		/// Velocity in tiles/s
		/// </summary>
		Vector2 velocity;
		float durationLeft, duration, fadeDelay;

		public FadeEffect(Vector2 movement, float duration, float fadeDelay = 0)
		{
			this.duration = this.durationLeft = duration / 1000;
			this.fadeDelay = fadeDelay / 1000;
			velocity = movement / this.durationLeft;
		}
		public override void Update(GameTime gameTime)
		{
			durationLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			Transform.LocalPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			Opacity = Math.Min(1, durationLeft / (duration - fadeDelay));
			if (durationLeft < 0)
				Manager.Finish(this);
			base.Update(gameTime);
		}
	}
}
