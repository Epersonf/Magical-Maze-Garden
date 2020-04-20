using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public GameObject[] interfaces;

    public Image characterFace;
    public Text chatText;


    #region InterfaceStatic
    private static InterfaceController InterfaceStatic;

    public static InterfaceController interfaceController
    {
        get => InterfaceStatic;
    }

    private void Start()
    {
        InterfaceStatic = this;
    }
    #endregion

    public void ShowMessage(Message message)
    {
        if (showingMessage) return;
        StartCoroutine(ShowMessageEnumerator(message));
    }

    private bool showingMessage = false;
    IEnumerator ShowMessageEnumerator(Message msg)
    {
        showingMessage = true;
        SetMode(InterfaceMode.Chat);
        if (msg.character != null)
            characterFace.sprite = msg.character.faceImage;
        chatText.text = msg.message;
        yield return new WaitForSeconds(msg.delay);
        SetMode(InterfaceMode.User);
        showingMessage = false;
    }

    public void SetMode(InterfaceMode mode)
    {
        for (int i = 0; i < interfaces.Length; i++)
        {
            interfaces[i].SetActive((int)mode == i);
        }
    }
}

public enum InterfaceMode
{
    User,
    Chat
}

[System.Serializable]
public class Message
{
    public CharacterData character;
    public string message;
    public float delay;

    public Message(CharacterData character, string msg, float delaySet)
    {
        if (character.faceImage != null)
            this.character = character;
        message = msg;
        delay = delaySet;
    }
}