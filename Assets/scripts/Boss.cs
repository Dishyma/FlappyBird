using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 5;
    public float moveSpeed = 2f;
    public float amplitude = 2f; // Amplitud del movimiento vertical
    private float initialY;

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        // Movimiento vertical sinusoidal
        float newY = initialY + Mathf.Sin(Time.time * moveSpeed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.BossDefeated();
        }
    }
}