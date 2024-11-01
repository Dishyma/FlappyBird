using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 5;
    public float moveSpeed = 2f;
    public float amplitude = 3f; // Amplitud del movimiento vertical
    private float initialY;

    // Nuevas variables para la animación
    public Sprite[] bossSprites;  // Arreglo para almacenar los 4 sprites
    private int currentSpriteIndex = 0;
    public float animationSpeed = 0.2f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        initialY = transform.position.y;
    
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        StartCoroutine(AnimateSprites());
    }

    void Update()
    {
        // Movimiento vertical sinusoidal
        float newY = initialY + Mathf.Sin(Time.time * moveSpeed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    IEnumerator AnimateSprites()
    {
        while (true)
        {
            if (bossSprites.Length > 0)
            {
                spriteRenderer.sprite = bossSprites[currentSpriteIndex];
                currentSpriteIndex = (currentSpriteIndex + 1) % bossSprites.Length;
            }
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameManager.Instance.IncreaseScore();
        if (health <= 0)
        {
            GameManager.Instance.BossDefeated();
            Destroy(gameObject);
        }
    }
}