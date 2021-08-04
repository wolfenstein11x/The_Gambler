using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMatchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadTownScene()
    {
        SceneManager.LoadScene("TownScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetPlayerMoney()
    {
        // set money to starting amount
        PlayerData.playerTotalMoney = 30f;
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void LoadControlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
