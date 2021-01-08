using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{

    public GameObject follow;
    public Vector3 tileMapOffset;

    void Update()
    {

        Vector3 camPosition = Camera.main.transform.position;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;

        Vector3 nearClipPlanePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 tileMapPosition = camPosition + (nearClipPlanePosition - camPosition) * camPosition.z / (camPosition.z - nearClipPlanePosition.z);

        Vector3 endPosition = new Vector3(
            Mathf.Floor(tileMapPosition.x),
            Mathf.Floor(tileMapPosition.y),
            Mathf.Floor(tileMapPosition.z)
        );

        follow.GetComponent<Transform>().position = endPosition + tileMapOffset;
    }

}
