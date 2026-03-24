using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Text speedText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    
    void Start()
    {
        Debug.Log("=== UIManager Start ===");
        
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerController>();
                Debug.Log("Player найден по тегу!");
            }
        }
        
        Debug.Log($"Player = {(player != null ? "ЕСТЬ" : "NULL")}");
        Debug.Log($"speedText = {(speedText != null ? speedText.name : "NULL")}");
        Debug.Log($"healthText = {(healthText != null ? healthText.name : "NULL")}");
        Debug.Log($"scoreText = {(scoreText != null ? scoreText.name : "NULL")}");
        Debug.Log($"highScoreText = {(highScoreText != null ? highScoreText.name : "NULL")}");
    }
    
    void Update()
    {
        if (player == null)
        {
            Debug.Log("UIManager: player = NULL");
            return;
        }
        
        if (speedText != null)
        {
            float currentSpeed = player.GetForwardSpeed();
            speedText.text = $"Speed: {currentSpeed:F1}";
            if (Time.frameCount % 120 == 0)
                Debug.Log($"Speed обновлён: {currentSpeed:F1}");
        }
        else
        {
            Debug.Log("speedText = NULL!");
        }
        
        if (healthText != null)
            healthText.text = $"Health: {player.GetCurrentHealth()}";
        
        if (scoreText != null)
            scoreText.text = $"Score: {player.GetCurrentScore()}";
        
        if (highScoreText != null)
            highScoreText.text = $"Best: {player.GetHighScore()}";
    }
}