using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nano.World;
using Nano.Entities.Effects;
using Nano.World.LevelTiles;
using Nano.Entities.Enemies;
using Engine;

namespace Nano.Entities
{
	class PlayerEntity : LivingEntity
	{
		Level currentLevel;
		ShockWave shockWave;
		List<Bullet> bullets;
		Cooldown bulletCooldown, damageCooldown;
		Vector2? spawn;
		int lives = 3;
		SpriteFont font;

		public PlayerEntity(Texture2D texture)
			: base(20, 7, 10)
		{
			Texture = texture;
			currentLevel = this.ParentObject as Level;
			DNA.Add(shockWave = new ShockWave());
			bullets = new List<Bullet>();
			RegenLoop(1, 1);
			Transform.LocalScale *= 0.9f;
			bulletCooldown = new Cooldown(0.2);
			damageCooldown = new Cooldown(0.2);
			font = NanoGame.Engine.ResourceManager.GetFont("fonts/storyFont");
		}

		public override void HandleInput(Engine.InputHelper inputHelper, GameTime gameTime)
		{
			Vector2 move = Vector2.Zero;
			if (inputHelper.IsKeyDown(Keys.A))
				move.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (inputHelper.IsKeyDown(Keys.D))
				move.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (inputHelper.IsKeyDown(Keys.W))
				move.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (inputHelper.IsKeyDown(Keys.S))
				move.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			for (float i = 1; i > 1f / 8; i /= 2) {
				Transform.LocalPosition.X += move.X * i;
				if (!touchedTiles.Any(tile => collides(tile)))
					break;
				Transform.LocalPosition.X -= move.X * i;
			}
			for (float i = 1; i > 1f / 8; i /= 2) {
				Transform.LocalPosition.Y += move.Y * i;
				if (!touchedTiles.Any(tile => collides(tile)))
					break;
				Transform.LocalPosition.Y -= move.Y * i;
			}
			var correction = 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (move.Y == 0) {
				float diff = (float)Math.Round(Transform.LocalPosition.Y) - Transform.LocalPosition.Y;
				Transform.LocalPosition.Y += Math.Min(Math.Abs(diff), Math.Abs(move.X)) / 5 * Math.Sign(diff);
			}
			if (move.X == 0) {
				float diff = (float)Math.Round(Transform.LocalPosition.X) - Transform.LocalPosition.X;
				Transform.LocalPosition.X += Math.Min(Math.Abs(diff), Math.Abs(move.Y)) / 5 * Math.Sign(diff);
			}

			if (inputHelper.MouseState.LeftButton == ButtonState.Pressed && bulletCooldown.TryTick()) {
				Bullet b = new Bullet(this, State.MouseLocation, Transform.LocalPosition + new Vector2(BoundingBox.Width, BoundingBox.Height) / 2);
				State.Level.Entities.Add(b);
			}

			if (inputHelper.IsKeyDown(Keys.Space)) {
				DNA.ActivateSkill(shockWave, this, Vector2.Zero);
			}

			foreach (Tile touched in touchedTiles) {
				if (touched.LevelEntity is StoryCheckpoint) {
					((StoryCheckpoint)touched.LevelEntity).Passed = true;
					foreach (Entity e in State.Level.Entities) {
						if (e is StoryCheckpoint && e.ID == ((StoryCheckpoint)touched.LevelEntity).ID) {
							(e as StoryCheckpoint).Passed = true;
						}
					}
				} else if (touched.LevelEntity is SwitchLevel) {
					if (!State.Level.Entities.OfType<White>().Any(w => w.Essential)) {
						NanoGame.PlayState.Reset(((SwitchLevel)touched.LevelEntity).Level);
					}
				}
			}

			if (inputHelper.KeyPressed(Keys.C) && inputHelper.KeyPressed(Keys.H) && inputHelper.KeyPressed(Keys.LeftShift)) {
				var switcher = State.Level.Entities.OfType<SwitchLevel>().FirstOrDefault();
				if (switcher != null)
					State.Reset(switcher.Level);
			}

			base.HandleInput(inputHelper, gameTime);
		}

		public override void Update(GameTime gameTime)
		{
			if (spawn == null)
				spawn = Transform.LocalPosition;
			base.Update(gameTime);
		}

		public override void Die()
		{
			if (lives-- > 0) {
				Transform.LocalPosition = spawn.Value;
				Heal(MaxHealth);
			} else {
				base.Die();
				State.Reset();
			}
		}

		public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			base.Draw(spriteBatch, transform);
			Transform.LocalScale *= 4;
			Transform.LocalPosition += Vector2.One * 0.1f;
			spriteBatch.DrawString(font, "+" + (lives - 1).ToString(), Color.Green, Transform, transform);
			Transform.LocalPosition -= Vector2.One * 0.1f;
			Transform.LocalScale /= 4;
		}

		public override void Damage(float hp)
		{
			if (damageCooldown.TryTick())
				base.Damage(hp);
		}

		/// <summary>
		/// Starts constant regeneration.
		/// </summary>
		/// <param name="delay">Delay between each boost in seconds</param>
		/// <param name="amount">Amount of healing</param>
		void RegenLoop(float delay, float amount)
		{
			Heal(amount);
			NanoGame.Awaiter.Delay(delay * 1000).ContinueWith(t => RegenLoop(delay, amount), System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
		}

		IEnumerable<Tile> touchedTiles
		{
			get
			{
				yield return State.Level.Map[(int)BoundingBox.X, (int)BoundingBox.Y];
				yield return State.Level.Map[(int)(BoundingBox.X + BoundingBox.Width), (int)BoundingBox.Y];
				yield return State.Level.Map[(int)BoundingBox.X, (int)(BoundingBox.Y + BoundingBox.Height)];
				yield return State.Level.Map[(int)(BoundingBox.X + BoundingBox.Width), (int)(BoundingBox.Y + BoundingBox.Height)];
			}
		}

		bool collides(Tile tile)
		{
			if (tile.LevelEntity != null && tile.LevelEntity.Solid)
				return Collision.Intersects(new Circle { Position = Transform.Position, Radius = .5f * Transform.LocalScale.X }, tile.LevelEntity.BoundingBox);
			return !tile.IsWalkableBy(this);
		}
	}
}
