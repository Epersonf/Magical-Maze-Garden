using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapEvents/MessageEvent")]
public class MessageEvent : EventTrigger
{
    public List<Message> messages;

    public override void Activate()
    {
        base.Activate();
        Trigged(0);
    }

    public void Trigged(int i)
    {
        if (i == messages.Count) return;
        InterfaceController.interfaceController.StartCoroutine(ShowMessage(messages[i], i));
    }

    IEnumerator ShowMessage(Message msg, int i)
    {
        InterfaceController.interfaceController.ShowMessage(msg);
        yield return new WaitForSeconds(msg.delay + 0.2f);
        Trigged(i + 1);
    }
}
