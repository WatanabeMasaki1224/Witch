using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public int damage = 1; // �v���C���[�ɗ^����_���[�W
    public float burnDuration = 1.5f; // �R���鎞�ԁi�����ډ��o�j
    public GameObject burnEffectPrefab; // ���̃G�t�F�N�g�i�C�Ӂj

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
        if (isBurned) return; // �R������͂Ȃɂ����Ȃ�

        // �v���C���[���G�ꂽ��_���[�W
        PlayerContolor player = collision.GetComponent<PlayerContolor>();
        if (player != null)
        {
            player.TakeDamage(damage); 
        }
    }

    // �����@����Ă΂�鏈��
    public void Burn()
    {
        if (isBurned) return;
        isBurned = true;
        Debug.Log("Vine is burning!!"); 

        // �R����G�t�F�N�g
        if (burnEffectPrefab != null)
        {
            Instantiate(burnEffectPrefab, transform.position, Quaternion.identity);
        }

        // �R���C�_�[���I�t �� �ʂ��悤�ɂȂ�
        col.enabled = false;

        // �����ɂ��ĔR���鉉�o �� �Ō�͍폜
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

        Destroy(gameObject); // ���S�ɍ폜�i�C�Ӂj
    }
}
