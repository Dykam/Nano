using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Engine.GameObjects
{
    public abstract class GameObject
    {
        protected string id;
        protected GameObject parentObject;
        protected Vector2 position, velocity;
        
        public string ID
        {
            get { return this.id; }
        }

		public GameObject(string id)
		{
			this.id = id ?? GetType().Name;
		}
		public GameObject()
			: this(null)
		{
		}

        public virtual void HandleInput(InputHelper inputHelper)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            this.position += this.velocity * gameTime.ElapsedGameTime.Milliseconds / 10;
        }

        public virtual void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
        {
        }

        public virtual Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public virtual Vector2 GlobalPosition
        {
            get
            {
                if (parentObject != null)
                    return parentObject.GlobalPosition + this.Position;
                else
                    return this.Position;
            }
        }

        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(GlobalPosition.X - Camera.CameraPos.X), (int)(GlobalPosition.Y - Camera.CameraPos.Y), 0, 0);
            }
        }

        public GameObject ParentObject
        {
            get { return parentObject; }
            set { parentObject = value; }
        }


    }
}
