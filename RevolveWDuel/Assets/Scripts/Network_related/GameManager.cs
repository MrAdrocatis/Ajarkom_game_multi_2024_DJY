using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public GameObject ammoPowerUpPrefab;
    public GameObject healthPowerUpPrefab;
    public float powerUpSpawnInterval = 10f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional, only if it should persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (IsServer) // Ensure this logic only runs on the server in a multiplayer game
        {
            StartCoroutine(SpawnPowerUps());
        }
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval);

            Vector2 spawnPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            GameObject powerUpPrefab = Random.value > 0.5f ? ammoPowerUpPrefab : healthPowerUpPrefab;

            var powerUpInstance = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
            powerUpInstance.GetComponent<NetworkObject>().Spawn();
        }
    }
}
