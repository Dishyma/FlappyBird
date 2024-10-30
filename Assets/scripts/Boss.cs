using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 10;
    public float moveSpeed = 2f;

    void Update()
    {
        // Implementa el movimiento del jefe
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
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