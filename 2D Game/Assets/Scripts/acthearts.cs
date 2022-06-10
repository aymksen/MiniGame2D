using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acthearts : MonoBehaviour
{
    int childs;
    
    void Start()
    {
        childs = transform.childCount;

    }
    void Update()
    {

    }

    public void heartactive()
    {
        Debug.Log("heartactive = "+childs);
        for(int i=0;i< childs; i++)
        {
            transform.GetChild(i).transform.gameObject.SetActive(true);
        }
        
       


    }
    
}
