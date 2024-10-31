using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    
    [SerializeField] private GameObject bossPrefab; // Arrastra el prefab del jefe aquí en el Inspector
    [SerializeField] private Vector3 bossSpawnPosition = new Vector3(10f, 0f, 0f); // Posición donde aparecerá el jefe
    public bool bossSpawned = false;

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }

        // Asegúrate de que el jefe no esté presente al inicio del juego
        GameObject existingBoss = GameObject.FindGameObjectWithTag("Boss");
        if (existingBoss != null) {
            Destroy(existingBoss);
        }
        bossSpawned = false;
        spawner.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        Debug.Log($"Score incrementado a: {score}");

        if (score >= 10 && !bossSpawned)
        {
            Debug.Log("Score llegó a 10, llamando a SpawnBoss()");
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null)
        {
            Debug.LogError("Error: Boss Prefab no está asignado en el GameManager");
            return;
        }
        Debug.Log("Iniciando spawn del boss");
        bossSpawned = true;
        spawner.gameObject.SetActive(false);

        // Calcula la posición del jefe basada en la vista de la cámara
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        spawnPos.z = 0f; // Asegura que está en el mismo plano Z que el juego
        
        GameObject bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        Debug.Log($"Boss spawneado en posición: {spawnPos}");
    }

    public void BossDefeated()
    {
        // Implementa lo que sucede cuando el jefe es derrotado
        Debug.Log("¡Jefe derrotado! ¡Has ganado!");
        GameOver();
    }
}
