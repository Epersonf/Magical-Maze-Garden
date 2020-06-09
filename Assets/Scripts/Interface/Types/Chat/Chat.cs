using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public static Chat active;

    public Text messageText;

    public static void ShowMessage(Message message)
    {
        active.messageText.text = message.text;
    }

    public static void ShowMessage(string text, float delay)
    {
        if (!active) return;
        GameManager.interfaceController.showGameInterface = false;
        active.StartCoroutine(Message(text, delay));
    }

    public static IEnumerator Message(string text, float delay)
    {
        active.messageText.text = text;
        yield return new WaitForSeconds(delay);
        GameManager.interfaceController.showGameInterface = true;
    }
}
