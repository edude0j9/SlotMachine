using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private MachineSlot machineSlotScript = null;
    private Vector3 screenPoint;
    private Vector3 offset;

    private bool isTouchable = true;
    private bool isDragged = false;
    private int tries = 0;
 
    private void OnMouseDown()
    {
        isDragged = true;
        if (isTouchable)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, Input.mousePosition.y, screenPoint.z));    
        }
    }
 
    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(transform.position.x, Input.mousePosition.y, screenPoint.z);
 
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if ((curPosition.y <= transform.position.y && curPosition.y >= -3) && isTouchable)
        {
            transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        if (transform.position.y <= -2.4)
        {
            machineSlotScript.SpinSlots();
            isTouchable = false;            
        }
    }

    public void TurnOnTouchable()
    {
        isTouchable = true;
    }

    private void Update()
    {
        if(isDragged == false && transform.position.y <= 3)
        {
            transform.position += Vector3.up * 5 * Time.deltaTime;
        }
    }
}
