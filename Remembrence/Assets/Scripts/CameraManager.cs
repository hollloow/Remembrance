using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
    private PlayerBehavior _playerBehavior;
    public Vector3 moveTo;
    
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerBehavior = player.GetComponent<PlayerBehavior>();
    }

    void Update()
    {
        moveTo = new (player.transform.position.x + 1.5f * _playerBehavior.directionX,
            player.transform.position.y + 1.5f * _playerBehavior.directionY,1) ;

        if (_playerBehavior.directionX != 0 || _playerBehavior.directionY != 0)
        {
            transform.position = moveTo;
        }
        else
        {
            transform.position = player.transform.position;
        }
            
       
    }
}
