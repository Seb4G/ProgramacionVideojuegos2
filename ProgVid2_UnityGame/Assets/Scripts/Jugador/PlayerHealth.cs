using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 5;
    public int lives { get; private set; }
    public bool isDead => lives <= 0;

    public UnityEvent<int> OnLivesChanged;
    private void Start()
    {
        lives = maxLives;
        OnLivesChanged.Invoke(lives);
    }

    public void LoseLife()
    {
        if (lives <= 0 || isDead) return;

        lives--;
        OnLivesChanged.Invoke(lives);

        if (lives <= 0)
        {
            GameEvents.TriggerGameOver();
        }
    }
    public void GainLife()
    {
        if (lives < maxLives)
        {
            lives++;
            OnLivesChanged.Invoke(lives);
        }
    }

    public bool IsPlayerDead()
    {
        return isDead;
    }
}