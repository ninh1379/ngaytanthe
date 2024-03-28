using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveComplete : MonoBehaviour
{
    [Header("Objectives to complete")]
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;
    public TextMeshProUGUI objective3;
    public TextMeshProUGUI objective4;

    public static ObjectiveComplete occurence;

    private void Awake()
    {
        occurence = this;

    }
    public void GetObjectivesDone(bool obj1, bool obj2,bool obj3,bool obj4)
    {
        if(obj1 == true)
        {
            objective1.text = "1.Completed";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "1.Tìm  súng";
            objective1.color = Color.white;
        }

        if (obj2 == true)
        {
            objective1.text = "2.Completed";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "2.Tìm Dân Làng";
            objective1.color = Color.white;
        }


        if (obj3 == true)
        {
            objective1.text = "3.Completed";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "3.Tìm chiếc Xe";
            objective1.color = Color.white;
        }


        if (obj4 == true)
        {
            objective1.text = "4.Completed";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "4.Đưa Mọi Người Vào Xe";
            objective1.color = Color.white;
        }
    }    
}

