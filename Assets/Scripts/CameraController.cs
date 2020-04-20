using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 startPos;

    GameObject focus;

    #region scale

    Vector3 initialScale;

    private float Scale = 1f;
    public float scale
    {
        get => Scale;
        set
        {
            Scale = value;
        }
    }

    private void StartScale()
    {
        initialScale = transform.localScale;
    }

    private void ActualizeScale()
    {
        transform.localScale = initialScale * scale;
    }

    #endregion

    #region destinationRot
    private Quaternion DestinationRot;
    public Quaternion destinationRot
    {
        get => DestinationRot;
    }

    public void MoveCamera(float side)
    {
        DestinationRot = Quaternion.Euler(0, destinationRot.eulerAngles.y + Mathf.Sign(side) * 90, 0);
    }

    public float movementSpeed = 10f;
    private void ActualizeRotation()
    {
        if (Quaternion.Angle(transform.rotation, destinationRot) == 0) return;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, destinationRot, movementSpeed * Time.deltaTime);
    }
    #endregion

    private void Start()
    {
        startPos = transform.position;
        StartScale();
    }

    void Update()
    {
        ActualizeScale();
        ActualizeRotation();
        ActualizeFocus();
    }


    const float speed = 5f;
    public void ActualizeFocus()
    {
        if (focus == null) return;
            transform.position = Vector3.Lerp(transform.position, focus.transform.position, speed * Time.deltaTime);
    }

    public void Focus(GameObject obj)
    {
        focus = obj;
    }
}
