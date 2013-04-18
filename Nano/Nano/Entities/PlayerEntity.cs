using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Nano.Entities
{
	class PlayerEntity : LivingEntity
	{
		public PlayerEntity(Texture2D texture)
		{
            Texture = texture;
			Health = MaxHealth = 20;
            Speed = 5;
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

            base.HandleInput(inputHelper, gameTime);
        }

	}
}
