using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scripts/Jugador/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int health = 5;
    public float speed;
    public float jumpHeight;
}