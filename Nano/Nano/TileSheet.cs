using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine;

namespace Nano
{
	class TileSheet
	{
		public Texture2D Texture { get; private set; }
		public int TileSize { get; private set; }
		public int Rows { get; private set; }
		public int Columns { get; private set; }
		public TileSheet(Texture2D texture, int tileSize)
		{
			Texture = texture;
			TileSize = tileSize;
			Rows = texture.Height / tileSize;
			Columns = texture.Width / tileSize;
		}
	}
	static class TileSheetExtensions
	{
		public static void Draw(this SpriteBatch spriteBatch, TileSheet texture, Rectangle destinationRectangle, int x, int y, Color color)
		{
			spriteBatch.Draw(texture.Texture, destinationRectangle, new Rectangle(texture.TileSize * x, texture.TileSize * y, texture.TileSize, texture.TileSize), color);
		}
		public static void Draw(this SpriteBatch spriteBatch, TileSheet texture, Vector2 position, int x, int y, Color color)
		{
			spriteBatch.Draw(texture.Texture, position, new Rectangle(texture.TileSize * x, texture.TileSize * y, texture.TileSize, texture.TileSize), color);
		}
		public static void Draw(this SpriteBatch spriteBatch, TileSheet texture, Rectangle destinationRectangle, int x, int y, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
		{
			spriteBatch.Draw(texture.Texture, destinationRectangle, new Rectangle(texture.TileSize * x, texture.TileSize * y, texture.TileSize, texture.TileSize), color, rotation, origin, effects, layerDepth);
		}
		public static void Draw(this SpriteBatch spriteBatch, TileSheet texture, Vector2 position, int x, int y, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			spriteBatch.Draw(texture.Texture, position, new Rectangle(texture.TileSize * x, texture.TileSize * y, texture.TileSize, texture.TileSize), color, rotation, origin, scale, effects, layerDepth);
		}
		public static void Draw(this SpriteBatch spriteBatch, TileSheet texture, Vector2 position, int x, int y, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			spriteBatch.Draw(texture.Texture, position, new Rectangle(texture.TileSize * x, texture.TileSize * y, texture.TileSize, texture.TileSize), color, rotation, origin, scale, effects, layerDepth);
		}
		public static void Draw(this SpriteBatch spriteBatch, TileSheet texture, int x, int y, Color color, Transform transform)
		{
			spriteBatch.Draw(texture.Texture, transform.Position, new Rectangle(texture.TileSize * x, texture.TileSize * y, texture.TileSize, texture.TileSize), color, 0, Vector2.Zero, transform.LocalScale, SpriteEffects.None, 0);
		}
		public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Color color, Transform transform)
		{
			spriteBatch.Draw(texture, transform.Position, null, color, 0, Vector2.Zero, transform.LocalScale, SpriteEffects.None, 0);
		}
	}
}
