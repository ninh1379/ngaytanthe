using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider heaalthbarslider;
    public void GivefullHealth(float health)
    {
        heaalthbarslider.maxValue = health;
        heaalthbarslider.value = health;
    }
    public void SetHealth(float health)
    {
        heaalthbarslider.value = health;
    }
}
