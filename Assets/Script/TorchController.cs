using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public Light torchLight;
    public GameObject door;
    private bool isLit = false;
    // Start is called before the first frame update
    void Start()
    {
     torchLight.enabled = false;   
    }
    public void Ignite()
    {
        if(isLit)
        {
            return;
        }
        isLit = true;

        if(torchLight != null)
        {
            torchLight.enabled = true;
        }

        if(door != null)
        {
            Door doorScript = door.GetComponent<Door>();
            if(doorScript != null )
            {
                doorScript.OpenDoor();
            }

        }
        // �����܂ɉ΂����鏈��
        Debug.Log("�����܂ɉ΂������I");
        // flameEffect.SetActive(true); �Ȃ�
    }
}
