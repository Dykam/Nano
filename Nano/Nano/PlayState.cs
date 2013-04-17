using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Nano.Interface;
using Microsoft.Xna.Framework;
using Nano.Entities;
using Nano.World;

namespace Nano
{
	class PlayState : GameState
	{
		NanoGame nanoGame;
		GameObjectList root;
		/// <summary>
		/// Contains all GUI gameobjects
		/// </summary>
		InterfaceManager @interface;
		/// <summary>
		/// Contains all interactive gameobjects
		/// </summary>
		EntityManager entities;
		Level level;
		TileSheet uisheet;
		PlayerEntity player;

		public PlayState(NanoGame nanoGame)
		{
			this.nanoGame = nanoGame;
			uisheet = new TileSheet(nanoGame.Content.Load<Texture2D>("Interface"), 128);
			Reset();
		}

		public override void Enable()
		{
			nanoGame.IsMouseVisible = false;
		}

		public override void Reset()
		{
			entities = new EntityManager("entities", true) {
				(player = new PlayerEntity(nanoGame.Content.Load<Texture2D>("Sprites/playerTexture")))
			};
			root = new GameObjectList("play", true) {
				(level = new Level(100, 100, entities)),
				(@interface = new InterfaceManager("interface", true) {
					new CrossHair(uisheet, 0, 0)
				}),
				// TODO: Add world
			};
            player.Transform.Position = new Vector2(nanoGame.GraphicsDevice.Viewport.Width / 2, nanoGame.GraphicsDevice.Viewport.Height / 2);
			level.Transform.LocalPosition += new Vector2(100, 100);
		}
		public override void Update(GameTime gameTime)
		{
			root.Update(gameTime);
            UpdateCamera(gameTime);
			root.HandleInput(nanoGame.Engine.InputHelper);
		}

        private void UpdateCamera(GameTime gameTime)
        {
            Vector2 desiredLevelPosition = level.Transform.LocalPosition - player.Transform.Position - new Vector2(64,64) + new Vector2(nanoGame.GraphicsDevice.Viewport.Width/2,nanoGame.GraphicsDevice.Viewport.Height/2);
            level.Transform.LocalPosition = Vector2.Lerp(level.Transform.LocalPosition, desiredLevelPosition, (float)(.99 * gameTime.ElapsedGameTime.TotalSeconds));
            
            Console.WriteLine(level.Transform.LocalPosition);
        }

		public override void Draw(SpriteBatch spriteBatch)
		{
			root.Draw(spriteBatch, false);
		}
	}
}
