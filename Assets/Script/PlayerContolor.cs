using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContolor : MonoBehaviour
{
    [Header("移動・ジャンプ設定")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumpCount = 1;

    [Header("魔法プレハブ")]
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject grassPrefab;

    [Header("魔法発射位置")]
    public Transform magicSpawnPoint;

    [Header("魔法使用設定")]
    public float magicCooldown = 0.5f; // 魔法硬直時間

    private bool isCasting = false;   // 魔法使用中フラグ

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

        // 進行方向に向ける
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
        // 地面判定は簡易的に速度で代用（正確にはRaycastなど推奨）
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
        if (Input.GetMouseButtonDown(1)) // 右クリック
        {
            currentMagic = (MagicType)(((int)currentMagic + 1) % System.Enum.GetValues(typeof(MagicType)).Length);
            Debug.Log("切り替え魔法: " + currentMagic);
        }
    }

    void HandleMagicCast()
    {
        if (Input.GetMouseButtonDown(0) && !isCasting) // 左クリック
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
                StartCoroutine(CastCooldown()); // 魔法硬直を開始
                rb.velocity = Vector2.zero; //魔法発動後動かないよう

            }
            animator.SetTrigger("attack");
        }
    }

    // 簡単な地面判定（Collider2Dの接触で地面判定をセット）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f) // 上向きの接触
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private IEnumerator CastCooldown() //魔法使用後の硬直
    {
        isCasting = true; // 硬直開始
        yield return new WaitForSeconds(magicCooldown);
        isCasting = false; // 硬直解除
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("HP残り: " + currentHealth);
        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("プレイヤー死亡");
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


