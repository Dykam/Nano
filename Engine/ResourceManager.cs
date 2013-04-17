using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*
 * ResouceManager usage:
 * 
 * //create instance
 *  
 * ResourceManager rManager = new ResourceManager(Content, "path to spites, may be empty");
 * 
 * //get sprites from it
 * 
 * Texture2D epicTexture = rManager.GetSprite("epictexture_omg_awesome.png");
 * 
 */


namespace Engine
{
    public class ResourceManager
    {

        GameEngine engine;
        StreamReader streamReader;
        SpriteBatch spriteBatch;
        ContentManager Content;
        TileSheetReader tileSheetReader;
        Dictionary<string, Texture2D> Sprites;
        Dictionary<string, SpriteFont> Fonts;
        Dictionary<string, Song> Songs;
        Dictionary<string, SoundEffect> SoundEffects;

        public ResourceManager(SpriteBatch spriteBatch, ContentManager Content, GameEngine engine, string ContentPath = "")
        {
            this.engine = engine;
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            this.Sprites = new Dictionary<string, Texture2D>();
            this.Fonts = new Dictionary<string, SpriteFont>();
            this.Songs = new Dictionary<string, Song>();
            this.SoundEffects = new Dictionary<string, SoundEffect>();
        }

        public string GetTextFromFile(string path)
        {
            streamReader = new StreamReader(path);
            string s = streamReader.ReadToEnd();
            streamReader.Close();
            return s;
        }

        public int[,] ReadGridFromFile(string path, char separatorChar, int width, int height)
        {
            streamReader = new StreamReader(path);
            string line = streamReader.ReadLine();
            int[,] data = new int[width, height];
            //loop through
            int x, y;
            x = y = 0;
            while (line != null)
            {
                string[] characters = line.Split(separatorChar);
                foreach (string s in characters)
                {
                    
                    //try placing it in the array
                    try
                    {
                        data[x, y] = int.Parse(s);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                    }
                    x++;
                }
                y++;
                x = 0;
                line = streamReader.ReadLine();
            }
            streamReader.Close();
            return data;
        }

        private void LoadSprite(string path)
        {
            if (Sprites.ContainsKey(path))
                return;

            Sprites.Add(path, Content.Load<Texture2D>(path));
        }

        public Texture2D GetSprite(string path)
        {
            if (!Sprites.ContainsKey(path))
                LoadSprite(path);

            return Sprites[path];
        }

        public void LoadSpriteSheet(string path, string name, int cols, int rows, int tileSize)
        {
            tileSheetReader = new TileSheetReader(spriteBatch, GetSprite(path), rows, cols, tileSize, name);
        }

        private void LoadSpriteFromTextureReader(int index)
        {
            if (tileSheetReader == null)
                throw new Exception("Must use LoadSpriteSheet(...) first before loading sprites from it!");

            Sprites.Add(tileSheetReader.Name + ":" + index, tileSheetReader.GetTile(index));
        }

        public Texture2D GetSpriteFromTextureReader(int index)
        {
            if (tileSheetReader == null)
                throw new Exception("Must use LoadSpriteSheet(...) first before loading sprites from it!");

            if (!Sprites.ContainsKey(tileSheetReader.Name + ":" + index))
                LoadSpriteFromTextureReader(index);

            return Sprites[tileSheetReader.Name + ":" + index];
        }

        private void LoadFont(string path)
        {
            if (Fonts.ContainsKey(path))
                return;

            Fonts.Add(path, Content.Load<SpriteFont>(path));
        }

        public SpriteFont GetFont(string path)
        {
            if (!Fonts.ContainsKey(path))
                LoadFont(path);

            return Fonts[path];
        }

        //begin sound managing.
        private void LoadSong(string path)
        {
            if (Songs.ContainsKey(path))
                return;

            Songs.Add(path, Content.Load<Song>(path));
        }

        public void PlaySong(string path, bool looped = false)
        {
            if (!Songs.ContainsKey(path))
            {
                LoadSong(path);
            }

            if (engine.MuteSound)
                return;

            MediaPlayer.Play(Songs[path]);
            MediaPlayer.IsRepeating = looped;
        }

        //begin sound managing.
        private void LoadSoundEffect(string path)
        {
            if (SoundEffects.ContainsKey(path))
                return;

            SoundEffects.Add(path, Content.Load<SoundEffect>(path));
        }

        public void PlaySoundEffect(string path)
        {
            if (!SoundEffects.ContainsKey(path))
            {
                LoadSoundEffect(path);
            }

            if (engine.MuteSound)
                return;

            SoundEffects[path].Play();
        }

        //end sound managing
    }
}
