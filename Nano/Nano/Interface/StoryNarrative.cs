using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nano;
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

        public override void Update(GameTime gameTime)
        {
            NanoGame.Awaiter.Delay(5000).ContinueWith(t => NanoGame.PlayState.Interface.Remove(this), System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix transform)
        {
            spriteBatch.DrawString(font, text, new Vector2(0, NanoGame.Engine.Screen.Y - 30), Color.Red);
            base.Draw(spriteBatch, transform);
        }
    }
}
