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
				(button = new Button(tileSheet, 3, 3, () => nanoGame.Engine.GameStateManager.SetCurrentState("play")) {
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
			root.HandleInput(nanoGame.Engine.InputHelper);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			root.Draw(spriteBatch, false);
		}

		class Button : GameObject
		{
			public TileSheet Tiles { get; set; }
			public int Width { get; set; }
			public int Height { get; set; }
			public Action Click { get; set; }
			public Color Border { get; set; }
			public Color Inner { get; set; }
			public Button(TileSheet tiles, int width, int height, Action click)
				: base("button")
			{
				Tiles = tiles;
				Width = width;
				Height = height;
				Click = click;
			}

			public override void HandleInput(InputHelper inputHelper)
			{
				if (!inputHelper.MouseLeftButtonPressed())
					return;

				if (!BoundingBox.Contains(new Point((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y)))
					return;
				Click();
			}

			public override Rectangle BoundingBox
			{
				get
				{
					return new Rectangle((int)position.X, (int)position.Y, Width * Tiles.TileSize / 2, Height * Tiles.TileSize / 2);
				}
			}

			public override void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
			{
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				for (int y = 0; y < 3; y++) {
					for (int x = 0; x < 3; x++) {
						spriteBatch.Draw(Tiles, new Vector2(x, y) * 64 + GlobalPosition, x + 3, y, Inner, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
						spriteBatch.Draw(Tiles, new Vector2(x, y) * 64 + GlobalPosition, x, y, Border, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
					}
				}
				spriteBatch.End();
			}
		}
	}
}
