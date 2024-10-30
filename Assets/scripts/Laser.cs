using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;
    
    void Update()
    {
        // Mueve el láser hacia la derecha
        transform.position += Vector3.right * speed * Time.deltaTime;

        // Destruye el láser cuando sale de la pantalla
        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si golpea al jefe u otro enemigo
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            // Aquí puedes añadir lógica de daño
            Destroy(gameObject);
        }
    }
}