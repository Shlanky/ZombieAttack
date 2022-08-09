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

    public GameObject Tank_Icon;
    public GameObject Jump_Icon;
    public GameObject Damage_Icon;

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

    //maze text
    public GameObject EscapeNow;

    [HideInInspector] public bool paused = false;
    [HideInInspector] public bool gameOver;


    public GameObject menuCurrentlyOpen;
    [Header("------Game Goals-------")]


    //for enemy ui
    public int enemyKillGoal;
    public int enimiesKilled = 0;

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

    //for rounds/survival
    int rounds;


    int gameModeHolder;


    //testing stuff with the spawner
    spawner test;

    [Header("------Transitions-------")]
    public GameObject startTransition;
    public GameObject Maze_enter;
    public GameObject Maze_finish;
    public GameObject Survival_Start;
    public GameObject PlayGround;


    // Start is called before the first frame update
    void Awake()
    {


        //this fixed the pause bug
        gameModeHolder = buttonFunction.gameModeNum;
        if (gameModeHolder == 0)
        {
           // startTransition.SetActive(true);
        }
        if (gameModeHolder == 1)
        {
            Maze_enter.SetActive(true);
        }
        if (gameModeHolder == 2)
        {
            Survival_Start.SetActive(true);
        }
        if (gameModeHolder == 3)
        {
            PlayGround.SetActive(true);
        }

        if (gameModeHolder > 0)
        {
            unlockCursorUnpause();
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player");

            playerScript = player.GetComponent<_PlayerControl>();

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (gameModeHolder > 0)
        {
            magAmmoLeft = gameManager.instance.playerScript.roundsInMag;
            resAmmoLeft = gameManager.instance.playerScript.roundsInReserve;


            //may need to put this outside the if statemtn if theres an issue w the main menu
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
            //StartCoroutine(jobsNotDone());
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
        if (check == true && mazesCompleted == 5)
        {
            // show win screen
            menuCurrentlyOpen = winGameMenu;
            menuCurrentlyOpen.SetActive(true);
            gameOver = true;
            lockCursorPause();
        }

        else if (mazesCompleted < 5)
        {
          
            moveUpLevel();
        }
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

        if (mazesCompleted == 3)
        {
            SceneManager.LoadScene("Maze 5");
            mazesCompleted++;
            return mazesCompleted;
        }

        if (mazesCompleted == 4)
        {
            SceneManager.LoadScene("Maze 6");
            mazesCompleted++;
            return mazesCompleted;
        }

        return 0;
    }

    IEnumerator exitTransition()
    {
        Maze_finish.SetActive(true);

        yield return new WaitForSeconds(2);

        Maze_finish.SetActive(false);
    }
}
