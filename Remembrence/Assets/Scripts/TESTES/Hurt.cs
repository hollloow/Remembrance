using System;
using UnityEngine;

public class Hurt : MonoBehaviour

{
    //place holder de um código de tomar dano
    
    [SerializeField] private int damage;
    private PlayerReactions _playerReactions = new PlayerReactions();
    public bool hit = false;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hit)
        {
            _playerReactions.OnHurt(damage);
            hit = true;
        }
    }
}
