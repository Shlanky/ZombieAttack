using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour, iDamageable
{
    [Header("Components")]

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rend;
    [SerializeField] Animator anim;

    [Header("----------------------------------")]
    [Header("Enemy Attributes")]
    [SerializeField] int HP;
    [SerializeField] int viewAngle;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int roamRadius;

    [Header("----------------------------------")]
    [Header("Weapon Stats")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootPos;

    [Header("----------------------------------")]
    [Header("Power Ups")]
    //[SerializeField] OneShotOneKIll damageDrop;
    //[SerializeField] doublePoints MoneyRush;
    //[SerializeField] Healer heal;
    //[SerializeField] FullOfBullets ammo;

    bool canShoot;
    [SerializeField] bool playerInRange;
    Vector3 playerDir;
    Vector3 startingPos;
    float StoppingDistOrig;

    int shotPoints;
    int killPoints;

    buttonFunction GameModeNum;
    int GameMode;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        StoppingDistOrig = agent.stoppingDistance;


        gameManager.instance.updateEnemyNumber();
      //  GameMode = GameModeNum.gameMode;
    }

    // Update is called once per frame
    void Update()
    {

        shotPoints = gameManager.instance.playerScript.shotPoint;
        killPoints = gameManager.instance.playerScript.killPoint;
        if (agent.isActiveAndEnabled)
        {

            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * 5));

            playerDir = gameManager.instance.player.transform.position - transform.position;
            if (playerInRange)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                canSeePlayer();
                facePlayer();
            }
            else if (agent.remainingDistance < 0.1f)
                StartCoroutine(roam());
        }
    }
    IEnumerator roam()
    {
        agent.stoppingDistance = 0;
        Vector3 randomDir = Random.insideUnitSphere * roamRadius;
        randomDir += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, roamRadius, 1);
        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);
        agent.SetPath(path);
        yield return new WaitForSeconds(3);
    }
    void facePlayer()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            playerDir.y = 0;
            var rotation = Quaternion.LookRotation(playerDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * playerFaceSpeed);
        }
    }

    void canSeePlayer()
    {
        float angle = Vector3.Angle(playerDir, transform.forward);
        Debug.Log(angle);
        RaycastHit hit;



        if (Physics.Raycast(transform.position, playerDir, out hit))
        {
            Debug.DrawRay(transform.position, playerDir);
            if (hit.collider.CompareTag("Player") && canShoot && angle <= viewAngle)
            {
                StartCoroutine(shoot());
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerInRange = true;
            canShoot = true;
            agent.stoppingDistance = StoppingDistOrig;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            agent.stoppingDistance = 0;
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        playerInRange = true;
        StartCoroutine(flashColor());
        gameManager.instance.playerScript.earnPoints(shotPoints);
        if (HP <= 0)
        {
            gameManager.instance.checkEnemyKills();
            gameManager.instance.playerScript.earnPoints(killPoints);
            agent.enabled = false;
            anim.SetBool("Dead", true);

            //for the drops in survival
            //if (GameMode == 3)
            //{
            //    powerUpDrop();
            //}

            foreach (Collider col in GetComponents<Collider>())
                col.enabled = false;

        }

    }
    IEnumerator flashColor()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = Color.white;
    }
    IEnumerator shoot()
    {
        canShoot = false;

        anim.SetTrigger("Shoot");// Lets us use shoot animation

        Instantiate(bullet, shootPos.transform.position, bullet.transform.rotation);

        yield return new WaitForSeconds(shootRate);

        canShoot = true;
    }

    public void powerUpDrop()
    {
        //make a random number
        int maybePowerUp = 0;
        if (maybePowerUp == 4)
        {
            //make a heal power up on the body
           // Instantiate(heal);
        }
        if (maybePowerUp == 8)
        {
            //make a damage power drop on body
        }
        if (maybePowerUp == 12)
        {
            //make a ammo drop on body
        }
        if (maybePowerUp == 16)
        {
            //make a double points drop on body
        }

    }


}
