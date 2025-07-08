using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowt : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0,0,-10f);
    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            offset = transform.position - player.position;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }

    
}
