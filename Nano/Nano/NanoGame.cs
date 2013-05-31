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
using System.Threading.Tasks;
using Nano.World;

namespace Nano
{
	class NanoGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		public SpriteBatch SpriteBatch { get; set; }
		public static GameEngine Engine { get; set; }
		public static GameAwaiter Awaiter;
		public static int TileSize { get; private set; }
		public static PlayState PlayState { get; private set; }
		public static GameTime LastGameTime { get; private set; }
		public static Random Random { get; private set; }

		public static SpriteFont DamageFont { get; private set; }

		public NanoGame()
		{
			graphics = new GraphicsDeviceManager(this) {
				PreferredBackBufferHeight = 720,
				PreferredBackBufferWidth = 1280,
				PreferMultiSampling = true
			};
			Content.RootDirectory = "Content";
			Random = new Random();
		}

		protected override void Initialize()
		{
			Awaiter = new GameAwaiter(this);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			Engine = new GameEngine(this, SpriteBatch, Content, _ => { }, () => { });

			Engine.GameStateManager.AddGameState("menu", new MenuState(this));
			Engine.GameStateManager.AddGameState("play", PlayState = new PlayState(this));
			PlayState.Reset();

			Engine.GameStateManager.SetCurrentState("menu");

			DamageFont = Content.Load<SpriteFont>("Fonts/Damage");

		}
		protected override void UnloadContent()
		{
		}
#if DEBUG
		Queue<TimeSpan> updates = new Queue<TimeSpan>(), draws = new Queue<TimeSpan>();
#endif
		protected override void Update(GameTime gameTime)
		{
#if DEBUG
			updates.Enqueue(gameTime.TotalGameTime);
			while (draws.Count > 0 && updates.Peek() < gameTime.TotalGameTime - TimeSpan.FromSeconds(1))
				updates.Dequeue();
			while (draws.Count > 0 && draws.Peek() < gameTime.TotalGameTime - TimeSpan.FromSeconds(1))
				draws.Dequeue();
			Window.Title = string.Format("FPS: {0:0}; UPS: {1:0}", draws.Count, updates.Count);
#endif
			LastGameTime = gameTime;
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();
			Awaiter.Update(gameTime);
			Engine.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
#if DEBUG
			draws.Enqueue(gameTime.TotalGameTime + TimeSpan.FromSeconds(1));
#endif
			GraphicsDevice.Clear(new Color(240, 240, 240));

			Engine.Draw(SpriteBatch);
		}
	}
}
