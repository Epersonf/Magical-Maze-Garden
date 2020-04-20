using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoChanger : MonoBehaviour
{
    public string scene;
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += LoadNextScene;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadNextScene(null);
        }
    }

    void LoadNextScene(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
