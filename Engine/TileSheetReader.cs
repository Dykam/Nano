using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class TileSheetReader
    {
        Texture2D input;
        SpriteBatch spriteBatch;
        int rows, cols, tileSize;
        string name; //how the tiles will be saved
        public static bool IsRenderingTile;

        public TileSheetReader(SpriteBatch spriteBatch, Texture2D inputTexture, int rows, int cols, int tile_size, string name)
        {
            this.name = name;
            this.spriteBatch = spriteBatch;
            this.input = inputTexture;
            this.cols = cols;
            this.rows = rows;
            this.tileSize = tile_size;
        }

        public Texture2D GetTile(int index, bool useGPU = false)
        {
            if (useGPU)
                return GetTileUsingGPU(index);

            return GetTileUsingCPU(index);            
        }

        private Texture2D GetTileUsingCPU(int index)
        {
            IsRenderingTile = true;

            
            Rectangle sourceRect = new Rectangle(tileSize * (index % cols), tileSize * (index / rows), tileSize, tileSize);
            Texture2D returnValue = new Texture2D(spriteBatch.GraphicsDevice, sourceRect.Width, sourceRect.Height);

            Color[] data = new Color[sourceRect.Width * sourceRect.Height];
            input.GetData(0, sourceRect, data, 0, data.Count());

            returnValue.SetData(data);

            IsRenderingTile = false;
            return returnValue;
        }

        private Texture2D GetTileUsingGPU(int index)
        {
            //set it to true, telling the engine we're busy!
            IsRenderingTile = true;

            //work it out
            Rectangle sourceRect = new Rectangle(tileSize * (index % cols), tileSize * (index / rows), tileSize, tileSize);
            RenderTarget2D rt = new RenderTarget2D(spriteBatch.GraphicsDevice, tileSize, tileSize);
            spriteBatch.GraphicsDevice.SetRenderTarget(rt);
            spriteBatch.Begin();
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Draw(input, Vector2.Zero, sourceRect, Color.White);
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SetRenderTarget(null);

            //set it to false again, so the engine may continue
            IsRenderingTile = false;
            return (Texture2D)rt;
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}
