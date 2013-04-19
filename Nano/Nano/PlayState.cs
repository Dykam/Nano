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
using Nano.Entities.Effects;

namespace Nano
{
	class PlayState : GameState
	{
		public NanoGame nanoGame;
		GameObjectList root;
		/// <summary>
		/// Contains all GUI gameobjects
		/// </summary>
		InterfaceManager @interface;
		/// <summary>
		/// Contains all interactive gameobjects
		/// </summary>
		EntityManager entities;
		public Level Level { get; private set; }
		TileSheet uisheet;
		public PlayerEntity Player { get; private set; }
		public EffectManager Effects { get; private set; }
		public Vector2 CameraOffset { get; private set; }
		LevelLoader loader;

		public PlayState(NanoGame nanoGame)
		{
			this.nanoGame = nanoGame;
			uisheet = new TileSheet(nanoGame.Content.Load<Texture2D>("Interface"), 128);
			loader = new LevelLoader("Content\\Levels", nanoGame.Content);
			Effects = new EffectManager();
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
				new Background(),
				(Level = loader.Load("Level1", entities)),
				(Effects = new EffectManager()),
				(@interface = new InterfaceManager("interface", true) {
					new CrossHair(uisheet, 0, 0),
                    new TutorialBox()
				}),
				// TODO: Add world
			};
			Player = (PlayerEntity)root.Find<PlayerEntity>();
			CameraOffset = -Player.Transform.Position;
			Effects.FinishAll();
			root.Transform.LocalScale *= .5f;
		}
		public override void Update(GameTime gameTime)
		{
			if (!nanoGame.IsActive)
				return;
			root.Update(gameTime);
            UpdateCamera(gameTime);
			root.HandleInput(NanoGame.Engine.InputHelper, gameTime);
		}

        private void UpdateCamera(GameTime gameTime)
		{
			var bb = Player.BoundingBox;
			Vector2 desiredCameraOffset = -Player.Transform.Position;
			CameraOffset = Vector2.Lerp(CameraOffset, desiredCameraOffset, (float)(.999 * gameTime.ElapsedGameTime.TotalSeconds));

        }

		public override void Draw(SpriteBatch spriteBatch) {

			var offset = (NanoGame.Engine.Screen - new Vector2(Player.Texture.Width, Player.Texture.Height) / 2) / 2;
			var transform = Matrix.Identity
				* Matrix.CreateTranslation(CameraOffset.X, CameraOffset.Y, 0)
				* Matrix.CreateScale(128, 128, 1)
				* Matrix.CreateScale(0.5f)
				* Matrix.CreateTranslation(offset.X, offset.Y, 1);
				root.Draw(spriteBatch, transform);
		}

		public Vector2 MouseLocation
		{
			get
			{
				var offset = (NanoGame.Engine.Screen - new Vector2(Player.Texture.Width, Player.Texture.Height) / 2) / 2;
				var transform = Matrix.Identity
					* Matrix.CreateTranslation(CameraOffset.X, CameraOffset.Y, 0)
					* Matrix.CreateScale(128, 128, 1)
					* Matrix.CreateScale(0.5f)
					* Matrix.CreateTranslation(offset.X, offset.Y, 1);
				return Vector2.Transform(NanoGame.Engine.InputHelper.MousePosition, Matrix.Invert(transform));
			}
		}
	}
}
