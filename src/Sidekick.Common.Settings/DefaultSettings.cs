namespace Sidekick.Common.Settings;

public static class DefaultSettings
{
    public static string LanguageParser => "en";

    public static string LanguageUi => "en";

    public static string KeyClose => "Space";

    public static string KeyOpenWealth => "F7";

    public static string KeyOpenPriceCheck => "Ctrl+D";

    public static string KeyOpenMapCheck => "Ctrl+X";

    public static string KeyOpenWiki => "Alt+W";

    public static string KeyFindItems => "Ctrl+F";

    public static bool EscapeClosesOverlays => true;

    public static bool RetainClipboard => true;

    public static string PreferredWiki => WikiSetting.PoeWiki.ToString();

    public static bool PriceCheckPredictionEnabled => true;

    public static string MapCheckDangerousRegex => "reflect|regen";

    public static string PriceCheckBulkCurrency => "divine";

    public static int PriceCheckBulkMinimumStock => 5;

    public static string PriceCheckCurrencyLayout => "Item";

    public static List<ChatSetting> ChatCommands =>
    [
        new ChatSetting("F5", "/hideout", true),
        new ChatSetting("F4", "/leave", true),
        new ChatSetting("Ctrl+Enter", "@last ", false),
        new ChatSetting("F9", "/exit", true),
    ];
}
