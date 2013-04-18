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
        int speed;
		public PlayerEntity(Texture2D texture)
		{
            Texture = texture;
			Health = MaxHealth = 20;
            speed = 5;
		}

        public override void HandleInput(Engine.InputHelper inputHelper, GameTime gameTime)
        {
            if (inputHelper.IsKeyDown(Keys.A))
                Transform.LocalPosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inputHelper.IsKeyDown(Keys.D))
				Transform.LocalPosition.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inputHelper.IsKeyDown(Keys.W))
				Transform.LocalPosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inputHelper.IsKeyDown(Keys.S))
				Transform.LocalPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.HandleInput(inputHelper, gameTime);
        }

	}
}
