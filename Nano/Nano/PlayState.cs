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
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

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
		public Matrix GameToScreenUnits { get; private set; }
		public Matrix ScreenToGameUnits { get; private set; }
		bool reset = false;
		string levelToResetTo;
		bool updating = false;
		TutorialBox tutorialBox = new TutorialBox();
		Vector2 scale = Vector2.One * 0.3f;
		SoundEffectInstance backgroundSound;

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

			entities = new EntityManager(30, 30, null, true);
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
			root.Transform.LocalScale *= scale;

			if (backgroundSound != null)
				backgroundSound.Stop();
			if (Level.BackgroundSound != null) {
				backgroundSound = Level.BackgroundSound.CreateInstance();
				backgroundSound.Play();
			}
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
			scale = Vector2.One * (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds / 2.7) * 0.02f
				+ new Vector2((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds / 3), (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds / 2.58)) * 0.01f
				+ Vector2.One * 0.3f;
			root.Transform.LocalScale = scale;
		}

        private void UpdateCamera(GameTime gameTime)
		{
			Vector2 voffset = new Vector2((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds / 2.547), (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds / 3.43)) * .5f;
			// Calculate the desired camera position, interpolate it with the current position, and clamp if it spaces too far apart.
			var bb = Player.BoundingBox;
			Vector2 desiredCameraOffset = -Player.Transform.Position + voffset;
			CameraOffset = Vector2.Lerp(CameraOffset, desiredCameraOffset, (float)(.999 * gameTime.ElapsedGameTime.TotalSeconds));

			var offset = (NanoGame.Engine.Screen - new Vector2(Player.Texture.Width, Player.Texture.Height) / 2) / 2;
			GameToScreenUnits = Matrix.Identity
				* Matrix.CreateTranslation(CameraOffset.X, CameraOffset.Y, 0)
				* Matrix.CreateScale(128, 128, 1)
				* Matrix.CreateScale(new Vector3(scale, 1))
				* Matrix.CreateTranslation(offset.X, offset.Y, 1);
			ScreenToGameUnits = Matrix.Invert(GameToScreenUnits);

			var screenRatio = new Vector2(NanoGame.Engine.ScreenRect.Width, NanoGame.Engine.ScreenRect.Height) / NanoGame.Engine.ScreenRect.Width;
			CameraOffset = Vector2.Clamp(CameraOffset, desiredCameraOffset - screenRatio * 3, desiredCameraOffset + screenRatio * 3);
			//CameraOffset = desiredCameraOffset;
        }

		public override void Draw(SpriteBatch spriteBatch) {
root.Draw(spriteBatch, GameToScreenUnits);
		}

		public Vector2 MouseLocation
		{
			get
			{
				return Vector2.Transform(NanoGame.Engine.InputHelper.MousePosition, ScreenToGameUnits);
			}
		}
	}
}
