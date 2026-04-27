using UnityEngine;

public static class TicTacToeDataService
{
    public static ThemeDataSO SelectedTheme;

    public static TicTacToeStats Load()
    {
        // Fallback for browser error when user opens up Stats before playing a single game.
        try
        {
            if (!PlayerPrefs.HasKey("Initialized"))
            {
                PlayerPrefs.SetInt("Player1Wins", 0);
                PlayerPrefs.SetInt("Player2Wins", 0);
                PlayerPrefs.SetInt("Draws", 0);
                PlayerPrefs.SetInt("TotalGameTime", 0);
                PlayerPrefs.SetInt("GamesPlayed", 0);

                PlayerPrefs.SetInt("Initialized", 1);
                PlayerPrefs.Save();
            }

            return new TicTacToeStats
            {
                player1Wins = PlayerPrefs.GetInt("Player1Wins", 0),
                player2Wins = PlayerPrefs.GetInt("Player2Wins", 0),
                draws = PlayerPrefs.GetInt("Draws", 0),
                totalGameTime = PlayerPrefs.GetInt("TotalGameTime", 0),
                gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0)
            };
        }
        catch
        {
            return new TicTacToeStats();
        }
    }

    public static void Save(TicTacToeStats stats)
    {
        PlayerPrefs.SetInt("Player1Wins", stats.player1Wins);
        PlayerPrefs.SetInt("Player2Wins", stats.player2Wins);
        PlayerPrefs.SetInt("Draws", stats.draws);
        PlayerPrefs.SetInt("TotalGameTime", stats.totalGameTime);
        PlayerPrefs.SetInt("GamesPlayed", stats.gamesPlayed);

        PlayerPrefs.Save();
    }
}