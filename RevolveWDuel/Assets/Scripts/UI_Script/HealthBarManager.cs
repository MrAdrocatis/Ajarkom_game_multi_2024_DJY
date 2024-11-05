using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance { get; private set; }

    public GameObject healthUnitPrefab;
    public Transform healthContainer;
    private List<GameObject> healthUnits = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeHealthBar(int maxHealth)
    {
        foreach (var unit in healthUnits)
        {
            Destroy(unit);
        }
        healthUnits.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject unit = Instantiate(healthUnitPrefab, healthContainer);
            healthUnits.Add(unit);
        }
    }

    public void UpdateHealthBar(int currentHealth)
    {
        for (int i = 0; i < healthUnits.Count; i++)
        {
            healthUnits[i].SetActive(i < currentHealth);
        }
    }
}


