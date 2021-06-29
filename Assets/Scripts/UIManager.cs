using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private bool _newGameStart = false;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
       // _liveSprites[CurrentPlayerLives] = 3;
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
            Debug.LogError("GameManager is NULL");
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }
    public void GameOverDisplay()
    {
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();

    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (_newGameStart == false)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
