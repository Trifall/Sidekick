using Sidekick.Common.Game.Items;

namespace Sidekick.Apis.Poe.Trade.Models
{
    public class PseudoModifierFilter : ITradeFilter
    {
        public PseudoModifierFilter(PseudoModifier modifier)
        {
            Modifier = modifier;
            Checked = false;
            NormalizeMinValue();
        }

        public PseudoModifier Modifier { get; }

        public bool? @Checked { get; set; }

        public decimal? Min { get; set; }

        public decimal? Max { get; set; }

        /// <summary>
        /// Normalizes the Min value between a -1 delta or 90%.
        /// </summary>
        public void NormalizeMinValue()
        {
            if (Modifier.Value > 0)
            {
                Min = (int)Math.Max(Math.Min(Modifier.Value - 1, Modifier.Value * 0.9), 0);
            }
            else
            {
                Min = (int)Math.Min(Math.Min(Modifier.Value - 1, Modifier.Value * 1.1), 0);
            }
        }

        /// <summary>
        /// Normalizes the Max value between a +1 delta or 90%.
        /// </summary>
        public void NormalizeMaxValue()
        {
            if (Modifier.Value > 0)
            {
                Max = (int)Math.Max(Math.Max(Modifier.Value + 1, Modifier.Value * 1.1), 0);
            }
            else
            {
                Max = (int)Math.Min(Math.Max(Modifier.Value + 1, Modifier.Value * 0.9), 0);
            }
        }

        /// <summary>
        /// Sets the filter to be the exact value.
        /// </summary>
        public void SetExactValue()
        {
            Min = (int)Modifier.Value;
            Max = (int)Modifier.Value;
        }
    }
}
