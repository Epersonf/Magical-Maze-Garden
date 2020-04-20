using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : MonoBehaviour
{
    public string unitName;
    public GameManager gameManager;
    public GameLevel currentLevel;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        currentLevel = gameManager.currentLevel;
    }

    #region Highlight
    const float colorMult = 1.5f;
    private bool highlighted = false;
    List<Color> previous = new List<Color>();
    public void SetHighlight(bool v)
    {
        SetHighlight(v, new Color(2, 2, 2));
    }
    public void SetHighlight(bool v, Color change)
    {
        if (highlighted == v) return;
        highlighted = v;
        if (v)
            EnableHighlight(change);
        else
            DisableHighlight();
    }
    public void EnableHighlight(Color change)
    {
        foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
        {
            previous.Add(m.material.color);
            m.material.color = change;
        }
    }
    public void DisableHighlight()
    {
        int i = 0;
        foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
        {
            previous.Add(m.material.color);
            m.material.color = previous[i];
            i++;
            if (i >= previous.Count) break;
        }
        previous.Clear();
    }
    #endregion

    #region Select
    private bool Selected = false;
    public bool selected
    {
        get => Selected;
    }
    public virtual void Select()
    {
        if (selected) return;
        Selected = true;
    }

    public virtual void Unselect()
    {
        if (!selected) return;
        Selected = false;
    }
    #endregion

}
