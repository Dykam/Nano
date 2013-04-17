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
		protected Vector2 velocity;

		public Transform Transform { get; private set; }
        
        public string ID { get { return this.id; } }

		public GameObject(string id)
		{
			this.id = id ?? GetType().Name;
			Transform = new Transform();
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
            Transform.Position += this.velocity * gameTime.ElapsedGameTime.Milliseconds / 10;
        }

        public virtual void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
        {
        }

        public virtual Rectangle BoundingBox
        {
            get
            {
				return new Rectangle((int)(Transform.Position.X - Camera.CameraPos.X), (int)(Transform.Position.Y - Camera.CameraPos.Y), 0, 0);
            }
        }

        public GameObject ParentObject
        {
            get { return parentObject; }
			set { parentObject = value; Transform.Parent = parentObject.Transform; }
        }


    }
}
