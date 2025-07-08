using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float stunDuration = 2f;
    private bool isStunned = false;
    private float stunTimer = 0f;
    public int damagetoPlayer = 1;
    private BaseEnemyMovement movement;

    public int maxHealth = 5;
    private int currentHealth;
    void Start()
    {
        movement = GetComponent<BaseEnemyMovement>();  
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //スタン中なら動かない
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
            }
            return;
        }
        if (movement != null)
        {

            movement.Move();
        }
    }

       
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Stun(stunDuration);

        Debug.Log("敵が " + amount + " ダメージを受けた！");
        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
    public void Stun(float customDuration)
    {
       isStunned = true;
        stunTimer = customDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerContolor player = collision.GetComponent<PlayerContolor>();
        if(player != null )
        {
            player.TakeDamage(damagetoPlayer);
        }
    }

}
