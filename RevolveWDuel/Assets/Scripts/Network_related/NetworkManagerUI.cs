using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button clientBtn;
    private HealthBarManager healthBarManager;

    private void Awake()
    {
        // Setup buttons for network
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager.Singleton tidak ditemukan di scene.");
            return;
        }

        hostBtn.onClick.AddListener(OnHostButtonClick);
        serverBtn.onClick.AddListener(OnServerButtonClick);
        clientBtn.onClick.AddListener(OnClientButtonClick);

        // Get the HealthBarManager reference
        healthBarManager = FindObjectOfType<HealthBarManager>();
    }

    private void OnHostButtonClick()
    {
        NetworkManager.Singleton.StartHost();
    }

    private void OnServerButtonClick()
    {
        NetworkManager.Singleton.StartServer();
    }

    private void OnClientButtonClick()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void SetHealthListener(Health health)
    {
        if (health != null)
        {
            healthBarManager.InitializeHealthBar(health.GetMaxHealth());
            health.OnHealthChanged += healthBarManager.UpdateHealthBar;
        }
    }

    private void OnPlayerSpawned(GameObject playerObject)
    {
        // Make sure we're only setting up the health for the local player
        if (playerObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            Health health = playerObject.GetComponent<Health>();
            SetHealthListener(health);
        }
    }
}


