using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities.Status
{
	class PoisonStatus : EntityStatus
	{
		public PoisonStatus(LivingEntity entity, float strength, float duration)
			: base(entity)
		{
			
		}

		public override void Activate()
		{
			throw new NotImplementedException();
		}
	}
}
