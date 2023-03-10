using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int max_health = 6;
    public int current_health;

    public HealthBar player_health_bar;
    public game_over_text game_over;

    // Start is called before the first frame update
    void Start(){
        current_health = max_health;
        player_health_bar.SetMaxHealth(max_health);
    }

    // Update is called once per frame
    void Update(){
        /*if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            TakeDamage(1);
        }*/
    } 

    void OnCollisionEnter(Collision collision){ //Check if the item collides with an object tagged as "enemy"
        /*if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }*/
        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage_taken){
        current_health -= damage_taken;
        player_health_bar.SetHealth(current_health);
        if (current_health <= 0){
            Destroy(gameObject);
            GameObject wrapObj = GameObject.FindGameObjectWithTag("CharacterWrapper");
            if(! wrapObj)
                Debug.Log("PlayerHealth.cs: Error: Unable to find object with tag \"CharacterWrapper\". Attempted to destroy player, but unable to destroy wrapper object.");
            else
                Destroy(wrapObj);
            game_over.RevealGameOver(); //reveals the game over screen
            Debug.Log("Player Destroyed. Game Over!");
        }
    }

    public void UpgradeHealth(int bonus_health){
        max_health += bonus_health;
        current_health = max_health;
        player_health_bar.SetMaxHealth(max_health);
    }
}