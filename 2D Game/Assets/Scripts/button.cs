using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPointerClickHandler
{


    public bool Pointerdown;
    public bool Pointerclick;
    public bool Pointerup;


    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick called.");
        Pointerclick = true;
        //Pointerup = false;


    }
    
    public void OnPointerDown(PointerEventData eventData)
    {   if (Input.GetButtonDown("button"))
        Debug.Log("OnPointerDown called.");
        Pointerdown = true;
        Pointerup = false;

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pointerup = true;
        Reset();
        Debug.Log("OnPointerUP called.");
    }
    private void Reset()
    {
        Pointerdown = false;
        Pointerclick = false;
    }

}