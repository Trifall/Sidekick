using Sidekick.Apis.Poe.Trade.Models;

namespace Sidekick.Apis.Poe.Trade.Requests.Filters;

internal class SearchFilterValue
{
    internal SearchFilterValue()
    {
    }

    public SearchFilterValue(string? option)
    {
        Option = option;
    }

    public SearchFilterValue(PropertyFilter filter)
    {
        Option = filter.Checked == true ? "true" : "false";
        Min = filter.Min;
        Max = filter.Max;
    }

    public SearchFilterValue(ModifierFilter filter)
    {
        Option = filter.Line.OptionValue;
        Min = filter.Min;
        Max = filter.Max;
    }

    public SearchFilterValue(PseudoModifierFilter filter)
    {
        Min = filter.Min;
        Max = filter.Max;
    }

    public object? Option { get; set; }

    public decimal? Min { get; set; }

    public decimal? Max { get; set; }
}
