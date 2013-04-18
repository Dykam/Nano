using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nano.World;
using System.IO;
using Nano.Entities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nano.Entities.Enemies;

namespace Nano
{
	class LevelLoader
	{
		string basePath;
		ContentManager content;
		Random random = new Random();
		public LevelLoader(string basePath, ContentManager content)
		{
			this.basePath = basePath;
			this.content = content;
		}

		public Level Load(string levelName, EntityManager entities)
		{
			var path = Path.Combine(basePath, levelName + ".json");
			Texture2D mapTex;
			if (!File.Exists(path) || (mapTex = content.Load<Texture2D>("Levels/" + levelName)) == null)
				throw new ArgumentException("Level does not exist or is incomplete", "level");

			var map = new Color[mapTex.Width * mapTex.Height];
			var level = new Level(mapTex.Width, mapTex.Height, entities);
			mapTex.GetData(map);

			dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(path), new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore });
			Dictionary<Color, dynamic> colorEntities = new Dictionary<Color, dynamic>();
			foreach (var entity in json.Entities) {
				if (entity.Tile != null) {
					var color = new Color((int)entity.Tile[0], (int)entity.Tile[1], (int)entity.Tile[2]);
					colorEntities.Add(color, entity);
				} else {
					entities.Add(SpawnEntity(entity));
				}
			}

			for (var y = 0; y < mapTex.Height; y++) {
				for (var x = 0; x < mapTex.Width; x++) {
					var color = map[x + y * mapTex.Width];

					if (colorEntities.ContainsKey(color)) {
						var entity = colorEntities[color];
						entity.Position = new JArray(x, y);
						entities.Add(SpawnEntity(entity));
					}
				}
			}

			return level;
		}

		Entity SpawnEntity(dynamic data)
		{
			switch ((String)data.Type) {
				case "White":
					var white = new White();
					white.Transform.Position = new Vector2((float)data.Position[0], (float)data.Position[1]);
					int minStrength = data.Strength[0];
					int maxStrength = data.Strength[1];
					white.Strength = random.Next(minStrength, maxStrength);
					return white;

				case "Player":
					var player = new PlayerEntity(content.Load<Texture2D>("Sprites/playerTexture"));
					player.Transform.Position = new Vector2((float)data.Position[0], (float)data.Position[1]);
					return player;
				default:
					throw new Exception();
			}
		}
	}
}
