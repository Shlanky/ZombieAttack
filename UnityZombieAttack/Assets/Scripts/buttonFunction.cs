using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunction : MonoBehaviour
{
    public static int gameModeNum;
    public AudioSource aud;

    //gun shot
    [SerializeField] AudioClip[] buttonClicked;
    [Range(0, 1)] [SerializeField] float volume;

    GameObject tmpMusic;
    Music soundsTrack;


    public void resume()
    {
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
        gameManager.instance.resume();

    }

    public void quit()
    {
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
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
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void restart()
    {

        if (gameModeNum == 1)
        {

            SceneManager.LoadScene("Maze Sample");
            aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
            gameManager.instance.restart();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
            gameManager.instance.restart();
        }

    }


    //for the main screan buttons
    public void escape()
    {
        
        gameModeNum = 1;
        SceneManager.LoadScene("Maze sample");
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void Survival()
    {
        gameModeNum = 2;
        SceneManager.LoadScene("Old map_playground");
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void homeScreen()
    {
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
        gameModeNum = 0;
        SceneManager.LoadScene("Starting Screen");

    }

    public void playGround()
    {
        gameModeNum = 3;
        SceneManager.LoadScene("ShowCase");
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void credits()
    {
        //might need to make a camera script so that it can move down like in a movie
        gameModeNum = 0;
        SceneManager.LoadScene("Credits");
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void setting()
    {
        gameModeNum = 0;
        SceneManager.LoadScene("Settings");
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void settingInGame()
    {

    }

    IEnumerator waitTimerForStartingScreen()
    {
        yield return new WaitForSeconds(5f);
    }
}
