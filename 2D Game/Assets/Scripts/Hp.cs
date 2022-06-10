using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public Heart heart;
    public LevelManager levelManager;
    public GameObject gameObjectToEnable;

    void Start()
    {
        heart = GameObject.Find("Player-sprite (1)").GetComponent<Heart>();
        levelManager = GameObject.Find("Player-sprite (1)").GetComponent<LevelManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
         if (other.name == "Player-sprite (1)" && heart.health < 5)
        {
            heart.Heal(1);
            Debug.Log("yum");
            gameObject.SetActive(false);

        }
       
    }
    }


    
    



