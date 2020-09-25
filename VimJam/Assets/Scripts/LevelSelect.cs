using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void PlayTutorial()
    {
        SceneManager.LoadScene("00 - Tutorial");
    }
    public void PlayL1()
    {
        SceneManager.LoadScene("01 - Farm");
    }
    public void PlayL2()
    {
        SceneManager.LoadScene("03 - Jazz");
    }
    public void PlayL3()
    {
        SceneManager.LoadScene("04 - Synthwave");
    }
}
