using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public static string lastLevel = "MainMenu";

    public void OnClickEvent()
    {
        SceneManager.LoadScene(lastLevel, LoadSceneMode.Single);
    }
}
