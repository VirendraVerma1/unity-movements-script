using Photon.Pun;
using UnityEngine;

public class PhotonPlayerManager : MonoBehaviour
{
    public GameObject PhotonPlayerPrefab;
    public Transform[] PlayerSpawnPositions;
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        //spawn the photon player
        GameObject go = PhotonNetwork.Instantiate(PhotonPlayerPrefab.name, PlayerSpawnPositions[getPlayerCount()].transform.position, PlayerSpawnPositions[getPlayerCount()].transform.rotation);

    }

    
    #region common methods

    int getPlayerCount()
    {
        return PhotonNetwork.PlayerList.Length;
    }
    
    #endregion
    
}
