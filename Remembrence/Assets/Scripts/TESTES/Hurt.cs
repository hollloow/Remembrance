using System;
using UnityEngine;

public class Hurt : MonoBehaviour

{
    //place holder de um código de tomar dano
    
    [SerializeField] private int damage;
    private PlayerReactions _playerReactions = new PlayerReactions();
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerReactions.OnHurt(damage);
        }
    }
}
