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

namespace Nano
{
	class LevelLoader
	{
		string basePath;
		ContentManager content;
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
				if (entity.Color != null) {
					var color = new Color(entity.Color[0], entity.Color[1], entity.Color[2]);
					colorEntities.Add(color, entity);
				} else {
					entities.Add(SpawnEntity(entity));
				}
			}

			return level;
		}

		Entity SpawnEntity(dynamic data)
		{
			switch ((String)data.Type) {
				case "White":
                    throw new NotImplementedException();
					break;

				case "Player":
					return new PlayerEntity(content.Load<Texture2D>("Sprites/playerTexture"));
					break;
				default:
					throw new Exception();
			}
		}
	}
}
