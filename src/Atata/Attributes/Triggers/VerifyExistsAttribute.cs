﻿namespace Atata
{
    public class VerifyExistsAttribute : TriggerAttribute
    {
        public VerifyExistsAttribute(TriggerEvents on = TriggerEvents.OnPageObjectInit, TriggerPriority priority = TriggerPriority.Medium, TriggerScope appliesTo = TriggerScope.Self)
            : base(on, priority, appliesTo)
        {
        }

        public override void Run(TriggerContext context)
        {
            context.Component.VerifyExists();
        }
    }
}
