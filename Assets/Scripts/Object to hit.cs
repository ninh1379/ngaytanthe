using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objecttohit : MonoBehaviour
{
    public float ObjectHealt = 300f;
    public void ObjectHitDamage(float ammount)
    {
        ObjectHealt -= ammount;
        if (ObjectHealt <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
