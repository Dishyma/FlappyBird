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

    [SerializeField] private GameObject trophyPrefab; // Arrastra el prefab del trofeo aquí en el Inspector
    [SerializeField] private GameObject victoryScreen; // Un nuevo GameObject para mostrar la pantalla de victoria

    [SerializeField] private Text bestScoreText;
    
    public int score { get; private set; }
    private int bestScore;


    public bool bossSpawned = false;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
        LoadBestScore();
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateBestScoreText();
    }

    private void SaveBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            UpdateBestScoreText();
        }
    }

    private void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best: " + bestScore.ToString();
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
        // Asegúrate de que la pantalla de victoria esté desactivada al inicio
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
        Pause(); // Esto pausa el juego al inicio como estaba antes
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

        UpdateBestScoreText();

        // Desactivamos todas las pantallas
        playButton.SetActive(false);
        gameOver.SetActive(false);
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
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
        SaveBestScore();
        UpdateBestScoreText();
        // Solo mostrar Game Over si no hemos ganado (no hay victoria)
        if (!victoryScreen.activeSelf)
        {
            playButton.SetActive(true);
            gameOver.SetActive(true);
            Pause();
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score > bestScore)
        {
            SaveBestScore();
        }
        if (score >= 15 && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null)
        {
            return;
        }
        bossSpawned = true;
        spawner.gameObject.SetActive(false);

        // Calcula la posición del jefe basada en la vista de la cámara
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        spawnPos.z = 0f; // Asegura que está en el mismo plano Z que el juego
        
        GameObject bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }

    public void BossDefeated()
    {
        
        SpawnTrophy();
        ShowVictoryScreen();
        // Aseguramos que la pantalla de Game Over no aparezca
        if (gameOver != null)
        {
            gameOver.SetActive(false);
        }
        if (playButton != null)
        {
            playButton.SetActive(false);
        }
    }

    private void SpawnTrophy()
    {
        if (trophyPrefab != null)
        {
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            spawnPos.z = 0f;
            Instantiate(trophyPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }
        Time.timeScale = 0f; // Pausa el juego
        player.enabled = false; // Desactiva el control del jugador
    }

    public void RestartGame()
    {
        // Oculta todas las pantallas
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
        if (gameOver != null)
        {
            gameOver.SetActive(false);
        }
        Play();
    }
}
