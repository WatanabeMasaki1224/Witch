using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    private bool isCleansed = false;  //�ŏ����򉻂���Ă��邩
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
                damageTimer = damageInterval; // �^�C�}�[���Z�b�g
            }
        }
    }
    public void Cleanse()
    {
        if (isCleansed) return;//���łɏ򉻂��Ă���Ȃ牽�����Ȃ�
        isCleansed = true; //�򉻍ς݂ɂ���
        GetComponent<SpriteRenderer>().color = Color.cyan;//�F��ς���
        //�򉻃G�t�F�N�g�Đ�
        if (cleanEffectPrefab != null )
        {
            Instantiate(cleanEffectPrefab, transform.position, Quaternion.identity);
        }

    }
}
