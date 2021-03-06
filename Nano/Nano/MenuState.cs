﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine.GameObjects;

namespace Nano
{
	class MenuState : GameState
	{
		NanoGame nanoGame;
		TileSheet tileSheet;
		GameObjectList root;
        Texture2D splash;
        SpriteFont font;

		public MenuState(NanoGame nanoGame)
		{
			this.nanoGame = nanoGame;
			tileSheet = new TileSheet(nanoGame.Content.Load<Texture2D>("MenuSheet"), 128);
            font = NanoGame.Engine.ResourceManager.GetFont("Fonts/Tutorial");
            splash = NanoGame.Engine.ResourceManager.GetSprite("Sprites/splash");

			Button button = null;
			root = new GameObjectList("menu", true) {
				(button = new Button(tileSheet, 3, 3, () => NanoGame.Engine.GameStateManager.SetCurrentState("play")) {
					Inner = Color.White,
					Border = Color.Red
				})
			};
		}

		public override void Enable()
		{
			nanoGame.IsMouseVisible = true;
		}

		public override void Reset()
		{
			root.Clear();
		}

		public override void Update(GameTime gameTime)
		{
			root.Update(gameTime);
			root.HandleInput(NanoGame.Engine.InputHelper, gameTime);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Begin();
            spriteBatch.Draw(splash, Vector2.Zero, Color.White);
            spriteBatch.End();
			root.Draw(spriteBatch, Matrix.Identity);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Play!", new Vector2(50, 100), Color.Red);
            spriteBatch.End();
		}

	}
}
