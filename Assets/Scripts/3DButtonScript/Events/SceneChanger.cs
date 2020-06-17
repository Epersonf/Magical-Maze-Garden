using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string scene = "Spring";

    public void OnClickEvent()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
