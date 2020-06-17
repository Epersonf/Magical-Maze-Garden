using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatDialogue : MonoBehaviour
{
    public List<Message> messages;

    private int Msg = 0;
    public int msg
    {
        get => Msg;
        set {
            if (value >= messages.Count || value < 0) Msg = 0;
            else
                Msg = value;
        }
    }


    private void Awake()
    {
        msg = 0;
    }

    private void OnEnable()
    {
        Chat.ShowMessage(messages[msg]);
        msg++;
    }
}

[System.Serializable]
public class Message
{
    [TextArea]
    public string text;
}
