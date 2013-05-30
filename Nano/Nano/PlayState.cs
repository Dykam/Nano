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
		public InterfaceManager Interface { get; private set; }
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
		Matrix gameToScreenUnits, screenToGameUnits;
		bool reset = false;
		string levelToResetTo;
		bool updating = false;
		TutorialBox tutorialBox = new TutorialBox();

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
			Reset(Level != null ? Level.Name : "Level1");
		}
		public void Reset(string level)
		{
			if (updating) {
				levelToResetTo = level;
				reset = true;
				return;
			}
			reset = false;

			entities = new EntityManager(20, 20, null, true);
			Level = loader.Load(level, entities);
			root = new GameObjectList("play", true) {
				new Background(),
				Level,
				(Effects = new EffectManager()),
				(Interface = new InterfaceManager("interface", true) {
					new CrossHair(uisheet, 0, 0),
					tutorialBox
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
			updating = true;
			root.Update(gameTime);
			root.HandleInput(NanoGame.Engine.InputHelper, gameTime);
			UpdateCamera(gameTime);
			updating = false;
			if (reset)
				Reset(levelToResetTo);
		}

        private void UpdateCamera(GameTime gameTime)
		{
			// Calculate the desired camera position, interpolate it with the current position, and clamp if it spaces too far apart.
			var bb = Player.BoundingBox;
			Vector2 desiredCameraOffset = -Player.Transform.Position;
			CameraOffset = Vector2.Lerp(CameraOffset, desiredCameraOffset, (float)(.999 * gameTime.ElapsedGameTime.TotalSeconds));

			var offset = (NanoGame.Engine.Screen - new Vector2(Player.Texture.Width, Player.Texture.Height) / 2) / 2;
			gameToScreenUnits = Matrix.Identity
				* Matrix.CreateTranslation(CameraOffset.X, CameraOffset.Y, 0)
				* Matrix.CreateScale(128, 128, 1)
				* Matrix.CreateScale(0.5f)
				* Matrix.CreateTranslation(offset.X, offset.Y, 1);
			screenToGameUnits = Matrix.Invert(gameToScreenUnits);

			var screenRatio = new Vector2(NanoGame.Engine.ScreenRect.Width, NanoGame.Engine.ScreenRect.Height) / NanoGame.Engine.ScreenRect.Width;
			CameraOffset = Vector2.Clamp(CameraOffset, desiredCameraOffset - screenRatio * 3, desiredCameraOffset + screenRatio * 3);
			//CameraOffset = desiredCameraOffset;
        }

		public override void Draw(SpriteBatch spriteBatch) {

				root.Draw(spriteBatch, gameToScreenUnits);
		}

		public Vector2 MouseLocation
		{
			get
			{
				return Vector2.Transform(NanoGame.Engine.InputHelper.MousePosition, screenToGameUnits);
			}
		}
	}
}
