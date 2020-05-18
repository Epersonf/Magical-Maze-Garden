﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    #region GameInterface and Chat switch
    public GameObject gameInterface;
    public GameObject chat;
    private bool ShowGameInterface;
    public bool showGameInterface
    {
        get => ShowGameInterface;
        set
        {
            showGameInterface = value;
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
    }
}
