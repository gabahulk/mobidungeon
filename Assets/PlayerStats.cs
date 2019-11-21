using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public HealthUIBehavior healthUI;

    public delegate void PlayerHealthChangeDelegate();
    public PlayerHealthChangeDelegate playerHealthChangeEvent;

    public override void SetHealth(int health)
    {
        base.SetHealth(health);
        playerHealthChangeEvent?.Invoke();
    }
}
