using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;
    private Character1 player;
    public Hp hp;
    public acthearts actheart;




    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Jump1>();
        hp = GameObject.Find("heart1").GetComponent<Hp>();
        actheart = GameObject.Find("Hearts").GetComponent<acthearts>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void respawnplayer()
    {

        actheart.heartactive();
        player.transform.position = currentCheckpoint.transform.position;
        
    }
}
