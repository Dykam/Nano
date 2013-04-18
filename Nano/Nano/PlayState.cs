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
		Vector2 cameraOffset;
		LevelLoader loader;

		public PlayState(NanoGame nanoGame)
		{
			this.nanoGame = nanoGame;
			uisheet = new TileSheet(nanoGame.Content.Load<Texture2D>("Interface"), 128);
			loader = new LevelLoader("Content\\Levels", nanoGame.Content);
			Reset();
		}

		public override void Enable()
		{
			nanoGame.IsMouseVisible = false;
		}

		public override void Reset()
		{
			entities = new EntityManager("entities", true) {
			};
			root = new GameObjectList("play", true) {
				(level = loader.Load("Level1", entities)),
				(@interface = new InterfaceManager("interface", true) {
					new CrossHair(uisheet, 0, 0)
				}),
				// TODO: Add world
			};
			player = (PlayerEntity)root.Find<PlayerEntity>();
		}
		public override void Update(GameTime gameTime)
		{
			root.Update(gameTime);
            UpdateCamera(gameTime);
			root.HandleInput(NanoGame.Engine.InputHelper, gameTime);
		}

        private void UpdateCamera(GameTime gameTime)
        {
			var bb = player.BoundingBox;
			Vector2 desiredCameraOffset = -player.Transform.Position - Vector2.One / 2;
			cameraOffset = Vector2.Lerp(cameraOffset, desiredCameraOffset, (float)(.99 * gameTime.ElapsedGameTime.TotalSeconds));
            
			Console.WriteLine(desiredCameraOffset);
        }

		public override void Draw(SpriteBatch spriteBatch) {
		
			var offset = (NanoGame.Engine.Screen - new Vector2(player.BoundingBox.Width, player.BoundingBox.Height)) / 2;
			var transform = Matrix.Identity
				* Matrix.CreateTranslation(cameraOffset.X, cameraOffset.Y, 0)
				* Matrix.CreateScale(128, 128, 1)
				* Matrix.CreateTranslation(offset.X, offset.Y, 1);
			root.Draw(spriteBatch, transform);
		}
	}
}
