using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities
{
	class PlayerEntity : LivingEntity
	{
		public PlayerEntity()
		{
			Health = MaxHealth = 20;
		}
	}
}
