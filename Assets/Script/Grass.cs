using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class Grass : MonoBehaviour
{
    public float stunDuration = 1f;
    public int damage = 1;
    public float jumpBoost = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if(enemy != null )
        {
            enemy.Stun(stunDuration);
            enemy.TakeDamage(damage);

        }
        PlayerContolor player = other.GetComponent<PlayerContolor>();
        if( player != null ) 
            {
                player.BoostJump(jumpBoost);
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
     PlayerContolor player = other.GetComponent <PlayerContolor>();
        if( player != null )
        {
            player.ResetJump();
        }
    }

}
