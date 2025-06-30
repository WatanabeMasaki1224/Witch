using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    public float lifeTime = 1f; //���������鎞��
    public int damage = 3;  //���̃_���[�W��
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime); ;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�G�Ƀ_���[�W
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        //�����܂ɉ΂�����
        TorchController torch = collision.GetComponent<TorchController>();
        if(torch != null)
        {
            torch.Ignite();
        }
        // ���R�₷�I
        IbaraController ibara = collision.GetComponent<IbaraController>();
        if (ibara != null)
        {
            ibara.Burn(); // �� �����ň�R����
        }
    }
      
}
