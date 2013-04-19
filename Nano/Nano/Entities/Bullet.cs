using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.GameObjects;
using Engine;
using Nano.World.LevelTiles;
namespace Nano.Entities
{
	class Bullet : Entity
	{
		Vector2 target, direction;
		float velocity;
		private RectangleF boundingBox;

		public Bullet(Vector2 target, Vector2 startPos)
			: base()
		{
			Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/bulletTexture");
			Transform.LocalScale = new Vector2(0.3f, 0.3f);
			this.target = target;
			Transform.Position = startPos;

			velocity = 10f;
			direction = target - startPos;
			direction.Normalize();
		}

		public override void Update(GameTime gameTime)
		{
			Transform.LocalPosition += direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			CheckCollision(gameTime);
		}

		private void CheckCollision(GameTime gameTime)
		{
			GameObject hit = null;
			var tile = State.Level.Map[(int)Transform.LocalPosition.X, (int)Transform.LocalPosition.Y];
			if (!tile.IsWalkableBy(this))
				hit = tile.LevelEntity;
			if(hit == null)
				hit = State.Level.Entities.EntitiesInDistance.FirstOrDefault(w => !(w is PlayerEntity) && !(w is Bullet) && !(w is  Wall) && w.BoundingBox.Contains(Transform.LocalPosition));
			if (hit == null)
				return;

			if (hit is LivingEntity)
				((LivingEntity)hit).Damage(5);
			Destroy();

		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			transform = transform * Matrix.CreateTranslation(new Vector3(Texture.Width, Texture.Height, 0) / -4 * new Vector3(Transform.LocalScale, 0));
			spriteBatch.Draw(Texture, new Color(1f, 1f, 1f, Opacity), Transform, transform);
		}

		private void Destroy()
		{
			State.Level.Entities.Remove(this);
		}
	}
}
