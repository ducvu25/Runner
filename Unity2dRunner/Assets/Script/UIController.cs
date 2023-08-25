using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    GameController gameController;
    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] GameObject menuGame;
    [SerializeField] GameObject startGame;
    [SerializeField] TextMeshProUGUI[] txtScoreEnd;

    private void Start()
    {
        gameController = GameController.Instance;
        startGame.SetActive(true);
        menuGame.SetActive(false);
        gameController.endGame.AddListener(EndGame);
    }
    private void Update()
    {
        if(startGame.activeSelf || menuGame.activeSelf) { return; }
        txtScore.text = "Score: " + gameController.Score().ToString();
    }
    public void StartGame()
    {
        gameController.NewGame();
        menuGame.SetActive(false);
        startGame.SetActive(false);
        txtScore.gameObject.SetActive(true);
    }
    public void EndGame()
    {
        menuGame.SetActive(true);
        txtScore.gameObject.SetActive(false);
        txtScoreEnd[0].text = "Your score: " + gameController.Score().ToString();
        txtScoreEnd[1].text = "Hight Score: " + Mathf.RoundToInt(gameController.data.score).ToString();
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
