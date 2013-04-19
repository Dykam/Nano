using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine.GameObjects;

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

    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (!inputHelper.MouseLeftButtonPressed())
            return;

        if (!BoundingBox.Contains(new Point((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y)))
            return;
        Click();
    }

    public override RectangleF BoundingBox
    {
        get
        {
            return new RectangleF((int)Transform.Position.X, (int)Transform.Position.Y, Width * Tiles.TileSize / 2, Height * Tiles.TileSize / 2);
        }
    }

    public override void Draw(SpriteBatch spriteBatch, Matrix transform)
    {
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                spriteBatch.Draw(Tiles, new Vector2(x, y) * 64 + Transform.Position, x + 3, y, Inner, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
                spriteBatch.Draw(Tiles, new Vector2(x, y) * 64 + Transform.Position, x, y, Border, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            }
        }
        spriteBatch.End();
    }
}