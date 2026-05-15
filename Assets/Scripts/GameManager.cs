using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject Crap;
    public GameObject IceCream;
    public float maxX;
    public Transform spawnPoint;
    public float spawnRate;

    [Header("UI")]
    public GameObject startPanel;
    public GameObject quizPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;

    public enum GameState
    {
        Playing,
        Quiz,
        GameOver
    }

    public GameState state;

    int score = 0;

    float difficultyTimer = 0f;
    float minSpawnRate = 0.3f;

    void Start()
    {
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        quizPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
       
        if (state == GameState.Playing)
        {
            difficultyTimer += Time.deltaTime;

            if (difficultyTimer >= 5f)
            {
                difficultyTimer = 0f;

                spawnRate *= 0.95f;

                if (spawnRate < minSpawnRate)
                    spawnRate = minSpawnRate;

                CancelInvoke();

                InvokeRepeating("SpawnCrap", 0.5f, spawnRate);
                InvokeRepeating("SpawnIceCream", 1f, spawnRate + 1f);
            }
        }
    }

    public void StartGame()
    {
        startPanel.SetActive(false);

        state = GameState.Playing;

        Time.timeScale = 1f;

        CancelInvoke();

        InvokeRepeating("SpawnCrap", 0.5f, spawnRate);
        InvokeRepeating("SpawnIceCream", 1f, spawnRate + 1f);
    }

    void SpawnCrap()
    {
        if (state != GameState.Playing)
            return;

        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);

        Instantiate(Crap, spawnPos, Quaternion.identity);
    }

    void SpawnIceCream()
    {
        if (state != GameState.Playing)
            return;

        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);

        Instantiate(IceCream, spawnPos, Quaternion.identity);
    }

    public void StartQuiz()
    {
        state = GameState.Quiz;

        CancelInvoke();

        GameObject[] craps = GameObject.FindGameObjectsWithTag("Crap");
        foreach (GameObject crap in craps)
        {
            Destroy(crap);
        }

        GameObject[] iceCreams = GameObject.FindGameObjectsWithTag("IceCream");
        foreach (GameObject ice in iceCreams)
        {
            Destroy(ice);
        }

        quizPanel.SetActive(true);

        FindFirstObjectByType<QuizManager>().ShowRandomQuestion();
    }

    public void ResumeGame()
    {
        state = GameState.Playing;

        quizPanel.SetActive(false);

        CancelInvoke();

        InvokeRepeating("SpawnCrap", 0.5f, spawnRate);
        InvokeRepeating("SpawnIceCream", 1f, spawnRate + 1f);
    }

    public void WrongAnswer()
    {
        state = GameState.GameOver;

        quizPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}