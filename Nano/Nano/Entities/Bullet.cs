using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.GameObjects;

namespace Nano.Entities
{
    class Bullet : InanimateEntity
    {
        private Vector2 target, velocity, direction;
        private RectangleF boundingBox;

        public Bullet(Vector2 target, Vector2 startPos)
            : base()
        {
            Texture = NanoGame.Engine.ResourceManager.GetSprite("Sprites/bulletTexture");
            Transform.LocalScale = new Vector2(0.3f, 0.3f);
            this.target = target;
            Transform.Position = startPos;
            
            velocity = new Vector2(0.1f, 0.1f);
            direction = target - startPos;
            direction.Normalize();
        }

        public override void Update(GameTime gameTime)
        {
            Transform.Position += direction*velocity;
            CheckCollision(gameTime);
        }

        private void CheckCollision(GameTime gameTime)
        {

            foreach (Entity le in State.Level.Entities)
            {
                if (le == State.Player)
                    return;
                if (le.BoundingBox.Contains(Transform.Position))
                {
                    Destroy();
                }
                if (Transform.Position.X >= le.BoundingBox.X && Transform.Position.X <= le.BoundingBox.X + le.BoundingBox.Width && Transform.Position.Y >= le.BoundingBox.Y && Transform.Position.Y <= le.BoundingBox.Y + le.BoundingBox.Height)
                {
                    Destroy();
                    Console.WriteLine("hit");
                }
            }

        }

        private void Destroy()
        {
            State.Level.Entities.Remove(this);
        }
    }
}
