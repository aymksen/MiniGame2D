using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRot : MonoBehaviour  
{
    public Joystick joystick2;

    private void Update()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 direction = joystick2.Direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
