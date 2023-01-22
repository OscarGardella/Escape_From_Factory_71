using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int max_health = 5;
    public int current_health;

    public HealthBar player_health_bar;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        player_health_bar.SetMaxHealth(max_health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            TakeDamage(1);
        }
    } 

    void TakeDamage(int damage_taken)
    {
        current_health -= damage_taken;
        player_health_bar.SetHealth(current_health);
        if (current_health <= 0){
            Destroy(gameObject);
        }
    }
}
