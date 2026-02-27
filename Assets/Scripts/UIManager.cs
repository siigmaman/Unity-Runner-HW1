using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Text speedText;
    [SerializeField] private Text healthText;
    
    void Update()
    {
        if (player != null)
        {
            if (speedText != null)
            {
                speedText.text = $"Speed: {player.GetForwardSpeed():F1}";
            }
            
            if (healthText != null)
            {
                healthText.text = $"Health: {player.GetCurrentHealth()}";
            }
        }
    }
}