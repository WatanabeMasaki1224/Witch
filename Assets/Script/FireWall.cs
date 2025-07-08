using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
            PlayerContolor player = other.GetComponent<PlayerContolor>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        
    }
}

