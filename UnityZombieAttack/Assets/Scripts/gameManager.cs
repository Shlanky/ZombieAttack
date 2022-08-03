using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject WallBuyTxt;
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

    public TMP_Text Points;

    //perks
    public GameObject TankTxt;
    public GameObject JumpTxt;
    public GameObject DamageTxt;

    //power up text test
    public GameObject RocketPowertst;
    public GameObject Healertxt;
    public GameObject FoBTxt;
    public GameObject MoneyRushtxt;

    //weapon info
    public GameObject AK47_msg;
    public GameObject FaMas_msg;
    public GameObject Ghost_msg;
    public GameObject M16_msg;
    public GameObject M1911_msg;
    public GameObject MP5_msg;
    public GameObject Revolver_msg;
    public GameObject Uzi_msg;

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

    //for ammo og 
    int ogMagCount;
    int ogResCount;

    //for key ui
    int keyPickedUp;

    //for points
    int points;

    //for the maze counter to make sure its going to the right maze
    static int mazesCompleted;

    //buttonFunction GameModeNum;
    //int tmpGMholder;

    //for rounds/survival
    int rounds;

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
        //get the amount of ammo so its the same in the ui
        //ogMagCount = gameManager.instance.playerScript.roundsInMag;
        //ogResCount = gameManager.instance.playerScript.roundsInReserve;

        //  tmpGMholder = GameModeNum.gameMode;

        magAmmoLeft = gameManager.instance.playerScript.roundsInMag;
        resAmmoLeft = gameManager.instance.playerScript.roundsInReserve;

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

        //uncomment this when im done with writing the scripts for gamemode
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

    //this works a lil better for the updating the ui
    //point ui update
    public void updatePoints()
    {
        points = gameManager.instance.playerScript.points;
        Points.text = points.ToString("F0");
    }


    //ammo 
    //needs a little work but otherwise works
    public void reload()
    {

        //try to use the gamemanager.instance.playscript.(the og number here maybe)
        //then set the resammo left to the gamemanager thing 
        int tmp = gameManager.instance.playerScript.roundsShot;

        //think the negative bug is in not sure tho
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
        //  updateReserveCount();
    }


    public void pickUpAmmo()
    {
        if (resAmmoLeft <= 150)
        {
            resAmmoLeft += 30;
            AmmoRes.text = resAmmoLeft.ToString("F0");
        }
        else if (resAmmoLeft > 150)
        {
            resAmmoLeft += 180 - resAmmoLeft;
            AmmoRes.text = resAmmoLeft.ToString("F0");
        }
    }

    public void updateMagCount()
    {
        magAmmoLeft = gameManager.instance.playerScript.roundsInMag;
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

    //still goes into the negatives need to find where thats happening
    //for ui of reserve
    public void updateReserveCount()
    {
        resAmmoLeft = gameManager.instance.playerScript.roundsInReserve;
        AmmoRes.text = resAmmoLeft.ToString("F0");
    }

    //key
    public void pickUpKey()
    {
        keyPickedUp++;
        keyCount.text = keyPickedUp.ToString("F0");
    }

    //this will move to the differant scenes
    public void checkKeysForWin(bool check)
    {
        if (check == true && mazesCompleted == 4)
        {
            // show win screen
            menuCurrentlyOpen = winGameMenu;
            menuCurrentlyOpen.SetActive(true);
            gameOver = true;
            lockCursorPause();
        }

        else if (mazesCompleted < 4)
        {
            moveUpLevel();
        }
        // if (num == 1)
        // {
        //     SceneManager.LoadScene("Maze 2");
        // }

        //else if (num == 2)
        // {
        //     SceneManager.LoadScene("Maze 3");
        // }

        //else if (num == 3)
        // {
        //     SceneManager.LoadScene("Maze 4");
        // }

        //else if (num == 4)
        // {
        //     menuCurrentlyOpen = winGameMenu;
        //     menuCurrentlyOpen.SetActive(true);
        //     gameOver = true;
        //     lockCursorPause();
        // }
        ////add gamemode here to chack if this is eligible
        //if (check == true)
        //{
        //    mazesCompleted = mazesCompleted + 1;

        //    if (mazesCompleted == 1)
        //    {
        //        SceneManager.LoadScene("Maze 2");
        //        //might need to rest the key count here to 0
        //    }

        //    if (mazesCompleted == 2)
        //    {
        //        SceneManager.LoadScene("Maze 3");
        //    }

        //    if (mazesCompleted == 3)
        //    {
        //        SceneManager.LoadScene("Maze 4");
        //    }

        //    if (mazesCompleted == 4)
        //    {
        //        //show win screen
        //        menuCurrentlyOpen = winGameMenu;
        //        menuCurrentlyOpen.SetActive(true);
        //        gameOver = true;
        //        lockCursorPause();
        //    }

        //}
    }

    public int moveUpLevel()
    {
        if (mazesCompleted == 0)
        {
            SceneManager.LoadScene("Maze 2");
            mazesCompleted++;
            return mazesCompleted;
        }

        if (mazesCompleted == 1)
        {
            SceneManager.LoadScene("Maze 3");
            mazesCompleted++;
            return mazesCompleted;
        }

        if (mazesCompleted == 2)
        {
            SceneManager.LoadScene("Maze 4");
            mazesCompleted++;
            return mazesCompleted;
        }

        return 0;
    }

}
