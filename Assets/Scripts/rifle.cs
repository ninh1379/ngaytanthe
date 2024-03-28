using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 200;
    public float fireChanrge = 15f;
    protected float nextTimeToShoot = 0f;
    public Animator animator;
    public PlayerMove player;
    public Transform hand;
    public GameObject rifleUi;

    [Header("Rifle Ammunition and shooting")]
    private int maxiumAmmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;

    [Header("Rifle Effect")]
    public ParticleSystem muzzlePark;
    public GameObject WooderEffect;
    public GameObject goneEffect;
    private AudioSource shootaudio;

    private void Awake()
    {
        transform.SetParent(hand);
        presentAmmunition = maxiumAmmunition;
        rifleUi.SetActive(true);
    }
    private void Update()
    {
        if (setReloading) return;

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        {
            
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time +1f/fireChanrge;
            Shoot();
            

        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
        }
    }
    private void Shoot()
    {
      

    //check for mag

        if(mag == 0)
        {
            //show ammo our text
            return;
        }
        presentAmmunition--;
        if(presentAmmunition == 0)
        {
            mag--;

        }
        // Updating UI
        AmmoCount.occurrence.UpdateAmmoText(presentAmmunition);
        AmmoCount.occurrence.UpdateMagText(mag);

        muzzlePark.Play();
       
        RaycastHit hitInfor;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfor, shootingRange))
        {
            Debug.Log(hitInfor.transform.name);
            Objecttohit objecttohit = hitInfor.transform.GetComponent<Objecttohit>();
            Zombie1 zombie1 = hitInfor.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfor.transform.GetComponent<Zombie2>();

            if(objecttohit != null )
            {
                objecttohit.ObjectHitDamage(giveDamageOf);
                GameObject woodGo = Instantiate(WooderEffect,hitInfor.point, Quaternion.LookRotation(hitInfor.normal));
               
                Destroy(woodGo, 1f);
            }
            else if(zombie1 != null)
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
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("reloading ....");
        animator.SetBool("Reloading", true);
        //play reload sound;
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmunition = maxiumAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3;
        setReloading = false;
    }
}
