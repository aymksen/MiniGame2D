    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public int health;
    public int numOfheart;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    





    void Start()
    {
        
    }


        void Update()
    {
        if(health > numOfheart)
        {
            health = numOfheart;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i< health)
            {
                hearts[i].sprite = fullHeart;
            }
            else { hearts[i].sprite = emptyHeart; }
            if (i < numOfheart)
            {
                hearts[i].enabled = true;
            }
            else { hearts[i].enabled = false; }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            health = 0;
        }
        
    }
    public void Damage(int dmg)
    {
        health -= dmg;  

    }
    public void Heal(int hp)
    {
        health += hp;

    }
    

}
