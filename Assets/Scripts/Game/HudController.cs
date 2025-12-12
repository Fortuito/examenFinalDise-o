using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public static HudController instance;

    [Header("Configuración Vidas")]
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private float spacing = 0.7f;
    
    [Header("Configuración Puntaje")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int lives = 3;
    private Camera cam;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (Player.instance != null) 
        {
            lives = Player.instance.lifes; 
        }
        
        cam = Camera.main;
        DrawLives();
        UpdateScoreText(0);
    }

    public void SetLives(int newLives) 
    {
        lives = newLives;
        RefreshLives();
    }

    public void GanarVida()
    {
        lives++;
        RefreshLives();
    }

    public void PerderVida()
    {
        if (lives <= 0) return;

        lives--;
        RefreshLives();
    }

    private void RefreshLives() 
    {
        ClearLives();
        DrawLives();
    }

    private void DrawLives()
    {
        if (lifePrefab == null)
        {
            Debug.LogError("¡Falta asignar el Life Prefab en el Inspector!");
            return;
        }

        Vector3 rightTop = cam.ViewportToWorldPoint(
            new Vector3(1f, 1f, -cam.transform.position.z + 10f) 
        );

        float startX = rightTop.x - 1f;
        float startY = rightTop.y - 1f;

        for (int i = 0; i < lives; i++)
        {
            Vector3 pos = new Vector3(startX - (i * spacing), startY, 0f);
            GameObject obj = Instantiate(lifePrefab, pos, Quaternion.identity, transform);
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;
            }
        }
    }

    private void ClearLives()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore;
        }
    }
}