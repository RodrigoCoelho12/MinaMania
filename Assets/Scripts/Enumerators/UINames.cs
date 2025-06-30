public enum UINames
{
    MainMenu,
    Settings,
    Ranking,
    KeyBinds,
    Loading,
}

public static class UINamesExtensions
{
    public static string GetPanelName(this UINames name)
    {
        //Panel name pattern: "Name Panel"
        return $"{name} Panel"; 
    }
}