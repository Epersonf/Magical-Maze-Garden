using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ButtonBehaviour : MonoBehaviour
{
    private void OnMouseDown()
    {
        SendMessage("OnClickEvent", SendMessageOptions.DontRequireReceiver);
    }

    private void OnMouseOver()
    {
        SendMessage("OnMouseOverEvent", SendMessageOptions.DontRequireReceiver);
    }
}
