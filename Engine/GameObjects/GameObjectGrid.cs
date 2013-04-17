using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.GameObjects
{
    //this class needs 2 ints and a string as parameter
    //the ints specify the number of rows and columns for the grid
    //the string id gives an id to the grid
    public class GameObjectGrid : GameObject
    {
        protected GameObject[,] grid;
        protected int objectWidth, objectHeight;

        public GameObjectGrid(int rows, int columns, string id)
            : base(id)
        {
            grid = new GameObject[columns, rows];
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    grid[x, y] = null;
        }

        //adds an object to the grid on position (x,y)
        public void Add(GameObject obj, int x, int y)
        {
            grid[x, y] = obj;
            obj.ParentObject = this;
            obj.Position = new Vector2(x, y);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject obj in grid)
                if (obj != null)
                    obj.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, bool isGridObject = false)
        {
            foreach (GameObject obj in grid)
                if (obj != null)
                    obj.Draw(spriteBatch, true);
        }

        //gets the gameobject on position (x,y)
        public GameObject Get(int x, int y)
        {
            return grid[x, y];
        }

        //returns the grid
        public GameObject[,] Grid
        {
            get { return grid; }      
        }

        public int ObjectWidth
        {
            get { return objectWidth; }
            set { objectWidth = value; }
        }

        public int ObjectHeight
        {
            get { return objectHeight; }
            set { objectHeight = value; }
        }

        public int Rows
        {
            get { return grid.GetLength(1); }
        }

        public int Columns
        {
            get { return grid.GetLength(0); }
        }
    }
}
