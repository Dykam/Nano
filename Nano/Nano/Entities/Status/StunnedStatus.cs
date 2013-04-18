using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities.Status
{
    class StunnedStatus : EntityStatus
    {
        float duration;

        public StunnedStatus(LivingEntity entity, float duration)
            : base(entity)
        {
            this.duration = duration;
        }

        public override void Activate()
        {
            Entity.Stunned = true;
            NanoGame.Awaiter.Delay(duration).ContinueWith(t => { Entity.Stunned = false; Entity.RemoveStatus(this);  });
        }
    }
}
