using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIBehavior : MonoBehaviour
{
    public GameObject heartContainerPrefab;

    private List<GameObject> HealthUI = new List<GameObject>();
    private PlayerStats stats;

    private void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        stats.playerHealthChangeEvent += OnPlayerHealthChanged;

        SetupTotalHealth();
        UpdateHealthUI(stats.health);
    }

    private void OnDestroy()
    {
        stats.playerHealthChangeEvent -= OnPlayerHealthChanged;
    }

    void OnPlayerHealthChanged()
    {
        UpdateHealthUI(stats.health);
    }

    void OnPlayerNumberOfHeartsChanged()
    {
        SetupTotalHealth();
    }

    private void UpdateHealthUI(int currentHeath)
    {
        HeartUIBehavior.Hearts state;
        for (int i = 0; i < stats.numberOfHearts; i++)
        {
            if (currentHeath - 2 >= 0)
            {
                state = HeartUIBehavior.Hearts.Full;
                currentHeath -= 2;
            }
            else if (currentHeath - 1 >= 0)
            {
                state = HeartUIBehavior.Hearts.Half;
                currentHeath -= 1;
            }
            else
            {
                state = HeartUIBehavior.Hearts.Empty;
            }

            HealthUI[i].GetComponent<HeartUIBehavior>().SetHeartState(state);

        }

    }


    public void SetupTotalHealth()
    {
        for (int i = 0; i < stats.numberOfHearts; i++)
        {
            var heart = Instantiate(heartContainerPrefab, this.transform);
            HealthUI.Add(heart);
        }
    }

}
