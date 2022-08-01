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

    public void kill()
    {
        gameMode = 1;
        //get the kill scene here
    }

    public void escape()
    {
        gameMode = 2;
        //get the escape scene here
        }

    public void Survival()
    {
        gameMode = 3;
        //get the surviuaval sceene here
    }
}
