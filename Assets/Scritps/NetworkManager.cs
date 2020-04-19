using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.MonoBehaviour
{
    public string Version = "1.0";
    private const int MAX_PLAYERS = 4;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(Version);
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Global", new RoomOptions() { MaxPlayers = MAX_PLAYERS },null);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Sphere", transform.position, transform.rotation, 0);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Esta funcion solo es por un fallo que da en el juego
        Debug.Log("[NetworkManager][OnPhotonSerializeView]" + info.ToString());
    }
}
