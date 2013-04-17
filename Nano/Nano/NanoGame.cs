using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Engine;

namespace Nano
{
	public class NanoGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		public SpriteBatch SpriteBatch { get; set; }
		public GameEngine Engine { get; set; }
		public GameAwaiter Awaiter;

		public NanoGame()
		{
			graphics = new GraphicsDeviceManager(this) {
				PreferredBackBufferHeight = 720,
				PreferredBackBufferWidth = 1280
			};
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			Awaiter = new GameAwaiter(this);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			Engine = new GameEngine(this, SpriteBatch, Content, _ => {}, () => {});

			Engine.GameStateManager.AddGameState("menu", new MenuState(this));
			Engine.GameStateManager.AddGameState("play", new PlayState(this));

			Engine.GameStateManager.SetCurrentState("menu");

		}
		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();
			Awaiter.Update(gameTime);
			Engine.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(new Color(240, 240, 240));

			Engine.Draw(SpriteBatch);
		}
	}
}
