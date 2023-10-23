using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//manages game state

public class GameManager : MonoBehaviour
{
    //simple singleton design pattern
    public static GameManager Instance {  get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed {  get; private set; }

    private Player player;
    private Spawner spawner;
    private Canvas canvas;

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
        canvas = FindObjectOfType<Canvas>();

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

        //disable the game over screen
        canvas.gameObject.SetActive(false);

        //set initial game speed
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

    }
    
    //making this us public so that it can be called when game over
    public void GameOver()
    {
        canvas.gameObject.SetActive(true);

        gameSpeed = 0f;
        //disables this script
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
    }

    private void Update()
    {
        //game speed increase as time goes by
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }

}
