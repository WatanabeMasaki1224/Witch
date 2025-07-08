using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    private bool isCleansed = false;  //毒沼が浄化されているか
    public GameObject cleanEffectPrefab;
    public int damagePerSecond = 1;
    public float damageInterval = 1f;
    private float damageTimer = 0f;

    private void Update()
    {
        if (isCleansed) return;
        if(damageTimer > 0f)damageTimer -=Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(isCleansed) return;
        if (damageTimer <= 0f)
        {
            PlayerContolor player = other.GetComponent<PlayerContolor>();
            if(player != null)
            {
                player.TakeDamage(damagePerSecond);
                damageTimer = damageInterval; // タイマーリセット
            }
        }
    }
    public void Cleanse()
    {
        if (isCleansed) return;//すでに浄化しているなら何もしない
        isCleansed = true; //浄化済みにする
        GetComponent<SpriteRenderer>().color = Color.cyan;//色を変える
        //浄化エフェクト再生
        if (cleanEffectPrefab != null )
        {
            Instantiate(cleanEffectPrefab, transform.position, Quaternion.identity);
        }

    }
}
