using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private Text livesText;

    public void UpdateLives(int lives)
    {
        //livesText.text = $"Vidas: {vidas}";
    }
}