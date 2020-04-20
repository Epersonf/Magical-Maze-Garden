using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallComponent : MonoBehaviour
{
    #region Movement
    public Action executeAfter;
    const float speed = 3f;
    const float margin = 0.01f;
    private Vector3 Destination;


    private CharacterComponent emissor;
    private TileComponent actual;
    private int damage = 0;
    public void SetStats(CharacterComponent emissor, int damage, int fuel, Vector3 forward)
    {
        this.damage = damage;
        this.emissor = emissor;
        this.actual = emissor.attachedTile;
        transform.position = GetAdjustedPosition(actual.transform.position);
        Destination = transform.position + emissor.gameManager.gridSize * fuel * forward;
    }

    public Vector3 GetAdjustedPosition(Vector3 pos)
    {
        Vector3 newPos = new Vector3(pos.x, pos.y + (actual.gameManager.gridSize/2f), pos.z);
        return newPos;
    }

    public void MoveToDestination()
    {
        if (Vector3.Distance(transform.position, Destination) < margin)
        {
            ConcludeMovement();
            return;
        }
        CheckCollision();
        transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
    }
    #endregion

    void Update()
    {
        MoveToDestination();
    }

    private void CheckCollision()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        if (collisions.Length == 0) return;

        foreach (Collider collision in collisions) {
            CharacterComponent character = collision.gameObject.GetComponent<CharacterComponent>();
            if (collision.gameObject == this.gameObject) continue;
            if (character != null)
                if (character.characterInformation.type != emissor.characterInformation.type)
                    collision.gameObject.GetComponent<CharacterComponent>().ChangeLife(-damage);
                else
                    continue;
            ConcludeMovement();
        }
    }

    public void ConcludeMovement()
    {
        if (executeAfter != null) executeAfter();
        Destroy(gameObject);
    }
}
