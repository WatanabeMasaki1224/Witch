using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : BaseEnemyMovement
{
    private int direction = 1;
    public float leftLimit = -3f;  // ¶‚ÌˆÚ“®”ÍˆÍ
    public float rightLimit = 3f;  // ‰E‚ÌˆÚ“®”ÍˆÍ
    private bool movingRight = true;

    public override void Move()
    {
        float moveDir = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * moveSpeed * moveDir * Time.deltaTime);

        if (transform.position.x >= rightLimit && movingRight)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftLimit && !movingRight) 
        {
            movingRight = true;
        }
    }
}
