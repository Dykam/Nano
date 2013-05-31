using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nano.Entities.Status
{
	class PoisonStatus : EntityStatus
	{
        float strength;
		public PoisonStatus(LivingEntity entity, float strength, float duration)
			: base(entity)
		{
            this.strength = strength;
		}

		public override void Activate()
		{
			NanoGame.Awaiter.Delay(1000).ContinueWith(t => Entity.Damage(strength), System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
		}
	}
}
