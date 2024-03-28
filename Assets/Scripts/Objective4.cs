using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vehical")
        {
            // objective complete
            ObjectiveComplete.occurence.GetObjectivesDone(false, false , false,true);
            SceneManager.LoadScene("Mainmenu"); 
        }
    }
}
