using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    
    void Start()
    {
        Debug.Log("Láser creado en posición: " + transform.position); // Debug para verificar creación
    }
    
    void Update()
    {
        // Mueve el láser hacia la derecha
        transform.position += Vector3.right * speed * Time.deltaTime;
        Debug.Log("Posición actual del láser: " + transform.position); // Debug para seguir posición

        // Destruye el láser cuando sale de la pantalla
        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 2f)
        {
            Debug.Log("Destruyendo láser");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}