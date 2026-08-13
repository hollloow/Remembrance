using System;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    //place holder de um código de tomar dano
    
    [SerializeField] private int damage;
    [SerializeField] private float shake;
    [SerializeField] private float impulseAmount;
    private PlayerReactions _playerReactions = new PlayerReactions();
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !PlayerStats.invincibility)
        {
            _playerReactions.OnHurt(damage,shake);
        }
    }
}
