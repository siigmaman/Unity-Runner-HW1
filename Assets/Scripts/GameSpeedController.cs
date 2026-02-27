using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float speedIncreasePerSecond = 0.1f;
    
    private float currentSpeed;
    private PlayerController playerController;
    
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController не найден на этом объекте!");
            return;
        }
        
        currentSpeed = startSpeed;
        playerController.SetForwardSpeed(currentSpeed);
    }
    
    void Update()
    {
        if (playerController == null) return;
        
        currentSpeed += speedIncreasePerSecond * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        
        playerController.SetForwardSpeed(currentSpeed);
    }
}