using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMagic : MonoBehaviour
{
    public float speed = 1.0f;  //���̑��x
    public float lifeTime = 1.0f; //���������鎞��
    public int damege = 2;
    private int direction = 1;        // ��ԕ����i1�͉E�A-1�͍��j

    public void SetDirection(int dir)
    {
        direction = dir;
    }
    void Start()
    {
        Destroy(gameObject,lifeTime); //�b��ɖ��@������
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * direction * lifeTime );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���Ǐ���
        FireWall fire = collision.GetComponent<FireWall>();�@
        if ( fire != null )
        {
            Destroy(fire.gameObject);//���ǂ�����
            Destroy(gameObject);//�����@��������
            return;
        }

        //�ŏ���
        PoisonArea poison = collision.GetComponent<PoisonArea>();
        if ( poison != null ) 
            {
                poison.Cleanse();//�ŏ��򉻃��\�b�h���Ăяo��
                Destroy(gameObject);//�����@����
                return;
            }
         //�G�Ƀ_���[�W
         EnemyController enemy = collision.GetComponent<EnemyController>();
        if ( enemy != null )
        {
            enemy.TakeDamage(damege);
            Destroy(gameObject);
        }
    }
}
