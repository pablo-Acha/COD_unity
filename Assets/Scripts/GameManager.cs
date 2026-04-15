using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int team1Score = 0;
    public int team2Score = 0;

    public float matchTime = 300f;
    private float timer;

    public TextMeshProUGUI timerText;

    void Awake()
    {
        instance = this;
        timer = matchTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            EndGame();
        }
    }

    public void AddPoint(int team)
    {
        if (team == 1) team1Score++;
        else team2Score++;
    }

    void EndGame()
    {
        Time.timeScale = 0;
        Debug.Log(team1Score > team2Score ? "Team 1 Wins" : "Team 2 Wins");
    }
}