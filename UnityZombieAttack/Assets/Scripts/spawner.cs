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

    public int spawnedEnemyNum;
    public int killed = 0;
    public int killGoal;
    bool canSpawn = true;


    // Start is called before the first frame update
    void Start()
    {

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
        }

        if (enemyNum == 2)
        {
            canSpawn = false;
            Instantiate(Spitter, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
        }

        if (enemyNum == 3)
        {
            canSpawn = false;
            Instantiate(Sprinter, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        killed = gameManager.instance.enimiesKilled;
        killGoal = gameManager.instance.enemyKillGoal;
        if (canSpawn && spawnedEnemyNum < numEnemiesToSpawn)
        {
            StartCoroutine(spawnEnemy());
        }
        if (canSpawn && killed >= killGoal)
        {
            //statrs new round
            spawnedEnemyNum = 0;
            numEnemiesToSpawn += 2;
            gameManager.instance.enimiesKilled = 0;
            gameManager.instance.enemyKillGoal = 0;
            gameManager.instance.RoundCounter();

            Spitter.roundIncreaseBuff();
            Sprinter.roundIncreaseBuff();
            enemy.roundIncreaseBuff();
            //  call the zombie buffers
        }
    }


}
