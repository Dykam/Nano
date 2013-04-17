using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities.Status
{
    class RegenerationStatus : EntityStatus
    {
        float amountPerTick;

        public RegenerationStatus(LivingEntity entity, float amount, float duration)
            : base(entity)
        {
            amountPerTick = amount / duration;
        }

        public override void Activate()
        {
            
        }
    }
}
