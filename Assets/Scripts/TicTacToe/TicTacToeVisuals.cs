using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class TicTacToeVisuals : MonoBehaviour
{
    [SerializeField] private ThemeDataSO theme;
    [SerializeField] private Cell[] board;  // Todo: Change to CellVisuals in the future
    [SerializeField] private GameObject[] playerHighlight;
    [SerializeField] private Image[] playerBg;
    [SerializeField] private Color[] playerBgColor; // Inactive and active player bg colors.
    [SerializeField] private TMP_Text[] movesCntTxt;

    [Header("Winning Strike")]    
    [SerializeField] private RectTransform winLine;
    [SerializeField] private float extraLineLen = 60f;
    [SerializeField] private float lineDrawDuration = 0.5f;

    [Header("Game Over Panel")]    
    [SerializeField] private PanelAnimator gameOverPanel;
    [SerializeField] private TMP_Text timerPanelText;
    [SerializeField] private TMP_Text timerGOText;
    [SerializeField] private TMP_Text outcomeText;
    private readonly Dictionary<TicTacToe.Outcome, string> outcomeStrings = new Dictionary<TicTacToe.Outcome, string>
    {
        [TicTacToe.Outcome.PLAYER1] = "PLAYER ONE WINS!", 
        [TicTacToe.Outcome.PLAYER2] = "PLAYER TWO WINS!", 
        [TicTacToe.Outcome.DRAW] = "DRAW!", 
    };

    [Header("Event Channels")]
    [SerializeField] private VoidEventSO onGameOver;

    [SerializeField] private ImpactVFX vfxImpact;



    void Awake()
    {
        theme = TicTacToeDataService.SelectedTheme;
    }



    public void UpdateMoveCounter(int playerId, int count)
    {
        movesCntTxt[playerId].text = count.ToString();
    }
    
    public void MarkCell(int playerId, int row, int col, int N)
    {
        int cellId = row * N + col;
        board[cellId].Mark(playerId == 0 ? theme.xSprite : theme.oSprite);

        vfxImpact.Play(board[cellId].GetComponent<RectTransform>().anchoredPosition);
    }

    public void ActivatePlayer(int playerId, bool setActive)
    {
        playerHighlight[playerId].SetActive(setActive);
        playerBg[playerId].color = playerBgColor[setActive ? 1 : 0];
    }



    public void DrawStrike(int[] winningCellIDs)
    {
        Vector2 start = GetCellPosition(winningCellIDs[0]);
        Vector2 end   = GetCellPosition(winningCellIDs[2]);
        Debug.Log("START: " + start.ToString());
        Debug.Log("END: " + end.ToString());
        
        // Expand line further from cell centers:
        Vector2 dirNorm = (end - start).normalized;
        start -= dirNorm * extraLineLen;
        end   += dirNorm * extraLineLen;

        Vector2 dir = end - start;
        float length = dir.magnitude;
        
        // Position
        winLine.anchoredPosition = start;

        // Rotation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        winLine.localRotation = Quaternion.Euler(0, 0, angle);

        // Start collapsed
        winLine.sizeDelta = new Vector2(0, winLine.sizeDelta.y);

        winLine.gameObject.SetActive(true);

        // Animate to full length
        winLine.DOSizeDelta(
            new Vector2(length, winLine.sizeDelta.y),
            lineDrawDuration
        );
    }

    public void DisplayGameOver(TicTacToe.Outcome outcome)
    {
        gameOverPanel.Open();

        outcomeText.text = outcomeStrings[outcome];
        timerPanelText.text = timerGOText.text;
    }



    Vector2 GetCellPosition(int index)
    {
        return board[index].GetComponent<RectTransform>().anchoredPosition;
    }
}