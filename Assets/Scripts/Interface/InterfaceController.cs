using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    #region GameInterface and Chat switch
    #pragma warning disable 649
    public GameObject gameInterface;
    public GameObject chat;

    private bool ShowGameInterface;
    public bool showGameInterface
    {
        get => ShowGameInterface;
        set
        {
            ShowGameInterface = value;
            gameInterface.SetActive(value == true);
            chat.SetActive(value != true);
        }
    }
    #endregion

    [System.NonSerialized]
    public UserInput userInput;
    [System.NonSerialized]
    public UpdatableInterface updatableInterface;
    [System.NonSerialized]
    public Alert alert;

    private void Awake()
    {
        userInput = GetComponentInChildren<UserInput>();
        updatableInterface = GetComponentInChildren<UpdatableInterface>();
        alert = GetComponentInChildren<Alert>();
        Chat.active = chat.GetComponent<Chat>();
    }

    public void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Chat.ShowMessage(null, "Ola", 2);
        }
    }
}
