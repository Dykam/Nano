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
		Texture2D pixel;
        string text;

        public StoryNarrative(string text)
        {
            font = NanoGame.Engine.ResourceManager.GetFont("fonts/storyFont");
			pixel = NanoGame.Engine.ResourceManager.GetSprite("Sprites/Pixel");
            this.text = text;
        }

        public override void Update(GameTime gameTime)
        {
            NanoGame.Awaiter.Delay(5000).ContinueWith(t => NanoGame.PlayState.Interface.Remove(this), System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			var drawedTextSize = font.MeasureString(text);
			spriteBatch.Draw(pixel, new Vector2(0, NanoGame.Engine.Screen.Y - 30), null, new Color(1f, 1f, 1f, 0.8f), 0, Vector2.Zero, drawedTextSize, SpriteEffects.None, 0);
			spriteBatch.DrawString(font, text, new Vector2(0, NanoGame.Engine.Screen.Y - 30), Color.Black);
            base.Draw(spriteBatch, transform);
        }
    }
}
