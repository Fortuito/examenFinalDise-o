using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float baseScale = 0.5f;

    private float minX, maxX;
    private Camera cam;
    
    public static Player instance;

    public int lifes = 3;
    public int score = 0;

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
        cam = Camera.main;
        CalculateViewportLimits();
    }

    void Update()
    {
        MoveWithMouse();
        ClampToViewport();
        LookAtMouse();
    }

    void MoveWithMouse()
    {
        if (Mouse.current == null) return;
        Vector2 mouseScreen = Mouse.current.position.ReadValue(); 
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(
            mouseScreen.x,
            mouseScreen.y,
            -cam.transform.position.z
        ));
        float targetX = Mathf.Lerp(transform.position.x, mouseWorld.x, speed * Time.deltaTime);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    void LookAtMouse()
    {
        if (Mouse.current == null) return;
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(
            mouseScreen.x,
            mouseScreen.y,
            -cam.transform.position.z
        ));

        if (mouseWorld.x > transform.position.x)
        {
            transform.localScale = new Vector3(baseScale, baseScale, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-baseScale, baseScale, 1f);
        }
    }

    void ClampToViewport()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    void CalculateViewportLimits()
    {
        Vector3 left  = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, -cam.transform.position.z));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, -cam.transform.position.z));

        minX = left.x;
        maxX = right.x;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            if (Keyboard.current != null && Keyboard.current.aKey.isPressed)
            {
                score += 10;
                Destroy(other.gameObject);

                if (HudController.instance != null)
                    HudController.instance.UpdateScoreText(score);
            }
            else
            {
                lifes--;
                if(HudController.instance != null) 
                    HudController.instance.PerderVida();
                
                checkGameOver();
            }
        }

        if (other.CompareTag("Life"))
        {
            if(lifes < 3)
            {
                lifes++;
                if(HudController.instance != null)
                    HudController.instance.GanarVida();
                
                Destroy(other.gameObject);
            }
        }
        
        Debug.Log("Lifes: " + lifes + " Score: " + score);
    }

    private void checkGameOver()
    {
        if(lifes <= 0)
        {
            PlayerPrefs.SetInt("FinalScore", score);
            SceneManager.LoadScene("GameOverScene");
        }
    }
}