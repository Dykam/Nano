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
		protected Texture2D Texture;
		protected bool HasDefaultTexture { get { return Texture != null; } }

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
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 viewingOffset)
        {
			if (HasDefaultTexture) {
				spriteBatch.Draw(Texture, Color.White, Transform, viewingOffset);
			}
        }

        public virtual Rectangle BoundingBox
        {
            get
            {
				if (!HasDefaultTexture) {
					return new Rectangle((int)(Transform.Position.X), (int)(Transform.Position.Y), 0, 0);
				} else {
					return new Rectangle((int)(Transform.Position.X), (int)(Transform.Position.Y), Texture.Width, Texture.Height);
				}
            }
        }

        public GameObject ParentObject
        {
            get { return parentObject; }
			set { parentObject = value; Transform.Parent = value == null ? null : value.Transform; }
        }


    }
}
