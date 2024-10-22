using UnityEngine;
using UnityEngine.Events;

public class PalancaTrigger : MonoBehaviour
{
    public UnityEvent OnPalancaTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Palancas"))
        {
            OnPalancaTriggered.Invoke();
        }
    }
}