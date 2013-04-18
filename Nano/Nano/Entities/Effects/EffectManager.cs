using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Entities.Effects
{
	// Contains all fire&forget graphics effects.
	class EffectManager : GameObject
	{
		EntityManager manager;
		public EffectManager()
		{
			manager = new EntityManager(null, true) {
				ParentObject = this,
			};
		}
		public void Start(EffectEntity effect, Entity hook, Vector2 offset = default(Vector2))
		{
			effect.ParentObject = hook;
			Start(effect, offset);
			effect.ParentObject = hook;
		}
		public void Start(EffectEntity effect, Vector2 position)
		{
			effect.Transform.LocalPosition += position;
			effect.Manager = this;
			manager.Add(effect);
		}

		public void Finish(EffectEntity effect)
		{
			manager.Remove(effect);
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			manager.Draw(spriteBatch, transform);
			base.Draw(spriteBatch, transform);
		}

		public override void Update(GameTime gameTime)
		{
			manager.Update(gameTime);
			base.Update(gameTime);
		}

		internal void FinishAll()
		{
			manager.Clear();
		}
	}
}
