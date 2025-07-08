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

    [Header("���@�g�p�ݒ�")]
    public float magicCooldown = 0.5f; // ���@�d������

    private bool isCasting = false;   // ���@�g�p���t���O

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private int jumpCount = 0;
    private bool isGrounded = false;

    public int maxHealth = 10;
    private int currentHealth;

    public enum MagicType { Fire, Water, Grass }
    private MagicType currentMagic = MagicType.Fire;

    private float originalJumpForce;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        originalJumpForce = jumpForce;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isCasting)
        {
            HandleMovement();
            HandleJump();
        }
        HandleMagicSwitch();
        HandleMagicCast();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1 (A), 0, 1 (D)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // �i�s�����Ɍ�����
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }  
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
        animator.SetBool("isRun",moveInput !=0);
    }

    void HandleJump()
    {
        // �n�ʔ���͊ȈՓI�ɑ��x�ő�p�i���m�ɂ�Raycast�Ȃǐ����j
        if (isGrounded && rb.velocity.y <= 0)
        {
            jumpCount = 0;
            animator.SetBool("isJump",false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            animator.SetBool("isJump",true);
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
        if (Input.GetMouseButtonDown(0) && !isCasting) // ���N���b�N
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
                StartCoroutine(CastCooldown()); // ���@�d�����J�n
                rb.velocity = Vector2.zero; //���@�����㓮���Ȃ��悤

            }
            animator.SetTrigger("attack");
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

    private IEnumerator CastCooldown() //���@�g�p��̍d��
    {
        isCasting = true; // �d���J�n
        yield return new WaitForSeconds(magicCooldown);
        isCasting = false; // �d������
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("HP�c��: " + currentHealth);
        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�v���C���[���S");
        animator.SetTrigger("die");
    }

   


    public void BoostJump(float boostedForce)
    {
        jumpForce = boostedForce;
    }

    public void ResetJump()
    {
        jumpForce = originalJumpForce;
    }

}


