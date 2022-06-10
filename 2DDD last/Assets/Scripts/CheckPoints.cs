using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public LevelManager levelManager;
    public Animator animatorFlag;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
   
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player-sprite (1)") 
        {

            levelManager.currentCheckpoint = gameObject;
            Debug.Log("activ check" + transform.position);
            animatorFlag.SetBool("flag active", true);
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());


        }
        else { 
            animatorFlag.SetBool("flag active", false);
            animatorFlag.Play("flag desactivate");
        }


    }
}
