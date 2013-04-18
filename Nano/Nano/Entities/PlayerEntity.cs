using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nano.World;
using Nano.Entities.Effects;
namespace Nano.Entities
{
	class PlayerEntity : LivingEntity
	{
        Level currentLevel;
		AimSkillDNA currentAimSkill;
		ShockWave shockWave;

		public PlayerEntity(Texture2D texture)
			: base(20, 5, 10)
		{
            Texture = texture;
            currentLevel = this.ParentObject as Level;
			DNA.Add(shockWave = new ShockWave());
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
				bool valid = true;
				valid = valid && State.Level.Map[(int)BoundingBox.X, (int)BoundingBox.Y].IsWalkableBy(this);
				valid = valid && State.Level.Map[(int)(BoundingBox.X + BoundingBox.Width), (int)BoundingBox.Y].IsWalkableBy(this);
				valid = valid && State.Level.Map[(int)BoundingBox.X, (int)(BoundingBox.Y + BoundingBox.Height)].IsWalkableBy(this);
				valid = valid && State.Level.Map[(int)(BoundingBox.X + BoundingBox.Width), (int)(BoundingBox.Y + BoundingBox.Height)].IsWalkableBy(this);
				if (valid)
					break;
				Transform.LocalPosition.X -= move.X * i;
			}
			for (float i = 1; i > 1f / 8; i /= 2) {
				Transform.LocalPosition.Y += move.Y * i;
				bool valid = true;
				valid = valid && State.Level.Map[(int)BoundingBox.X, (int)BoundingBox.Y].IsWalkableBy(this);
				valid = valid && State.Level.Map[(int)(BoundingBox.X + BoundingBox.Width), (int)BoundingBox.Y].IsWalkableBy(this);
				valid = valid && State.Level.Map[(int)BoundingBox.X, (int)(BoundingBox.Y + BoundingBox.Height)].IsWalkableBy(this);
				valid = valid && State.Level.Map[(int)(BoundingBox.X + BoundingBox.Width), (int)(BoundingBox.Y + BoundingBox.Height)].IsWalkableBy(this);
				if (valid)
					break;
				Transform.LocalPosition.Y -= move.Y * i;
			}

			if (inputHelper.MouseLeftButtonPressed()) {

			}
			if (inputHelper.IsKeyDown(Keys.Space)) {
				DNA.ActivateSkill(shockWave, this, Vector2.Zero);
			}

            base.HandleInput(inputHelper, gameTime);
        }
	}
}
