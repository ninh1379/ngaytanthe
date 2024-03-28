using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
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
    
    public float zombieSpeed;
    

    [Header("Zombie Animation")]
    public Animator ani;


    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie moood")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerinvisionradius;
    public bool playerInatatckingRadius;
    private AudioSource zombieAudio;

    private void Awake()
    {
        presentHealts = ZombieHealts;
        zombieAgent = GetComponent<NavMeshAgent>();
        zombieAudio = GetComponent<AudioSource>();
        healthBar.GivefullHealth(ZombieHealts);
    }
    private void Update()
    {
        playerinvisionradius = Physics.CheckSphere(transform.position, visionRadius, Playerlayer);
        playerInatatckingRadius = Physics.CheckSphere(transform.position, attackingRadius, Playerlayer);
        if (!playerinvisionradius && !playerInatatckingRadius) Idle();
        if (playerinvisionradius && !playerInatatckingRadius) Pursueplayer();
        if (playerinvisionradius && playerInatatckingRadius) AttackPlayer();
    }

    private void Idle ()
    {
        zombieAgent.SetDestination(transform.position);
        ani.SetBool("Idle", true);
        ani.SetBool("Running", false);
       // zombieAudio.Stop();
    }



    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(Playerbody.position))
        {
            //anim
            ani.SetBool("Idle", false);
            ani.SetBool("Running", true);
            ani.SetBool("Attacking", false);
            //zombieAudio.Play();
        }
       

    }
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(Lookpoint);
        if (!previouslyAttack)
        {
            zombieAudio.Play();
            RaycastHit hitInfor;
            if (Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfor, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfor.transform.name);

                PlayerMove playerBody = hitInfor.transform.GetComponent<PlayerMove>();
                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamamge);
                }

                ani.SetBool("Running", false);
                ani.SetBool("Attacking", true);
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
        Object.Destroy(gameObject, 5.0f);

    }
}
