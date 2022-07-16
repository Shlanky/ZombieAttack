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

    public GameObject heal;
    public healPickUp healScript;

    public GameObject ammo;
    public ammoPickUp ammoScript;



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
    public GameObject healPickU;

    public GameObject ammoMAxed;
    public GameObject ammoPickU;


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

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        heal = GameObject.FindGameObjectWithTag("Heal");
        ammo = GameObject.FindGameObjectWithTag("Ammo");

        playerScript = player.GetComponent<_PlayerControl>();
        ammoScript = ammo.GetComponent<ammoPickUp>();
        healScript = heal.GetComponent<healPickUp>();
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
        if (enimiesKilled >= enemyKillGoal)
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
        // resAmmoLeft -= tmp;
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
