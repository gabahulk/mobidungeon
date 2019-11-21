using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool attacking;
    public float visionRadius;
    public int health;
    public int numberOfHearts;
    public int totalHealth;
 

    private void Awake()
    {
        totalHealth = numberOfHearts * 2;
        health = totalHealth;
    }

    public void SetFullHealth()
    {
        SetHealth(totalHealth);
    }

    public virtual void SetHealth(int health)
    {
        this.health = health;
    }

    public void ReceiveDamage(int damage)
    {
        SetHealth(this.health - damage);
    }
}
