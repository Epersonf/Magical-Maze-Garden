using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameLevelObject/Keys")]
public class KeyObject : ScriptableObject
{
    [Tooltip("Variables used in level.")]
    public KeyManager keyManager;
    [Tooltip("Variables needed to end game.")]
    public ConditionChecker conditionsToEnd;
    [Tooltip("If the main team needs to kill all players to conclude the map.")]
    public bool killAllPlayersNeeded = true;
}


#region Variables system classes
#pragma warning disable 0649
[System.Serializable]
public class EventConditionChecker : ConditionChecker
{
    [SerializeField]
    private GameKey[] givenKeys;

    public void TryToGiveKeys(KeyManager manager)
    {
        if (!HasAchievedAllKeys(manager)) return;
        foreach (GameKey g in givenKeys)
        {
            manager.SetKey(g.keyName, g.keyValue);
        }
    }
}

[System.Serializable]
public class ConditionChecker
{
    [SerializeField]
    private KeyManager[] neededKeys;

    public bool HasAchievedAllKeys(KeyManager manager)
    {
        foreach (KeyManager c in neededKeys)
        {
            if (c.CompareKeys(manager)) return true;
        }
        return false;
    }
}


[System.Serializable]
public class KeyManager
{
    [SerializeField]
    private GameKey[] gameKeys;

    public bool CompareKeys(KeyManager manager)
    {
        foreach (GameKey g in gameKeys)
        {
            int index = manager.GetKeyIndex(g.keyName);
            if (g.keyValue != manager.GetKey(index)) return false;
        }
        return true;
    }

    #region Using index functions
    public void SetKey(string key, bool value)
    {
        int index = GetKeyIndex(key);
        SetKey(index, value);
    }

    public bool GetKey(string key)
    {
        int index = GetKeyIndex(key);
        return GetKey(index);
    }
    #endregion

    #region Not using index functions
    public void SetKey(int index, bool value)
    {
        gameKeys[index].keyValue = value;
    }

    public bool GetKey(int index)
    {
        return gameKeys[index].keyValue;
    }
    #endregion

    #region Index functions
    public int GetKeyIndex(string key)
    {
        for (int i = 0; i < gameKeys.Length; i++)
        {
            if (gameKeys[i].keyName.Equals(key))
                return i;
        }
        Debug.LogError("Key not found: " + key);
        return -1;
    }
    #endregion
}

[System.Serializable]
public class GameKey
{
    public string keyName;
    public bool keyValue;
}

public enum EndLevelType
{
    Variables,
    Deathmatch
}
#endregion