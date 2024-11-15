﻿using UnityEngine;
namespace MyGame
{

    public class Player : MonoBehaviour
    {
        public Sprite[] sprites;
        public float strength = 5f;
        public float gravity = -9.81f;
        public float tilt = 5f;

        private SpriteRenderer spriteRenderer;
        private Vector3 direction;
        private int spriteIndex;

        [Header("Laser Settings")]
        public GameObject laserPrefab;
        public float laserCooldown = 0.5f;
        private float lastLaserTime;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        }

        private void OnEnable()
        {
            Vector3 position = transform.position;
            position.y = 0f;
            transform.position = position;
            direction = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                direction = Vector3.up * strength;
            }

            // Apply gravity and update the position
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;

            // Tilt the bird based on the direction
            Vector3 rotation = transform.eulerAngles;
            rotation.z = direction.y * tilt;
            transform.eulerAngles = rotation;

            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("Intentando disparar láser"); // Añade esta línea
                ShootLaser();
            }
        }

        private void AnimateSprite()
        {
            spriteIndex++;

            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }

            if (spriteIndex < sprites.Length && spriteIndex >= 0)
            {
                spriteRenderer.sprite = sprites[spriteIndex];
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameManager.Instance.GameOver();
            }
            else if (other.gameObject.CompareTag("Scoring"))
            {
                GameManager.Instance.IncreaseScore();
            }
        }

        private void ShootLaser()
        {
            Debug.Log("ShootLaser llamado");
            if (Time.time - lastLaserTime >= laserCooldown)
            {
                if (laserPrefab != null)
                {
                    Debug.Log("Instanciando láser");
                    lastLaserTime = Time.time;
                    Vector3 spawnPosition = transform.position + Vector3.right;
                    GameObject laser = Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
                    Debug.Log("Láser instanciado: " + (laser != null));
                }
                else
                {
                    Debug.LogError("El prefab del láser no está asignado");
                }
            }
            else
            {
                Debug.Log("Esperando cooldown del láser");
            }
        }
    }

}