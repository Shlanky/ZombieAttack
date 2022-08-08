using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class SpitterAi2 : MonoBehaviour, iDamageable
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
    [SerializeField] VisualEffect bile;
    float SpeedOrig;

    [Header("----------------------------------")]
    [Header("Weapon Stats")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootPos;

    bool canShoot=true;
    [SerializeField] bool playerInRange;
    Vector3 playerDir;
    Vector3 startingPos;
    float StoppingDistOrig;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        StoppingDistOrig = agent.stoppingDistance;
        gameManager.instance.updateEnemyNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {

            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * 5));

            playerDir = gameManager.instance.player.transform.position - transform.position;
            agent.SetDestination(gameManager.instance.player.transform.position);
            facePlayer();
            if (playerInRange)
            {
                canSeePlayer();
            }
           /* else if (agent.remainingDistance < 0.1f)
                StartCoroutine(roam());*/
        }
    }
    /*
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
    }*/
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
          //  canShoot = true;
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
        if (HP <= 0)
        {
            gameManager.instance.checkEnemyKills();
            agent.enabled = false;
            anim.SetBool("Dead", true);

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
        if(canShoot==true)
        {
        canShoot = false;
        SpeedOrig = agent.speed;
        agent.speed = 0;
        anim.SetTrigger("Shoot");// Lets us use shoot animation

        bile.Play();

        Instantiate(bullet, shootPos.transform.position, bullet.transform.rotation);

        yield return new WaitForSeconds(3.5f);
        //3.5 b4
        agent.speed = SpeedOrig;

        yield return new WaitForSeconds(shootRate);
        canShoot = true;
        }
    }



}