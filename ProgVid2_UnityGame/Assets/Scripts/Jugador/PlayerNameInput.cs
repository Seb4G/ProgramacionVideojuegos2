using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private HUDController hudController;

    public void SavePlayerName()
    {
        if (playerNameInputField != null && hudController != null)
        {
            string playerName = playerNameInputField.text;
            if (!string.IsNullOrEmpty(playerName))
            {
                hudController.SavePlayerName(playerName);
                Debug.Log($"Player name '{playerName}' saved.");
            }
        }
    }
}