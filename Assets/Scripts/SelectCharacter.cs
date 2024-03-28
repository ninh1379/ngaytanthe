using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public GameObject seclect;
    public GameObject MainMenu;
  public void OnCharacter1()
    {
        SceneManager.LoadScene("ZombieLand");
    }
    public void OnCharacter2()
    {
        SceneManager.LoadScene("ZombieLand 1");
    }
    public void OnCharacter3()
    {
        SceneManager.LoadScene("ZombieLand 2");
    }
    public void BackMenu()
    {
        seclect.SetActive(false);
        MainMenu.SetActive(true);
    }
}
