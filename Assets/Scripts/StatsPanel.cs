using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text gamesPlayed;
    [SerializeField] private TMP_Text playerWins;
    [SerializeField] private TMP_Text draws;
    [SerializeField] private TMP_Text avgGameTime;
    private TicTacToeStats stats;

    void Start()
    {
        stats = TicTacToeDataService.Load();

        gamesPlayed.text = stats.gamesPlayed.ToString();
        playerWins.text = $"{stats.player1Wins} - {stats.player2Wins}";
        draws.text = stats.draws.ToString();

        if (stats.gamesPlayed > 0)
            avgGameTime.text = $"{stats.totalGameTime / stats.gamesPlayed}s";
        else 
            avgGameTime.text = "0s";
    }
}