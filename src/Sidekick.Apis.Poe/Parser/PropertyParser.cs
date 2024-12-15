using System.Globalization;
using System.Text.RegularExpressions;
using Sidekick.Apis.Poe.Parser.Patterns;
using Sidekick.Common.Game.Items;
using Sidekick.Common.Game.Languages;
using Sidekick.Common.Initialization;

namespace Sidekick.Apis.Poe.Parser;

public class PropertyParser(IGameLanguageProvider gameLanguageProvider) : IInitializableService
{
    public int Priority => 100;

    private Regex? Armor { get; set; }

    private Regex? EnergyShield { get; set; }

    private Regex? Evasion { get; set; }

    private Regex? ChanceToBlock { get; set; }

    private Regex? Quality { get; set; }

    private Regex? AlternateQuality { get; set; }

    private Regex? Level { get; set; }

    private Regex? ItemLevel { get; set; }

    private Regex? MapTier { get; set; }

    private Regex? ItemQuantity { get; set; }

    private Regex? ItemRarity { get; set; }

    private Regex? MonsterPackSize { get; set; }

    private Regex? AttacksPerSecond { get; set; }

    private Regex? CriticalStrikeChance { get; set; }

    private Regex? ElementalDamage { get; set; }

    private Regex? PhysicalDamage { get; set; }

    private Regex? Blighted { get; set; }

    private Regex? BlightRavaged { get; set; }

    private Regex? Anomalous { get; set; }

    private Regex? Divergent { get; set; }

    private Regex? Phantasmal { get; set; }

    private Regex? AreaLevel { get; set; }

    private Regex? Unidentified { get; set; }

    private Regex? Corrupted { get; set; }

    public Task Initialize()
    {
        Armor = gameLanguageProvider.Language.DescriptionArmour.ToRegexIntCapture();
        EnergyShield = gameLanguageProvider.Language.DescriptionEnergyShield.ToRegexIntCapture();
        Evasion = gameLanguageProvider.Language.DescriptionEvasion.ToRegexIntCapture();
        ChanceToBlock = gameLanguageProvider.Language.DescriptionChanceToBlock.ToRegexIntCapture();
        Level = gameLanguageProvider.Language.DescriptionLevel.ToRegexIntCapture();
        ItemLevel = gameLanguageProvider.Language.DescriptionItemLevel.ToRegexIntCapture();
        AttacksPerSecond = gameLanguageProvider.Language.DescriptionAttacksPerSecond.ToRegexDecimalCapture();
        CriticalStrikeChance = gameLanguageProvider.Language.DescriptionCriticalStrikeChance.ToRegexDecimalCapture();
        ElementalDamage = gameLanguageProvider.Language.DescriptionElementalDamage.ToRegexStartOfLine();
        PhysicalDamage = gameLanguageProvider.Language.DescriptionPhysicalDamage.ToRegexStartOfLine();

        Quality = gameLanguageProvider.Language.DescriptionQuality.ToRegexIntCapture();
        AlternateQuality = gameLanguageProvider.Language.DescriptionAlternateQuality.ToRegexLine();

        MapTier = gameLanguageProvider.Language.DescriptionMapTier.ToRegexIntCapture();
        AreaLevel = gameLanguageProvider.Language.DescriptionAreaLevel.ToRegexIntCapture();
        ItemQuantity = gameLanguageProvider.Language.DescriptionItemQuantity.ToRegexIntCapture();
        ItemRarity = gameLanguageProvider.Language.DescriptionItemRarity.ToRegexIntCapture();
        MonsterPackSize = gameLanguageProvider.Language.DescriptionMonsterPackSize.ToRegexIntCapture();
        Blighted = gameLanguageProvider.Language.AffixBlighted.ToRegexAffix();
        BlightRavaged = gameLanguageProvider.Language.AffixBlightRavaged.ToRegexAffix();
        Anomalous = gameLanguageProvider.Language.AffixAnomalous.ToRegexAffix();
        Divergent = gameLanguageProvider.Language.AffixDivergent.ToRegexAffix();
        Phantasmal = gameLanguageProvider.Language.AffixPhantasmal.ToRegexAffix();

        Unidentified = gameLanguageProvider.Language.DescriptionUnidentified.ToRegexLine();
        Corrupted = gameLanguageProvider.Language.DescriptionCorrupted.ToRegexLine();

        return Task.CompletedTask;
    }

