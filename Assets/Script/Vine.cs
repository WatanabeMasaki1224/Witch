using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public int damage = 1; // プレイヤーに与えるダメージ
    public float burnDuration = 1.5f; // 燃える時間（見た目演出）
    public GameObject burnEffectPrefab; // 炎のエフェクト（任意）

    private Collider2D col;
    private SpriteRenderer sr;
    private bool isBurned = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBurned) return; // 燃えた後はなにもしない

        // プレイヤーが触れたらダメージ
        PlayerContolor player = collision.GetComponent<PlayerContolor>();
        if (player != null)
        {
            player.TakeDamage(damage); 
        }
    }

    // 炎魔法から呼ばれる処理
    public void Burn()
    {
        if (isBurned) return;
        isBurned = true;
        Debug.Log("Vine is burning!!"); 

        // 燃えるエフェクト
        if (burnEffectPrefab != null)
        {
            Instantiate(burnEffectPrefab, transform.position, Quaternion.identity);
        }

        // コライダーをオフ → 通れるようになる
        col.enabled = false;

        // 透明にして燃える演出 → 最後は削除
        StartCoroutine(BurnAndDisappear());
    }

    private IEnumerator BurnAndDisappear()
    {
        float time = 0f;
        Color originalColor = sr.color;

        while (time < burnDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, time / burnDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject); // 完全に削除（任意）
    }
}
