using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { loading, inGame, gameOver }
    public GameState gameState;
    public List<GameObject> targetPrefabs;
    public GameObject titleScreen, panelGameOver;
    public Text scoreText;
    public List<GameObject> lives;


    private float spawnRate = 1.5f;
    private int numberOfLives = 4;
    private int _score;
    private int Score
    {
        set 
        {
            _score = Mathf.Max(value,0);
        }
        get 
        {
            return _score;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
       ShowMaxScore();
       gameState = GameState.loading;
    }
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score: " + Score;
    }

    private const string MAX_SCORE = "MAX_SCORE";
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "Max Score: " + maxScore;
    }
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        titleScreen.SetActive(false);

        for (int i =0; i< numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }
        Score = 0;
        UpdateScore(0);
    }
    // Update is called once per frame
    void Update()
    {
            
    }
        public void GameOver()
    {
        numberOfLives--;
        if (numberOfLives >=0)
        {
            Image heartImage = lives[numberOfLives].GetComponent<Image>();
            var tempColor = heartImage.color;
            tempColor.a = 0.3f;
            heartImage.color = tempColor;
        }
        if(numberOfLives <=0)
        {
            SetMaxScore();
            gameState = GameState.gameOver;
            panelGameOver.SetActive(true);
        }
    }
    IEnumerator SpawnTarget()
    {
       while (gameState == GameState.inGame)
       {
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0, targetPrefabs.Count);
        Instantiate(targetPrefabs[index]);
       }
    }
    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0 );
        if (Score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, Score);
        }
    }
public void RestartScene()
{
    SceneManager.LoadScene(0);
}
public void RestartScore()
{
    PlayerPrefs.SetInt(MAX_SCORE, 0);
}
}