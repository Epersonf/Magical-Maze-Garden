using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    public static Alert active;
    private Text textUI;
    public void Awake()
    {
        active = this;
        textUI = GetComponent<Text>();
        textUI.text = "";
    }

    public static void AlertMessage(string text, float delay)
    {
        active.textUI.text = text;
        active.StartCoroutine(EndAlert(delay));
    }

    private static IEnumerator EndAlert(float delay)
    {
        yield return new WaitForSeconds(delay);
        active.textUI.text = "";
    }
}
