using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IbaraController : MonoBehaviour
{
    public GameObject burnEffect; // �R����Ƃ��̃G�t�F�N�g

    public void Burn()
    {
        Debug.Log("��R���ď������I");

        // �R����G�t�F�N�g���o��
        if (burnEffect != null)
        {
            Instantiate(burnEffect, transform.position, Quaternion.identity);
        }

        // ������j��
        Destroy(gameObject);
    }
}
