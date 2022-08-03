using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour
{

    public int gameMode = 0;

    public void resume()
    {
        gameManager.instance.resume();
    }

    public void quit()
    {
        Application.Quit();
    }

    public void givePlayerHP(int amount)
    {
        gameManager.instance.playerScript.giveHP(amount);
    }
    
    public void respawn()
    {
        gameManager.instance.playerScript.respawn();
        gameManager.instance.restart();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.restart();
    }


    //for the main screan buttons
    public void escape()
    {
        gameMode = 1;
        SceneManager.LoadScene("Maze sample");
    }

    public void Survival()
    {
        gameMode = 2;
        SceneManager.LoadScene("Old map_playground");
    }

    public void homeScreen()
    {
        gameMode = 0;
        SceneManager.LoadScene("Starting Screen");
    }
  
    public void setting()
    {
      //  SceneManager.LoadScene();
    }
}
