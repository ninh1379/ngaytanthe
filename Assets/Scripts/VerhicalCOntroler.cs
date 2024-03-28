using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerhicalCOntroler : MonoBehaviour
{
    [Header("Whells colliders")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    [Header("Wheels Tranforms")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform vehicleDoor;

    [Header("Vehical Engine")]
    public float accelerationForce = 100f;
    public float presentAcceleration = 0f;
    public float breakingForce = 200f;
    public float presentBreakForce = 0f;

    [Header("Vehical Steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehical Security")]
    public PlayerMove player;
    public float radius = 5f;
    private bool isOpened = false;

    [Header("Disable Things")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject PlayerCharacter;

    [Header("Vehical Hit Var")]
    public Camera cam;
    public float HitRange = 2f;
    public float giveDamageOf = 100f;
    //public ParticleSystem hitpark;
    public GameObject goneEffect;
   


    private void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpened = true;
                radius = 500f;
                //obj complete
                ObjectiveComplete.occurence.GetObjectivesDone(false, false,true , false);

            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;
            }
        }   
        if(isOpened == true)
        {
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            PlayerCharacter.SetActive(false);

            MoveVehicle();
            VehicaleSteering();
            ApplyBreaks();
            HitZombie();
        }
        else if(isOpened == false) 
        {
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            PlayerCharacter.SetActive(true);
        }
    }
    void MoveVehicle()
    {
        frontRightWheelCollider.motorTorque = presentAcceleration;
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;
            
        presentAcceleration = accelerationForce * - Input.GetAxis("Vertical");
    }
    void VehicaleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;
        //animate
        StreeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        StreeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        StreeringWheels(backRightWheelCollider, backRightWheelTransform);
        StreeringWheels(backLeftWheelCollider, backLeftWheelTransform);
    }

    void StreeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;
        WC.GetWorldPose(out position, out rotation);
        WT.position = position; 
        WT.rotation = rotation;
    }
    void ApplyBreaks()
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            presentBreakForce = breakingForce;
        }
        else
        {
            presentBreakForce = 0f;
        }
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;

    }
    void HitZombie()
    {
        RaycastHit hitInfor;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfor, HitRange))
        {
            Debug.Log(hitInfor.transform.name);
         
            Zombie1 zombie1 = hitInfor.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfor.transform.GetComponent<Zombie2>();

            if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                GameObject goneEffectGo = Instantiate(goneEffect, hitInfor.point, Quaternion.LookRotation(hitInfor.normal));
                Destroy(goneEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                GameObject goneEffectGo = Instantiate(goneEffect, hitInfor.point, Quaternion.LookRotation(hitInfor.normal));
                Destroy(goneEffectGo, 1f);
            }
        }
    }
}
