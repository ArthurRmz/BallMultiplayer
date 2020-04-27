using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour
{
    public string Version = "1.0";
    private const int MAX_PLAYERS = 4;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(Version);
    }

    public override void OnConnectedToPhoton()
    {
        Debug.Log("[NetworkManager][OnConnectedToPhoton]");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("[NetworkManager][OnConnectedToMaster]");
        var options = new RoomOptions();
        options.MaxPlayers = MAX_PLAYERS;
        PhotonNetwork.JoinOrCreateRoom("Global", options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("[NetworkManager][OnCreatedRoom]");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[NetworkManager][OnJoinedRoom]");
        PhotonNetwork.Instantiate("Sphere", Vector3.zero, Quaternion.identity, 0);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Esta funcion solo es por un fallo que da en el juego
        Debug.Log("[NetworkManager][OnPhotonSerializeView]" + info.ToString());
    }
}
