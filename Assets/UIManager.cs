using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI startInfo;
    [SerializeField] TextMeshProUGUI playAgainScoreText;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject playAgain;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject carSpawner;
    private int currentScore = 0;
    private int bestScore;
    private bool gameOver;
    private bool gameStarted;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("best score");
        bestScoreText.text = "Best score: " + bestScore.ToString();
    }

    private void Update()
    {
        startText.SetActive(!gameStarted);
    }

    public void setGameStarted(bool isStarted)
    {
        gameStarted = true;
    }

    public void SetBestScore()
    {
        bestScoreText.text = "Best score: " + bestScore.ToString();
    }

    public void PlayAgainClicked()
    {
        playAgain.SetActive(false);
        gameOver = false;
        currentScore = 0;
    }

    public void UpdateScore(int score)
    {
        currentScore++;
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void IncreaseScore()
    {
        currentScore++;
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void DisplayMainMenu(bool display)
    {
        mainMenu.SetActive(display);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        if (currentScore > PlayerPrefs.GetInt("best score"))
        {
            PlayerPrefs.SetInt("best score", currentScore);
            SetBestScore();
        }
        playAgainScoreText.text = "YOUR SCORE: " + currentScore.ToString();
        playAgain.SetActive(true);
        scoreText.text = "Score: ";
        gameOver = true;
        DisplayMainMenu(true);
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
