using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AmmoCount : MonoBehaviour
{
    
    public TextMeshProUGUI ammunitionText;
    public TextMeshProUGUI magText;
    public static AmmoCount occurrence;
    private void Awake()
    {
        occurrence = this;
    }
    public void UpdateAmmoText(int presentAmmunition)
    {
        ammunitionText.text = "Đan." + presentAmmunition; 
    }
    public void UpdateMagText(int mag)
    {
        magText.text = "Bang." + mag;
    }
}
