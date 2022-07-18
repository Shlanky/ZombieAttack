using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("------UI-------")]
    public static gameManager instance;
    public GameObject player;
    public _PlayerControl playerScript;

    [Header("------UI-------")]

    public GameObject pauseMenu;
    public GameObject playerDeadMenu;
    public GameObject winGameMenu;
    public GameObject playerDamageFlash;
    public GameObject reload_txt;
    public GameObject noAmmo;
    public Image HPBar;
    public TMP_Text enemyDead;
    public TMP_Text enemyTotal;
    public TMP_Text keyCount;


    public TMP_Text AmmoMag;
    public TMP_Text AmmoRes;

    public GameObject healMaxedMSG;

    public GameObject ammoMAxed;

    public GameObject noKeys;
    public GameObject Key_1;
    public GameObject Key_2;

    public GameObject jobsNotDoneMsg;


    [HideInInspector] public bool paused = false;
    [HideInInspector] public bool gameOver;


    public GameObject menuCurrentlyOpen;
    [Header("------Game Goals-------")]

    //for enemy ui
    public int enemyKillGoal;
    int enimiesKilled;

    //for ammo ui
    int magAmmoLeft;
    int resAmmoLeft;

    //for key ui
    int keyPickedUp;

    //true = kill enemys
    //false = kill enemies and escape 
    //bool game_mode = true;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");

        playerScript = player.GetComponent<_PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel") && !gameOver)
        {
            if (!paused && !menuCurrentlyOpen)
            {
                paused = true;
                menuCurrentlyOpen = pauseMenu;
                menuCurrentlyOpen.SetActive(true);
                lockCursorPause();
            }
            else
            {
                resume();
            }
        }

    }

    public void resume()
    {
        paused = false;
        menuCurrentlyOpen.SetActive(false);
        menuCurrentlyOpen = null;
        unlockCursorUnpause();
    }


    public void playerDead()
    {
        gameOver = true;
        menuCurrentlyOpen = playerDeadMenu;
        menuCurrentlyOpen.SetActive(true);
        lockCursorPause();
    }

    public void checkEnemyKills()
    {
        enimiesKilled++;
        enemyDead.text = enimiesKilled.ToString("F0");
        //if (enimiesKilled >= enemyKillGoal && game_mode == true)
        //{
        //    menuCurrentlyOpen = winGameMenu;
        //    menuCurrentlyOpen.SetActive(true);
        //    gameOver = true;
        //    lockCursorPause();

        //}
        //else
        //{
        //    StartCoroutine(jobsNotDone());
        //}

        if (enimiesKilled >= enemyKillGoal)
        {
            StartCoroutine(jobsNotDone());
        }
    }

    IEnumerator jobsNotDone()
    {
        jobsNotDoneMsg.SetActive(true);
        yield return new WaitForSeconds(2f);
        jobsNotDoneMsg.SetActive(false);
    }

    public void checkKeysForWin(bool check)
    {
        if (check == true)
        {
            //show win screen
            menuCurrentlyOpen = winGameMenu;
            menuCurrentlyOpen.SetActive(true);
            gameOver = true;
            lockCursorPause();
        }
    }

    public void restart()
    {
        gameOver = false;
        menuCurrentlyOpen.SetActive(false);
        menuCurrentlyOpen = null;
        unlockCursorUnpause();
    }


    public void lockCursorPause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void unlockCursorUnpause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void updateEnemyNumber()
    {
        enemyKillGoal++;
        enemyTotal.text = enemyKillGoal.ToString("F0");
    }

    //for ammo ui
    public void updateMagCount()
    {
        magAmmoLeft = 29;
        magAmmoLeft++;
        AmmoMag.text = magAmmoLeft.ToString("F0");

    }

    public void shot()
    {
        magAmmoLeft--;
        AmmoMag.text = magAmmoLeft.ToString("F0");
    }

    public void resetAmmoMagCount()
    {
        magAmmoLeft = 30;
        AmmoMag.text = magAmmoLeft.ToString("F0");
    }


    //for ui of reserve
    public void updateReserveCount()
    {
        resAmmoLeft = 180;
        AmmoRes.text = resAmmoLeft.ToString("F0");
    }

    //needs a little work but otherwise works
    public void reload()
    {
        int tmp = gameManager.instance.playerScript.roundsShot;


        if (resAmmoLeft < 30)
        {
            resAmmoLeft -= tmp;
            AmmoRes.text = resAmmoLeft.ToString("F0");
        }
        else
        {
            resAmmoLeft -= tmp;
            AmmoRes.text = resAmmoLeft.ToString("F0");
        }
    }


    public void pickUpAmmo()
    {
        if (resAmmoLeft < 180)
        {
            resAmmoLeft += 30;
            AmmoRes.text = resAmmoLeft.ToString("F0");
        }
    }

    //add one for key in morning
    public void pickUpKey()
    {
        keyPickedUp++;
        keyCount.text = keyPickedUp.ToString("F0");
    }

}
