using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Engine.GameObjects
{
    public class SpriteGameObject : GameObject
    {
        protected Texture2D texture;
        private Vector2 drawPosition, origin; //where this SPO is drawn
        protected Color drawColor;
        float scale;
        
        //Pulse Effect vars
        public bool PulseEffect;
        bool isGrowing;

        public SpriteGameObject(string id, Texture2D texture)
            : base(id)
        {
            this.texture = texture;
            drawColor = Color.White;
            scale = 1f;
            origin = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            if (PulseEffect)
                HandlePulse();

            base.Update(gameTime);
        }


        private void HandlePulse()
        {
            origin = new Vector2(25, 7);
            if (isGrowing && scale < 2)
            {
                scale += 0.025f;
                if (scale >= 2f)
                {
                    isGrowing = false;
                }
            }
            else if (scale > 0.5f && !isGrowing)
            {
                scale -= 0.025f;
                if (scale <= 0.5f)
                {
                    isGrowing = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
        {
            drawPosition = this.GlobalPosition - Camera.CameraPos;
            if (isGridObject)
                drawPosition = this.GlobalPosition * 32 - Camera.CameraPos;

            spriteBatch.Draw(texture, drawPosition, new Rectangle(0,0,texture.Width,texture.Height),drawColor,0f,origin,scale,SpriteEffects.None,1f); //draws the sprite on its position minus the cameraposition
        }

        public override Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)this.drawPosition.X, (int)this.drawPosition.Y, texture.Width, texture.Height);
            }
        }

        public bool Clicked(InputHelper iH)
        {
            return (iH.MouseLeftButtonPressed() && (this.BoundingBox.Contains((int)iH.MousePosition.X, (int)iH.MousePosition.Y)));
        }

        public bool MouseOver(InputHelper iH)
        {
            return (this.BoundingBox.Contains((int)iH.MousePosition.X, (int)iH.MousePosition.Y));
        }
    }
}
