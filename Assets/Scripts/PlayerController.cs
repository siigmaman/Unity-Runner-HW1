using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 1;

    private Rigidbody rb;
    private int currentHealth;
    private int targetLane = 0;
    private bool isGrounded;
    private int currentScore;        
    private int highScore;
    private float scoreTimer;    
    private float originalSpeed;
    private float speedBoostTimer;
    private bool isSpeedBoosted;  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        Debug.Log($"Game Started! Player Health: {currentHealth}");
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log($"Рекорд: {highScore}");
    }

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveHorizontal(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveHorizontal(1);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 1f)
        {
            AddScore(10);
            scoreTimer = 0f;
        }

        if (isSpeedBoosted)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                forwardSpeed = originalSpeed;
                isSpeedBoosted = false;
                Debug.Log("Ускорение закончилось");
            }
        }
    }

    void MoveHorizontal(int direction)
    {
        targetLane = Mathf.Clamp(targetLane + direction, -1, 1);
        float targetX = targetLane * laneWidth;
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        Debug.Log("Прыжок!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("На земле");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("В воздухе");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetForwardSpeed(float newSpeed)
    {
        forwardSpeed = newSpeed;
        Debug.Log($"Скорость изменена на: {forwardSpeed}");
    }

    public float GetForwardSpeed()
    {
        return forwardSpeed;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        Debug.Log("Player Died! Restarting...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Очки: {currentScore}");
        
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log($"Новый рекорд: {highScore}");
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"Вылечен! Здоровье: {currentHealth}");
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (!isSpeedBoosted)
        {
            originalSpeed = forwardSpeed;
        }
        forwardSpeed = originalSpeed * multiplier;
        speedBoostTimer = duration;
        isSpeedBoosted = true;
        Debug.Log($"Ускорение! Скорость: {forwardSpeed}");
    }
}