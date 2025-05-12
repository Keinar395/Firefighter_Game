using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject youWinPanel;
    public GameObject youLosePanel;

    void Start()
    {
        youWinPanel.SetActive(false);
        youLosePanel.SetActive(false);
    }

    public void ShowWin()
    {
        youWinPanel.SetActive(true);
    }

    public void ShowLose()
    {
        youLosePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Demo");
        ScoreScript.scoreValue = 0;
    }
}
