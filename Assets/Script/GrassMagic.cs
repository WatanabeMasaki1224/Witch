using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMagic : MonoBehaviour
{
    public float spawnDistance = 2f;
    public GameObject grassPrefab;
    public LayerMask groundLayer;
    private int direction = 1;

    public void SetDirection(int dir)
    {
        direction = dir;
    }
    void Start()
    {
        Vector2 tagetPos = transform.position + new Vector3(direction *  spawnDistance, 0.5f, 0);
        RaycastHit2D hit = Physics2D.Raycast(tagetPos, Vector2.down,1f,groundLayer);
        if(hit.collider != null)
        {
            Instantiate(grassPrefab, hit.point, Quaternion.identity);
        }

        Destroy(gameObject);
    }

}