    public Properties Parse(ParsingItem parsingItem)
    {
        return parsingItem.Metadata?.Category switch
        {
            Category.Gem => ParseGemProperties(parsingItem),
            Category.Map or Category.Contract => ParseMapProperties(parsingItem),
            Category.Accessory => ParseAccessoryProperties(parsingItem),
            Category.Armour => ParseArmourProperties(parsingItem),
            Category.Weapon => ParseWeaponProperties(parsingItem),
            Category.Jewel => ParseJewelProperties(parsingItem),
            Category.Flask => ParseFlaskProperties(parsingItem),
            Category.Sanctum => ParseSanctumProperties(parsingItem),
            Category.Logbook => ParseLogbookProperties(parsingItem),
            _ => new Properties(),
        };
    }

    private Properties ParseWeaponProperties(ParsingItem parsingItem)
    {
        var propertyBlock = parsingItem.Blocks[1];
        var attacksPerSecond = GetDouble(AttacksPerSecond, propertyBlock);
        var criticalStrikeChance = GetDouble(CriticalStrikeChance, propertyBlock);

        // Parse physical damage
        var physicalDamage = new DamageRange();
        foreach (var line in propertyBlock.Lines)
        {
            if (!line.Text.Contains("Physical Damage:"))
            {
                continue;
            }

            var matches = new Regex("(\\d+)-(\\d+)").Matches(line.Text);
            if (matches.Count <= 0 || matches[0].Groups.Count < 3)
            {
                continue;
            }

            double.TryParse(matches[0].Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var min);
            double.TryParse(matches[0].Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var max);
            physicalDamage.Min = min;
            physicalDamage.Max = max;
        }

        // Parse chaos damage
        var chaosDamage = new DamageRange();
        foreach (var line in propertyBlock.Lines)
        {
            if (!line.Text.Contains("Chaos Damage:"))
            {
                continue;
            }

            var matches = new Regex("(\\d+)-(\\d+)").Matches(line.Text);
            if (matches.Count <= 0 || matches[0].Groups.Count < 3)
            {
                continue;
            }

            double.TryParse(matches[0].Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var min);
            double.TryParse(matches[0].Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var max);
            chaosDamage.Min = min;
            chaosDamage.Max = max;
        }

        // Parse elemental damages
        var elementalDamages = new List<DamageRange>();
        var elementalDamageTypes = new List<DamageType>();

        // First check modifiers to determine damage types from "Adds X to Y" lines
        foreach (var line in propertyBlock.Lines)
        {
            if (!line.Text.Contains("Adds") || !line.Text.Contains("Damage"))
            {
                continue;
            }

            if (line.Text.Contains("Fire"))
                elementalDamageTypes.Add(DamageType.Fire);
            else if (line.Text.Contains("Cold"))
                elementalDamageTypes.Add(DamageType.Cold);
            else if (line.Text.Contains("Lightning")) elementalDamageTypes.Add(DamageType.Lightning);
        }

        // Parse individual elemental damage lines first
        foreach (var line in propertyBlock.Lines)
        {
            if (line.Text.Contains("Fire Damage:"))
            {
                var matches = new Regex("(\\d+)-(\\d+)").Matches(line.Text);
                if (matches.Count <= 0 || matches[0].Groups.Count < 3)
                {
                    continue;
                }

                double.TryParse(matches[0].Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var min);
                double.TryParse(matches[0].Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var max);
                elementalDamages.Add(new DamageRange
                {
                    Min = min,
                    Max = max,
                    Type = DamageType.Fire
                });
            }
            else if (line.Text.Contains("Cold Damage:"))
            {
                var matches = new Regex("(\\d+)-(\\d+)").Matches(line.Text);
                if (matches.Count <= 0 || matches[0].Groups.Count < 3)
                {
                    continue;
                }

                double.TryParse(matches[0].Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var min);
                double.TryParse(matches[0].Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var max);
                elementalDamages.Add(new DamageRange
                {
                    Min = min,
                    Max = max,
                    Type = DamageType.Cold
                });
            }
            else if (line.Text.Contains("Lightning Damage:"))
            {
                var matches = new Regex("(\\d+)-(\\d+)").Matches(line.Text);
                if (matches.Count <= 0 || matches[0].Groups.Count < 3)
                {
                    continue;
                }

                double.TryParse(matches[0].Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var min);
                double.TryParse(matches[0].Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var max);
                elementalDamages.Add(new DamageRange
                {
                    Min = min,
                    Max = max,
                    Type = DamageType.Lightning
                });
            }
        }

        // If no individual lines were found, try to parse combined "Elemental Damage:" line
        if (!elementalDamages.Any())
        {
            foreach (var line in propertyBlock.Lines)
            {
                if (!line.Text.Contains("Elemental Damage:"))
                {
                    continue;
                }

                var matches = new Regex("(\\d+)-(\\d+)").Matches(line.Text);
                var matchIndex = 0;
                foreach (Match match in matches)
                {
                    if (match.Groups.Count < 3)
                    {
                        continue;
                    }

                    double.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var min);
                    double.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var max);

                    // Get the damage type from the corresponding "Adds X to Y" line
                    var damageType = matchIndex < elementalDamageTypes.Count ? elementalDamageTypes[matchIndex] : DamageType.Physical;

                    elementalDamages.Add(new DamageRange
                    {
                        Min = min,
                        Max = max,
                        Type = damageType
                    });
                    matchIndex++;
                }

                break;
            }
        }

        // Calculate DPS values
        var physicalDps = physicalDamage.Min > 0 || physicalDamage.Max > 0 ? Math.Round(((physicalDamage.Min + physicalDamage.Max) / 2) * attacksPerSecond, 1) : null as double?;

        var elementalDps = elementalDamages.Any() ? Math.Round(elementalDamages.Sum(x => (x.Min + x.Max) / 2) * attacksPerSecond, 1) : null as double?;

        var chaosDps = chaosDamage.Min > 0 || chaosDamage.Max > 0 ? Math.Round(((chaosDamage.Min + chaosDamage.Max) / 2) * attacksPerSecond, 1) : null as double?;

        // Calculate total DPS including chaos damage
        var totalDps = Math.Round((physicalDps ?? 0) + (elementalDps ?? 0) + (chaosDps ?? 0), 1);

        return new Properties
        {
            ItemLevel = GetInt(ItemLevel, parsingItem),
            Identified = !GetBool(Unidentified, parsingItem),
            Corrupted = GetBool(Corrupted, parsingItem),
            Quality = GetInt(Quality, propertyBlock),
            AttacksPerSecond = attacksPerSecond,
            CriticalStrikeChance = criticalStrikeChance,
            PhysicalDamage = physicalDamage,
            ElementalDamages = elementalDamages,
            ChaosDamage = chaosDamage,
            PhysicalDps = physicalDps,
            ElementalDps = elementalDps,
            ChaosDps = chaosDps,
            TotalDps = totalDps > 0 ? totalDps : null
        };
    }

    private Properties ParseArmourProperties(ParsingItem parsingItem)
    {
        var propertyBlock = parsingItem.Blocks[1];

        return new Properties()
        {
            ItemLevel = GetInt(ItemLevel, parsingItem),
            Identified = !GetBool(Unidentified, parsingItem),
            Corrupted = GetBool(Corrupted, parsingItem),
            Quality = GetInt(Quality, propertyBlock),
            Armor = GetInt(Armor, propertyBlock),
            EnergyShield = GetInt(EnergyShield, propertyBlock),
            Evasion = GetInt(Evasion, propertyBlock),
            ChanceToBlock = GetInt(ChanceToBlock, propertyBlock),
        };
    }

    private Properties ParseAccessoryProperties(ParsingItem parsingItem)
    {
        return new Properties()
        {
            ItemLevel = GetInt(ItemLevel, parsingItem),
            Identified = !GetBool(Unidentified, parsingItem),
            Corrupted = GetBool(Corrupted, parsingItem),
        };
    }

    private Properties ParseMapProperties(ParsingItem parsingItem)
    {
        var propertyBlock = parsingItem.Blocks[1];

        return new Properties()
        {
            ItemLevel = GetInt(ItemLevel, parsingItem),
            Identified = !GetBool(Unidentified, parsingItem),
            Corrupted = GetBool(Corrupted, parsingItem),
            Blighted = Blighted?.IsMatch(parsingItem.Blocks[0].Lines[^1].Text) ?? false,
            BlightRavaged = BlightRavaged?.IsMatch(parsingItem.Blocks[0].Lines[^1].Text) ?? false,
            ItemQuantity = GetInt(ItemQuantity, propertyBlock),
            ItemRarity = GetInt(ItemRarity, propertyBlock),
            MonsterPackSize = GetInt(MonsterPackSize, propertyBlock),
            MapTier = GetInt(MapTier, propertyBlock),
            Quality = GetInt(Quality, propertyBlock),
        };
    }

    private Properties ParseGemProperties(ParsingItem parsingItem)
    {
        var propertyBlock = parsingItem.Blocks[1];

        return new Properties()
        {
            Corrupted = GetBool(Corrupted, parsingItem),
            GemLevel = GetInt(Level, propertyBlock),
            Quality = GetInt(Quality, propertyBlock),
            AlternateQuality = GetBool(AlternateQuality, parsingItem),
            Anomalous = GetBool(Anomalous, parsingItem),
            Divergent = GetBool(Divergent, parsingItem),
            Phantasmal = GetBool(Phantasmal, parsingItem),
        };
    }

    private Properties ParseJewelProperties(ParsingItem parsingItem)
    {
        return new Properties()
        {
            ItemLevel = GetInt(ItemLevel, parsingItem),
            Identified = !GetBool(Unidentified, parsingItem),
            Corrupted = GetBool(Corrupted, parsingItem),
        };
    }

    private Properties ParseFlaskProperties(ParsingItem parsingItem)
    {
        return new Properties()
        {
            ItemLevel = GetInt(ItemLevel, parsingItem),
            Identified = !GetBool(Unidentified, parsingItem),
            Corrupted = GetBool(Corrupted, parsingItem),
            Quality = GetInt(Quality, parsingItem),
        };
    }

    private Properties ParseSanctumProperties(ParsingItem parsingItem)
    {
        return new Properties
        {
            AreaLevel = GetInt(AreaLevel, parsingItem),
        };
    }

    private Properties ParseLogbookProperties(ParsingItem parsingItem)
    {
        return new Properties
        {
            AreaLevel = GetInt(AreaLevel, parsingItem),
        };
    }

    private static bool GetBool(Regex? pattern, ParsingItem parsingItem)
    {
        if (pattern == null)
        {
            return false;
        }

        return parsingItem.TryParseRegex(pattern, out _);
    }

    private static int GetInt(Regex? pattern, ParsingItem parsingItem)
    {
        if (pattern == null)
        {
            return 0;
        }

        if (parsingItem.TryParseRegex(pattern, out var match) && int.TryParse(match.Groups[1].Value, out var result))
        {
            return result;
        }

        return 0;
    }

    private static int GetInt(Regex? pattern, ParsingBlock parsingBlock)
    {
        if (pattern == null)
        {
            return 0;
        }

        if (parsingBlock.TryParseRegex(pattern, out var match) && int.TryParse(match.Groups[1].Value, out var result))
        {
            return result;
        }

        return 0;
    }

    private static double GetDouble(Regex? pattern, ParsingBlock parsingBlock)
    {
        if (pattern == null)
        {
            return 0;
        }

        if (!parsingBlock.TryParseRegex(pattern, out var match))
        {
            return 0;
        }

        var value = match.Groups[1].Value.Replace(",", ".");
        if (value.EndsWith("%"))
        {
            value = value.TrimEnd('%');
        }

        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
        {
            return result;
        }

        return 0;
    }

    private static double GetDps(Regex? pattern, ParsingBlock parsingBlock, double attacksPerSecond)
    {
        if (pattern == null)
        {
            return 0;
        }

        if (!parsingBlock.TryParseRegex(pattern, out var match))
        {
            return 0;
        }

        var matches = new Regex("(\\d+-\\d+)").Matches(match.Value);
        var dps = matches.Select(x => x.Value.Split("-"))
            .Sum(split =>
            {
                if (double.TryParse(split[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var minValue) && double.TryParse(split[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var maxValue))
                {
                    return (minValue + maxValue) / 2d;
                }

                return 0d;
            });

        return Math.Round(dps * attacksPerSecond, 2);
    }
}
