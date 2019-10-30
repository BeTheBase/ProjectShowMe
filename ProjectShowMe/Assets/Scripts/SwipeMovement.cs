using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMovement : MonoBehaviour
{
    [SerializeField] private float height = 10f;
    [SerializeField] private float speed = 5f;
    private bool tap = false;

    private void Update()
    {
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // get the touch position from the screen touch to world point
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, transform.position.y, transform.position.z));
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
            }
        }*/

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Vector3 newPosition = new Vector3(hit.point.x, height, 0);
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }
    }
}
