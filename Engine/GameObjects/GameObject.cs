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
		/// <summary>
		/// Optional texture. If omitted the gameobject has to override GameObject.Draw().
		/// </summary>
		protected Texture2D Texture;
		protected float Opacity = 1;
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

        public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, Matrix transform)
        {
			if (HasDefaultTexture) {
				spriteBatch.Draw(Texture, new Color(1f, 1f, 1f, Opacity), Transform, transform);
			}
        }

        public virtual RectangleF BoundingBox
        {
            get
            {
				if (!HasDefaultTexture) {
					return new RectangleF((Transform.Position.X), (Transform.Position.Y), 0, 0);
				} else {
					return new RectangleF((Transform.Position.X), (Transform.Position.Y), Texture.Width / 128f, Texture.Height / 128f);
				}
            }
        }



		public virtual GameObject Find(string keyword)
		{
			if (keyword == id)
				return this;
			return null;
		}
		public virtual GameObject Find<T>()
		{
			if (this is T)
				return this;
			return null;
		}

        public GameObject ParentObject
        {
            get { return parentObject; }
			set { parentObject = value; Transform.Parent = value == null ? null : value.Transform; }
        }


    }
}
