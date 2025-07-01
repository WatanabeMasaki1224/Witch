using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMagic : MonoBehaviour
{
    public float speed = 1.0f;  //水の速度
    public float lifeTime = 1.0f; //水が消える時間
    public int damege = 2;
    private int direction = 1;        // 飛ぶ方向（1は右、-1は左）

    public void SetDirection(int dir)
    {
        direction = dir;
    }
    void Start()
    {
        Destroy(gameObject,lifeTime); //秒後に魔法を消す
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * direction * lifeTime );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //炎壁消す
        FireWall fire = collision.GetComponent<FireWall>();　
        if ( fire != null )
        {
            Destroy(fire.gameObject);//炎壁を消す
            Destroy(gameObject);//水魔法が消える
            return;
        }

        //毒沼浄化
        PoisonArea poison = collision.GetComponent<PoisonArea>();
        if ( poison != null ) 
            {
                poison.Cleanse();//毒沼浄化メソッドを呼び出す
                Destroy(gameObject);//水魔法消す
                return;
            }
         //敵にダメージ
         EnemyController enemy = collision.GetComponent<EnemyController>();
        if ( enemy != null )
        {
            enemy.TakeDamage(damege);
            Destroy(gameObject);
        }
    }
}
