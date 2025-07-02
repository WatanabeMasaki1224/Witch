using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stunDuration = 2f;
    private bool isStunned = false;
    private float stunTimer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //スタン中なら動かない
        if (isStunned)
        {
            stunTimer = Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned=false;
            }
            return;
        }
        move();
    }
    
    void move()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    public void TakeDamage(int amount)
    {
        Debug.Log("敵が " + amount + " ダメージを受けた！");
    }
    public void Stun(float customDuration)
    {
       isStunned = true;
        stunTimer = customDuration;
    }

}
