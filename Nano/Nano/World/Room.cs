using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Nano.Entities;

namespace Nano.World
{
	abstract class Room
	{
		bool playerInRoom = false;
		public RectangleF Location { get; private set; }
		protected abstract void OnPlayerEnter(PlayerEntity player);
		protected abstract void OnPlayerLeave(PlayerEntity player);

        public Room(RectangleF location)
        {
            Location = location;
        }


		public void UpdateWithPlayer(PlayerEntity entity)
		{
			if (entity.BoundingBox.Intersects(Location) != playerInRoom) {
				playerInRoom = !playerInRoom;
				if (playerInRoom)
					OnPlayerEnter(entity);
				else
					OnPlayerLeave(entity);
			}
		}
	}
}
