using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}

    public int health {get; private set;}
    public int maxHealth {get; private set;}

    public void Startup() {
        Debug.Log("Player Manager startup");
        health = 50;
        maxHealth = health;

        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value) {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);
        if(health == 0) {
            Debug.Log("Player has died");
        } else {
            Debug.Log("Player health: " + health + "/" + maxHealth);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
