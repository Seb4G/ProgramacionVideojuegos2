using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    public UnityEvent<int> OnLivesChanged;
    private bool isDead = false;

    public void LoseLife()
    {
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