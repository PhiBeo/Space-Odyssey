using System.Collections.Generic;

public enum Speed
{
    stop,
    slow,
    normal,
    fast
}

public enum GameoverType
{ 
    caught,
    fuel,
    health
}

public enum SceneType
{ 
    MainMenu,
    Intro,
    Gameplay,
    Outro,
    Gameover
}


public static class GlobalData
{
    public static Dictionary<int, string> monthNumToString = new Dictionary<int, string>()
    {
        { 1 , "January"},
        { 2 , "february"},
        { 3 , "March"},
        { 4 , "April"},
        { 5 , "May"},
        { 6 , "June"},
        { 7 , "July"},
        { 8 , "August"},
        { 9 , "September"},
        { 10 , "October"},
        { 11 , "November"},
        { 12 , "December"}
    };
}
