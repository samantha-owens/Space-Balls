using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI newHighScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    public Button restartButton;

    public bool gameOver;

    public int score;
    public int highScore;

    private void Start()
    {
        gameOver = false;

        // grabs high score from previous game
        highScore = PlayerPrefs.GetInt("highscore", 0);
     
        // sets score and updates UI
        score = 0;
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    // adds a point to the score and updates UI
    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    // if there is a new high score then save to player prefs, update UI, and start high score flashing text
    public void SaveScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            highScoreText.text = "High Score: " + score;
            StartCoroutine(FlashText());
        }
    }

    // flash the high score text on and off
    IEnumerator FlashText()
    {
        while (gameOver)
        {
            newHighScoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            newHighScoreText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // initiate game over, start save score method
    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        SaveScore();
    }

    // restarts the scene, attached to the RESTART button in UI
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
