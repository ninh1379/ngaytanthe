using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Health & Things")]
    public float ZombieHealts = 100f;
    public float presentHealts;
    public float giveDamamge = 5f;
    public HealthBar healthBar;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform Lookpoint;
    public Camera AttackingRaycastArea;
    public Transform Playerbody;
    public LayerMask Playerlayer;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingpointRadius =2;

    [Header ("Zombie Animation")]
    public Animator ani;


    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie moood")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerinvisionradius;
    public bool playerInatatckingRadius;

    private void Awake()
    {
        presentHealts = ZombieHealts;
        zombieAgent = GetComponent<NavMeshAgent>();
        healthBar.GivefullHealth(ZombieHealts);
    }
    private void Update()
    {
        playerinvisionradius = Physics.CheckSphere(transform.position, visionRadius, Playerlayer);
        playerInatatckingRadius= Physics.CheckSphere(transform.position, attackingRadius, Playerlayer);
        
        if (!playerinvisionradius && !playerInatatckingRadius) Guard();
        if (playerinvisionradius && !playerInatatckingRadius) Pursueplayer();
        if (playerinvisionradius && playerInatatckingRadius) AttackPlayer();
    }

    private void Guard()
    {
        if(Vector3.Distance(walkPoints[currentZombiePosition].transform.position,transform.position) < walkingpointRadius)
        {
            currentZombiePosition = Random.Range(0,walkPoints.Length);
            if(currentZombiePosition >= walkPoints.Length) {

                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        // zombie facing
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }



    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(Playerbody.position))
        {
            //anim
            ani.SetBool("Walking", false);
            ani.SetBool("Running", true);
            ani.SetBool("Attacking", false);
            ani.SetBool("Died", false);
        }
        else
        {
            ani.SetBool("Walking", false);
            ani.SetBool("Running",false );
            ani.SetBool("Attacking", false);
            ani.SetBool("Died", true);
        }
    
    }
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(Lookpoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfor;
            if(Physics.Raycast(AttackingRaycastArea.transform.position,AttackingRaycastArea.transform.forward,out hitInfor,attackingRadius))
                {
                Debug.Log("Attacking" + hitInfor.transform.name);

                PlayerMove playerBody = hitInfor.transform.GetComponent<PlayerMove>();
                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamamge);
                }

                ani.SetBool("Attacking", true);
               ani.SetBool("Walking", false);
                ani.SetBool("Running", false);
                ani.SetBool("Died", false);
            }
            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);


           
        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }
    public void zombieHitDamage(float takeDamage)
    {
        presentHealts -= takeDamage;
        healthBar.SetHealth(presentHealts);
        if (presentHealts <= 0) 
        {

            ani.SetBool("Walking", false);
            ani.SetBool("Running", false);
            ani.SetBool("Attacking", false);
            ani.SetBool("Died", true);
            zombieDie();
        }
        
        }
    
    private void zombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInatatckingRadius = false;
        playerinvisionradius = false;
        Object.Destroy(gameObject , 5.0f);

    }
}
