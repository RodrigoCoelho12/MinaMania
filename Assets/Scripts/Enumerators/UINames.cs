public enum UINames
{
    MainMenu,
    Settings,
    Store,
    InsertUsername,
    HUD,
    Pause,
    Ranking,
    Dialog,
    Credit,
    SpecificCredit,
    KeyBinds,
    Tutorial,
    GameOver,
    Loading,
}

public static class UINamesExtensions
{
    public static string GetPanelName(this UINames name)
    {
        //Panel name pattern: "Name_Panel"
        return $"{name}_Panel"; 
    }
}