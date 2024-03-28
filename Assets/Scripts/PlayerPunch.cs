using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header ("Player Punch Var")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float punchingRange = 5f;
    public GameObject WooderEffect;
    public GameObject goneEffect;

    public void punch()
    {
        RaycastHit hitInfor;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hitInfor, punchingRange))
        {
            Debug.Log(hitInfor.transform.name);
            Objecttohit objecttohit = hitInfor.transform.GetComponent<Objecttohit>();
            Zombie1 zombie1 = hitInfor.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfor.transform.GetComponent<Zombie2>();
            if (objecttohit != null)
            {
                objecttohit.ObjectHitDamage(giveDamageOf);
                GameObject woodGo = Instantiate(WooderEffect, hitInfor.point, Quaternion.LookRotation(hitInfor.normal));
                Destroy(woodGo, 1f);
            }
            else if (zombie1 != null)
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

