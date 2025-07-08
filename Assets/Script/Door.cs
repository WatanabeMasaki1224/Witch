using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    private Collider2D doorCollider;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (isOpen)
        {
            return;
        }
        isOpen = true;
        //通れなくする壁のcolliderを無効か
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
        //ドアが開くアニメーションがあれば再開
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
