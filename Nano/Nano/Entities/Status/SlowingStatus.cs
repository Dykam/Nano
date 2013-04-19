using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities.Status
{
    class SlowingStatus : EntityStatus
    {
        float duration;

        public SlowingStatus(LivingEntity enity, float duration)
            : base(enity)
        {
            this.duration = duration;
        }

        public override void Activate()
        {
            float originalSpeed = Entity.Speed;
            Entity.Speed = Entity.Speed / 2;
			NanoGame.Awaiter.Delay(duration).ContinueWith(t => { Entity.Speed = originalSpeed; Entity.RemoveStatus(this); }, System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
