using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public int Score = 0;

    public Food food;

    public GameObject StartScreen;
    public GameObject Snake;

    private bool isPaused = true;

    private void Start()
    {
        Score = 0;
        PauseGame(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ScoreIncrease()
    {
        Score++;
        textMeshProUGUI.text = Score.ToString();
    }
    public void ResetScore()
    {
        Score = 0;
        textMeshProUGUI.text = Score.ToString();
        food.RandomizePosition();
    }
    public void PauseGame(bool state)
    {
        isPaused = state;
        Time.timeScale = state ? 0 : 1;
        StartScreen.SetActive(state);
        Snake.SetActive(!state);

        if (state)
            ResetScore(); 
    }
    public void Play()
    {
        PauseGame(false);
        ResetScore();
    }
}