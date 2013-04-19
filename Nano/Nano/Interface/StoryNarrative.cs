using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Interface
{
    class StoryNarrative : GameObject
    {
        SpriteFont font;
        string text;

        public StoryNarrative(string text)
        {
            font = NanoGame.Engine.ResourceManager.GetFont("fonts/storyFont");
            this.text = text;
        }


        public override void Draw(SpriteBatch spriteBatch, Matrix transform)
        {
            spriteBatch.DrawString(font, text, new Vector2(0, NanoGame.Engine.Screen.Y - 15), Color.Red);
            base.Draw(spriteBatch, transform);
        }
    }
}
