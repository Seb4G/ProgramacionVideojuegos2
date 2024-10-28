using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public int lives = 5;

    public UnityEvent<int> OnLivesChanged;
    private bool isDead = false;

    private void Start()
    {
        OnLivesChanged.Invoke(lives);
    }

    public void LoseLife()
    {
        if (lives <= 0 || isDead) return;

        lives--;
        OnLivesChanged.Invoke(lives);

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            isDead = true;
        }
    }

    public bool IsPlayerDead()
    {
        return isDead;
    }
}