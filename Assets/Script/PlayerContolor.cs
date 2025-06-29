using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContolor : MonoBehaviour
{
    [Header("�ړ��E�W�����v�ݒ�")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumpCount = 1;

    [Header("���@�v���n�u")]
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject grassPrefab;

    [Header("���@���ˈʒu")]
    public Transform magicSpawnPoint;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private int jumpCount = 0;
    private bool isGrounded = false;

    public enum MagicType { Fire, Water, Grass }
    private MagicType currentMagic = MagicType.Fire;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleMagicSwitch();
        HandleMagicCast();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1 (A), 0, 1 (D)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // �i�s�����Ɍ�����
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;
    }

    void HandleJump()
    {
        // �n�ʔ���͊ȈՓI�ɑ��x�ő�p�i���m�ɂ�Raycast�Ȃǐ����j
        if (isGrounded && rb.velocity.y <= 0)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    void HandleMagicSwitch()
    {
        if (Input.GetMouseButtonDown(1)) // �E�N���b�N
        {
            currentMagic = (MagicType)(((int)currentMagic + 1) % System.Enum.GetValues(typeof(MagicType)).Length);
            Debug.Log("�؂�ւ����@: " + currentMagic);
        }
    }

    void HandleMagicCast()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            GameObject prefabToSpawn = null;
            switch (currentMagic)
            {
                case MagicType.Fire:
                    prefabToSpawn = firePrefab;
                    break;
                case MagicType.Water:
                    prefabToSpawn = waterPrefab;
                    break;
                case MagicType.Grass:
                    prefabToSpawn = grassPrefab;
                    break;
            }
            if (prefabToSpawn != null && magicSpawnPoint != null)
            {
                Instantiate(prefabToSpawn, magicSpawnPoint.position, magicSpawnPoint.rotation);
            }
        }
    }

    // �ȒP�Ȓn�ʔ���iCollider2D�̐ڐG�Œn�ʔ�����Z�b�g�j
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f) // ������̐ڐG
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}


