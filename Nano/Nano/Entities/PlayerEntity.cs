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
        Texture2D texture;
		public PlayerEntity(Texture2D texture)
		{
            this.texture = texture;
			Health = MaxHealth = 20;
            speed = 5;
		}

        public override void HandleInput(Engine.InputHelper inputHelper)
        {
            if (inputHelper.IsKeyDown(Keys.A))
                Transform.LocalPosition.X -= speed;
            if (inputHelper.IsKeyDown(Keys.D))
                Transform.LocalPosition.X += speed;
            if (inputHelper.IsKeyDown(Keys.W))
                Transform.LocalPosition.Y -= speed;
            if (inputHelper.IsKeyDown(Keys.S))
                Transform.LocalPosition.Y += speed;

            base.HandleInput(inputHelper);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, bool isGridObject = false)
        {
            spriteBatch.Draw(texture, Color.White, Transform);
        }

	}
}
