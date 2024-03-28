using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Movenment")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;

    [Header("Healts Player Things")]
    public float playerHealts = 120f;
    public float presentHealts;
    public GameObject playerDamage;
    public HealthBar healthBar;

    [Header("Player Script Cameras")]
    public Transform playerCamera;
    public GameObject EndGameMenuUi;


    [Header("Player Animator and Gravity")]
    public CharacterController cC;// characterControler
    public float gravity = -9.81f;
    public Animator animator;


    [Header("Player Jumping and Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmTimeVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealts = playerHealts;
        healthBar.GivefullHealth(playerHealts);
    }
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
        playerMove();
        Jump();
        Sprint();
    }
    //Dichuyen
    void playerMove()
    {
        float horizontal_Axis = Input.GetAxisRaw("Horizontal");
        float vertical_Axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_Axis, 0f, vertical_Axis).normalized;
        if (direction.magnitude >= 0.1f )
        {

            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("Riflewalk", false);
            animator.SetBool("IdleAim", false);


            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmTimeVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }


    }
    // Nhay///
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Jump", true);
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("idle", true);
            animator.SetBool("Jump", false);
        }
    }
        // CHay nhanh
        void Sprint()
        {
            if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
            {


                float horizontal_Axis = Input.GetAxisRaw("Horizontal");
                float vertical_Axis = Input.GetAxisRaw("Vertical");

                Vector3 direction = new Vector3(horizontal_Axis, 0f, vertical_Axis).normalized;
                if (direction.magnitude >= 0.1f)
                {

                    animator.SetBool("Walk", false);
                    animator.SetBool("Running", true);

                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmTimeVelocity, turnCalmTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
                }
                else
                {

                    animator.SetBool("Walk", true);
                    animator.SetBool("Running", false);
                }
            }

        }

    public void playerHitDamage(float takeDamage)
    {
        presentHealts -= takeDamage;
        StartCoroutine(PlayerDamage());
        healthBar.SetHealth(presentHealts);

        if (presentHealts <= 0)
        {
            playerDie();
        }
    }
    private void playerDie()
    {
        EndGameMenuUi.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject,1.0f);
    }
    
    IEnumerator PlayerDamage()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);
    }
}
