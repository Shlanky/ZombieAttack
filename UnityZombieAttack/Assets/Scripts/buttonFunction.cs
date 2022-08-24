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

    gameManager test;

    int tmp;

    public void resume()
    {
        if (gameModeNum == 0)
        {
            gameManager.instance.SettingsMenu.SetActive(false);
            gameManager.instance.startScreen.SetActive(true);

        }
        else
        {
            aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
            gameManager.instance.resume();
            gameManager.instance.SettingsMenu.SetActive(false);
        }

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
        tmp = gameManager.mazesCompleted;
        if (gameModeNum == 1 && tmp == 4)
        {
            SceneManager.LoadScene("Maze Sample");
            aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
            gameManager.instance.restart();
            gameManager.mazesCompleted = 0;
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
        SceneManager.LoadScene("NEWSURVIVALMAP");
        //SceneManager.LoadScene("Old Map_playground");
        //  SceneManager.LoadScene("New Scene");



        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void homeScreen()
    {
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
        gameModeNum = 0;
        SceneManager.LoadScene("ChrisMainMenuScene");

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
        SceneManager.LoadScene("Settings1");
        //gameManager.instance.pauseMenu.SetActive(false);
        //gameManager.instance.SettingsMenu.SetActive(true);
        aud.PlayOneShot(buttonClicked[Random.Range(0, buttonClicked.Length)], volume);
    }

    public void settingInGame()
    {
            gameManager.instance.pauseMenu.SetActive(false);
            gameManager.instance.SettingsMenu.SetActive(true);
    }
}
