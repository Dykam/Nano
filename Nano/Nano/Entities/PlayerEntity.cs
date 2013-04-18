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
			: base(20, 3, 10)
		{
            Texture = texture;
            currentLevel = this.ParentObject as Level;
			DNA.Add(shockWave = new ShockWave());
		}

        public override void HandleInput(Engine.InputHelper inputHelper, GameTime gameTime)
        {
            if (inputHelper.IsKeyDown(Keys.A))
                Transform.LocalPosition.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inputHelper.IsKeyDown(Keys.D))
				Transform.LocalPosition.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inputHelper.IsKeyDown(Keys.W))
				Transform.LocalPosition.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inputHelper.IsKeyDown(Keys.S))
				Transform.LocalPosition.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (inputHelper.MouseLeftButtonPressed()) {

			}
			if (inputHelper.IsKeyDown(Keys.Space)) {
				DNA.ActivateSkill(shockWave, this, Vector2.Zero);
			}

            base.HandleInput(inputHelper, gameTime);
        }
	}
}
