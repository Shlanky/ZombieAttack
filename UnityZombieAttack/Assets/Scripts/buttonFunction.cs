using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour
{
    public static int gameModeNum;

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
        gameModeNum = 1;
        
        SceneManager.LoadScene("Maze sample");
    }

    public void Survival()
    {
        gameModeNum = 2;
        SceneManager.LoadScene("Old map_playground");
    }

    public void homeScreen()
    {
        gameModeNum = 0;
        SceneManager.LoadScene("Starting Screen");
    }
  
    public void playGround()
    {
        gameModeNum = 3;
        SceneManager.LoadScene("ShowCase");
    }

    public void credits()
    {
        //might need to make a camera script so that it can move down like in a movie
        gameModeNum = 4;
        SceneManager.LoadScene("Credits");
    }

    public void setting()
    {
        gameModeNum = 5;
        SceneManager.LoadScene("Settings");
    }
}
