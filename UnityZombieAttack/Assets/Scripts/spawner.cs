using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] public int numEnemiesToSpawn;
    [SerializeField] int timer;
    [SerializeField] ZombieAi enemy;
    [SerializeField] SpitterAi2 Spitter;
    [SerializeField] SprinterZomb Sprinter;
    [SerializeField] public bool thisOnePlaysSound = false;

    [Header("--------Audio----------")]
    public AudioSource aud;

    //gun shot/Famas
    [SerializeField] AudioClip[] RoundStartingSound;
    [Range(0, 1)] [SerializeField] float volume;

    public int spawnedEnemyNum;
    public int killed = 0;
    public int killGoal;
    bool canSpawn = true;
    int gameMode;
    int timesplayed;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = buttonFunction.gameModeNum;
        if (thisOnePlaysSound == true)
        {
            aud.PlayOneShot(RoundStartingSound[Random.Range(0, RoundStartingSound.Length)], volume);
        }

    }

    public IEnumerator spawnEnemy()
    {
        int enemyNum = Random.Range(1, 4);

        if (enemyNum == 1)
        {
            canSpawn = false;
            Instantiate(enemy, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
            timesplayed++;
        }

        if (enemyNum == 2)
        {
            canSpawn = false;
            Instantiate(Spitter, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
            timesplayed++;

        }

        if (enemyNum == 3)
        {
            canSpawn = false;
            Instantiate(Sprinter, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
            timesplayed++;

        }

    }

    // Update is called once per frame
    void Update()
    {


        killed = gameManager.instance.enimiesKilled;
        killGoal = gameManager.instance.enemyKillGoal;
        if (canSpawn && spawnedEnemyNum < numEnemiesToSpawn)
        {
            if (thisOnePlaysSound == true && timesplayed == 1)
            {
                aud.PlayOneShot(RoundStartingSound[Random.Range(0, RoundStartingSound.Length)], volume);
            }
            StartCoroutine(spawnEnemy());
        }
        if (canSpawn && killed >= killGoal)
        {
            timesplayed = 0;
            //statrs new round
            spawnedEnemyNum = 0;
            numEnemiesToSpawn += 2;
            gameManager.instance.enimiesKilled = 0;
            gameManager.instance.enemyKillGoal = 0;

            //if (gameMode != 1)
            //{

                Spitter.roundIncreaseBuff();
                Sprinter.roundIncreaseBuff();
                enemy.roundIncreaseBuff();
            //}
            //  call the zombie buffers
        }

    }


}
