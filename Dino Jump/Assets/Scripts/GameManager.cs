
using TMPro;

using UnityEngine;
using UnityEngine.UI;

//manages game state

public class GameManager : MonoBehaviour
{
    //simple singleton design pattern
    public static GameManager Instance {  get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed {  get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;
    public Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //makes sure that only one instance exist
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        //clears the obstacles
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        //set the hi score
        UpdateHiScore();

        //set initial game speed
        gameSpeed = initialGameSpeed;
        enabled = true;
        score = 0f;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

    }
    
    //making this us public so that it can be called when game over
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        gameSpeed = 0f;
        //disables this script
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        UpdateHiScore();
    }

    private void Update()
    {
        //game speed increase as time goes by
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

        score += gameSpeed * Time.deltaTime;
        //round and convert the score to string, also adding a format that has 5 digits
        //https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");
    }
    private void UpdateHiScore() 
    {
        //get the saved hi score if not found put a default value
        float hiScore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetFloat("hiscore", hiScore); //update the hi score
        }

        hiScoreText.text = Mathf.RoundToInt(hiScore).ToString("D5");
    }

}
