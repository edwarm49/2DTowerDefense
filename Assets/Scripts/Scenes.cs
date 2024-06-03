using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    private Scene activeScene;
    //Functions for buttons that swtich scenes. Pretty standard stuff.
    private void Start()
    {
        activeScene = SceneManager.GetActiveScene();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main Level");
    }

    public void Help()
    {
        SceneManager.LoadScene("Help");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("Title Screen");
    }

    //This scene is scary. You don't want to see this one. Please dont click this one. It's not worth it.
    //(You should check out this scene.)
    public void Disclaimer()
    {
        SceneManager.LoadScene("Disclaimer");
    }

    private void Update()
    {
        //This is setup to not continually load the loss scene over and over
        if(GameManager.main.health <= 0)
        {
            if(activeScene.name == "Loss")
            {
                return;
            }
            else
            {
                SceneManager.LoadScene("Loss");
                GameManager.main.health = 10;
            }
        }
    }
}
