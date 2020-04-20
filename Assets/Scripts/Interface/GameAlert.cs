using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAlert : MonoBehaviour
{
    private static GameAlert MainAlert;
    public static GameAlert mainAlert
    {
        get => MainAlert;
    }
    #pragma warning disable 0649
    [SerializeField]
    private Text setAlertText;

    private void OnEnable()
    {
        MainAlert = this;
        setAlertText.enabled = false;
    }

    public void Alert(string text)
    {
        setAlertText.enabled = true;
        Alert(text, 1f);
    }

    public void Alert(string text, float time) {
        StartCoroutine(ShowAlert(text, time));
    }

    private IEnumerator ShowAlert(string text, float time)
    {
        setAlertText.text = text;
        yield return new WaitForSeconds(time);
        setAlertText.text = "";
        setAlertText.enabled = false;
    }
}
