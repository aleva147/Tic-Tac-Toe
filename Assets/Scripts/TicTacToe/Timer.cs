using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private bool isRunning;
    private float startTime;
    private float stopTime;
    [SerializeField] private VoidEventSO onGameOver;


    void Start()
    {
        RestartTimer();
    }

    void OnEnable()
    {
        onGameOver.OnEventRaised += StopTimer;
    }

    void OnDisable()
    {
        onGameOver.OnEventRaised -= StopTimer;
    }

    void Update()
    {
        if (!isRunning) return;

        float elapsedTime = Time.time - startTime;

        text.text = TimeToString(elapsedTime);
    }


    public void RestartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        stopTime = Time.time - startTime;
    }

    public int GetGameTime()
    {
        return (int)stopTime;
    }

    
    private string TimeToString(float elapsedTime)
    {
        string text;
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        if (hours > 0)
            text = $"{hours:00}:{minutes:00}:{seconds:00}";
        else 
            text = $"{minutes:00}:{seconds:00}";

        return text;
    }
}