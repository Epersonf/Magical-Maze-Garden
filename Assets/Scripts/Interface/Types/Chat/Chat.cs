using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public static Chat active;

    public Image characterFace;
    public Text messageText;


    public static void ShowMessage(Sprite face, string text, float delay)
    {
        if (!active) return;
        GameManager.interfaceController.showGameInterface = false;
        active.StartCoroutine(Message(face, text, delay));
    }

    public static IEnumerator Message(Sprite face, string text, float delay)
    {
        active.characterFace.sprite = face;
        active.messageText.text = text;
        yield return new WaitForSeconds(delay);
        GameManager.interfaceController.showGameInterface = true;
    }
}
