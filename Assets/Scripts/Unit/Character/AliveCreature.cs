using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveCreature : UnitComponent
{

    #region life
    private int life = 0;

    public void ChangeLife(int change)
    {
        life += change;
        if (life <= 0) Kill();
    }

    public void SetLife(int lifeSet)
    {
        life = lifeSet;
    }

    #endregion

    #region alive
    private bool Alive = true;
    public bool alive
    {
        get => Alive;
    }
    public virtual void Kill()
    {
        CharacterComponent character = GetComponent<CharacterComponent>();
        if (character != null)
            if (character.IsTurn())
                gameManager.turn++;
        Alive = false;
    }
    #endregion
}
