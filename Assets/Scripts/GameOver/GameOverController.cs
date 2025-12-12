using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        if (scoreText != null)
        {
            int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // 0 es el valor por defecto
            scoreText.text = "Score: " + finalScore;
        }
    }

    void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }
}