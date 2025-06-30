using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IbaraController : MonoBehaviour
{
    public GameObject burnEffect; // 燃えるときのエフェクト

    public void Burn()
    {
        Debug.Log("茨が燃えて消えた！");

        // 燃えるエフェクトを出す
        if (burnEffect != null)
        {
            Instantiate(burnEffect, transform.position, Quaternion.identity);
        }

        // 自分を破壊
        Destroy(gameObject);
    }
}
