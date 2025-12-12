using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;

    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogError("¡No has asignado el botón en el Inspector!");
        }
    }

    void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene"); 
    }
} 
