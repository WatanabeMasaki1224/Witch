using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class SharcContllorer : BaseEnemyMovement
{
    public float leftLimit = -3;
    public float rightLimit = 3;
    private bool movingRight = true;
    public float chaseRange = 5f;
    public Transform player;
    private bool isChasing = false;
    
    private void Update()
    {
        if(player == null)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position,player.position);
        isChasing = distance <= chaseRange;
    }

    public override void Move()
    {
        if(isChasing)
        {
            Vector3 direction =(player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * (player.position.x > transform.position.x ? 1 : -1);
            transform.localScale = newScale;
        }
        else
        {
            float moveDir = movingRight ?  1f : -1f;
            transform.Translate(Vector2.right * moveSpeed * moveDir * Time.deltaTime);
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * (movingRight ? 1f : -1f);
             if(transform.position.x >= rightLimit && movingRight)
            {
                movingRight = false;
            }
            else if(transform.position.x <= leftLimit &&   !movingRight) 
                {
                    movingRight=true;
                }
        }
    }

}
