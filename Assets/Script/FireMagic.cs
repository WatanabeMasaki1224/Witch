using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    public float lifeTime = 1f; //炎が消える時間
    public int damage = 3;  //炎のダメージ量
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime); ;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵にダメージ
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        //たいまつに火をつける
        TorchController torch = collision.GetComponent<TorchController>();
        if(torch != null)
        {
            torch.Ignite();
        }
        // 茨を燃やす！
        IbaraController ibara = collision.GetComponent<IbaraController>();
        if (ibara != null)
        {
            ibara.Burn(); // ← ここで茨が燃える
        }
    }
      
}
