// Logic for TicTacToe game (essentially the game manager).

using System.Collections;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    public enum Outcome { PLAYER1, PLAYER2, DRAW, NOTOVER };
    private const int N = 3;  // Board dimensions
    private int activePlayerId = 0;
    private int[] movesCnt = new int[2];

    // Sum of each row, each col, and both diagonals (for efficient game over detection):
    private int[] rows = new int[N];
    private int[] cols = new int[N];
    private int mainDiagonal = 0;
    private int antiDiagonal = 0;

    // For replaying the game:
    private static int playsFirst = 0;  // Keeps its value when scene reloads, but resets on different sessions.

    [Header("Event Channels")]
    [SerializeField] private IntIntEventSO onCellMark;
    [SerializeField] private VoidEventSO onGameOver;

    private TicTacToeVisuals visuals;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject clickBlocker;  // Actives on game over to prevent additional user actions on the board. 



    void Awake()
    {
        activePlayerId = playsFirst;
        playsFirst = GetOtherPlayer();  // Who will play first in the next game.
    }

    void Start()
    {
        visuals = GetComponent<TicTacToeVisuals>();

        visuals.ActivatePlayer(activePlayerId, true);
        visuals.ActivatePlayer(GetOtherPlayer(), false);
    }



    private void OnEnable()
    {
        onCellMark.OnEventRaised += MarkCell;
    }

    private void OnDisable()
    {
        onCellMark.OnEventRaised -= MarkCell;
    }



    // Executes when the user clicks on an empty cell.
    public void MarkCell(int row, int col)
    {
        // Update the board with a new mark:
        int markValue = (activePlayerId == 0) ? 1 : -1;

        rows[row] += markValue;
        cols[col] += markValue;
        if (row == col) mainDiagonal += markValue;
        if (row + col == N - 1) antiDiagonal += markValue;

        // Update move counters:
        movesCnt[activePlayerId]++;

        // Visually update game:
        visuals.MarkCell(activePlayerId, row, col, N);
        visuals.UpdateMoveCounter(activePlayerId, movesCnt[activePlayerId]);

        // Check for game over:
        Outcome outcome = CheckGameOver(row, col); 
        if (outcome == Outcome.NOTOVER)
        {
            visuals.ActivatePlayer(activePlayerId, false);
            activePlayerId = GetOtherPlayer();
            visuals.ActivatePlayer(activePlayerId, true);
        }
        else
        {
            visuals.ActivatePlayer(activePlayerId, false);

            int[] winningCells = GetWinningCells(row, col);
            StartCoroutine(EndGame(outcome, winningCells));
        }
    }

    private Outcome CheckGameOver(int row, int col)
    {
        // Check for win (all three cells have the same mark):
        if (Mathf.Abs(rows[row]) == N ||
            Mathf.Abs(cols[col]) == N ||
            Mathf.Abs(mainDiagonal) == N ||
            Mathf.Abs(antiDiagonal) == N)
        {
            return (activePlayerId == 0) ? Outcome.PLAYER1 : Outcome.PLAYER2;
        }

        // Check for draw (all cells are marked):
        if (movesCnt[0] + movesCnt[1] == N * N)
        {
            return Outcome.DRAW;
        }

        return Outcome.NOTOVER;
    }

    private IEnumerator EndGame(Outcome outcome, int[] winningCells)
    {
        clickBlocker.SetActive(true);
        onGameOver.Raise(); // AudioManager and Timer are subscribed to this.

        yield return new WaitForSeconds(0.25f);  // Don't immediately show the popup panel for better UX.

        if (outcome != Outcome.DRAW) {
            visuals.DrawStrike(winningCells);
            yield return new WaitForSeconds(0.85f);  // Wait additional time for the visuals to settle in.
        }

        visuals.DisplayGameOver(outcome);

        UpdateData(outcome, timer.GetGameTime());
    }



    // Get IDs of all three winning cells based on the last cell that was marked
    private int[] GetWinningCells(int latestRow, int latestCol)
    {
        int[] winningCells = new int[N];

        // Horizontal strike:
        if (Mathf.Abs(rows[latestRow]) == N)
        {
            for (int i = 0; i < N; i++) {
                winningCells[i] = latestRow * N + i;
            }
        }

        // Vertical strike:
        else if (Mathf.Abs(cols[latestCol]) == N)
        {
            for (int i = 0; i < N; i++) {
                winningCells[i] = i * N + latestCol;
            }
        }

        // Main diagonal strike:
        else if (Mathf.Abs(mainDiagonal) == N)
        {
            for (int i = 0; i < N; i++) {
                winningCells[i] = i * N + i;
            }
        }
        
        // Anti-diagonal strike:
        else if (Mathf.Abs(antiDiagonal) == N)
        {
            for (int i = 0; i < N; i++) {
                winningCells[i] = i * N + (N - 1 - i);
            }
        }

        // Draw:
        else winningCells = null;

        return winningCells;
    }


    public void UpdateData(Outcome outcome, int gameTime)
    {
        TicTacToeStats stats = TicTacToeDataService.Load();

        stats.gamesPlayed++;
        stats.totalGameTime += gameTime;

        switch (outcome)
        {
            case Outcome.PLAYER1:
                stats.player1Wins++;
                break;
            case Outcome.PLAYER2:
                stats.player2Wins++;
                break;
            case Outcome.DRAW:
                stats.draws++;
                break;
        }
        
        TicTacToeDataService.Save(stats);
    }



    // This function if for readability purposes only, because it's used a lot.
    private int GetOtherPlayer()
    {
        return (activePlayerId + 1) % 2;
    }
}