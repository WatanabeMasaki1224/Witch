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

        // 進行方向に向ける
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;
    }

    void HandleJump()
    {
        // 地面判定は簡易的に速度で代用（正確にはRaycastなど推奨）
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
        if (Input.GetMouseButtonDown(1)) // 右クリック
        {
            currentMagic = (MagicType)(((int)currentMagic + 1) % System.Enum.GetValues(typeof(MagicType)).Length);
            Debug.Log("切り替え魔法: " + currentMagic);
        }
    }

    void HandleMagicCast()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
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
}


