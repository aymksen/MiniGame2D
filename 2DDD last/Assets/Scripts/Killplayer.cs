using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killplayer : MonoBehaviour
{
    public LevelManager levelManager;
    public Heart heart;


    void Start()
    {
        levelManager = GameObject.Find("Player-sprite (1)").GetComponent<LevelManager>();
        heart = GameObject.Find("Player-sprite (1)").GetComponent<Heart>();

       

    }

    // Update is called once per frame
    void Update()
    {
        if (heart.health <= 0)
        {
            
            levelManager.respawnplayer();
            heart.health = 3;
        }
        
    }
}
