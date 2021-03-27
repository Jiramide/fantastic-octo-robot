using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{

    private Camera cam;
    private GameObject currentlyPickedUp;

    void Awake()
    {
        cam = Camera.main;
    }

    public void PickUp(GameObject pickup)
    {
        if (currentlyPickedUp != null)
        {
            return;
        }

        currentlyPickedUp = Instantiate(pickup);
    }

    private RaycastHit2D RaycastHitFromMouse()
    {
        var mousePosition = Input.mousePosition;
        var mouseRay = cam.ScreenPointToRay(mousePosition);
         
        return Physics2D.GetRayIntersection(mouseRay);
    }

    public void PlacePickUp()
    {
        Debug.Log("placed down");

        currentlyPickedUp = null;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var raycastHit = RaycastHitFromMouse();
            Debug.Log(raycastHit.point);

         //   PlacePickUp();
        }
        if (currentlyPickedUp == null)
        {
            // nothing picked up, maybe have camera dragging behaviour here.
            if (Input.GetMouseButton(0))
            {
                cam.transform.Translate(new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0.0f));
            }

            return; 
        }

        /*
        if (Input.GetMouseButton(0))
        {
            // currently still dragging
            Debug.Log(currentlyPickedUp);
            
        }
        else
        {
            // PlacePickUp();
        }*/
    }

}
