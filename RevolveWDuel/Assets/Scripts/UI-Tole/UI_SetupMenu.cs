using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_SetupMenu : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI playersCount;
    [SerializeField] private int requiredPlayers = 2; // Jumlah pemain yang diperlukan untuk memulai game
    [SerializeField] private string gameSceneName = "GameScene"; // Nama scene permainan

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    private void Awake() 
    {
        hostButton.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Jadi Host");
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            Debug.Log("Jadi Client");
        });
    }
    
    private void Update() 
    {
        playersCount.text = "Players : " + playersNum.Value.ToString();
        if (!IsServer)
        {
            return;
        }

        // Update jumlah pemain yang terhubung
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;

        // Cek apakah jumlah pemain sudah memenuhi syarat untuk mulai permainan
        if (playersNum.Value >= requiredPlayers)
        {
            StartGame();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void StartGameServerRpc()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            // Ganti scene ke scene permainan
            NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
    }

    private void StartGame()
    {
        // Panggil ServerRpc untuk mengganti scene ke semua client
        StartGameServerRpc();
    }
}
