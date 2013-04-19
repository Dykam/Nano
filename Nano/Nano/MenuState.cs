using System;
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

		public MenuState(NanoGame nanoGame)
		{
			this.nanoGame = nanoGame;
			tileSheet = new TileSheet(nanoGame.Content.Load<Texture2D>("MenuSheet"), 128);
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
			root.Draw(spriteBatch, Matrix.Identity);
		}

	}
}
