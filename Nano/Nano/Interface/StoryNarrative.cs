﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nano;
using Microsoft.Xna.Framework.Audio;
namespace Nano.Interface
{
    class StoryNarrative : GameObject
    {
        SpriteFont font;
		Texture2D pixel;
        string text;
		static SoundEffect beep;

        public StoryNarrative(string text)
        {
            font = NanoGame.Engine.ResourceManager.GetFont("fonts/storyFont");
			pixel = NanoGame.Engine.ResourceManager.GetSprite("Sprites/Pixel");
            this.text = text;
			if (beep == null) {
				beep = NanoGame.PlayState.nanoGame.Content.Load<SoundEffect>("Sound/beep");
			}
			beep.Play();
        }

        public override void Update(GameTime gameTime)
        {
            NanoGame.Awaiter.Delay(5000).ContinueWith(t => NanoGame.PlayState.Interface.Remove(this), System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix transform)
		{
			var drawedTextSize = font.MeasureString(text);
			var pos = new Vector2(NanoGame.Engine.Screen.X - drawedTextSize.X, 30);
			spriteBatch.Draw(pixel, pos, null, new Color(1f, 1f, 1f, 0.8f), 0, Vector2.Zero, drawedTextSize, SpriteEffects.None, 0);
			spriteBatch.DrawString(font, text, pos, Color.Black);
            base.Draw(spriteBatch, transform);
        }

		public static void Set(InterfaceManager @interface, string text) {
			Console.WriteLine(text);
			foreach (var oldNarrative in @interface.OfType<StoryNarrative>().ToArray())
				@interface.Remove(oldNarrative);
			@interface.Add(new StoryNarrative(text));
		}
    }
}
