using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Buton : MonoBehaviour
{
    public void OyunSahnesi()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom("YeniOda", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
            SceneManager.LoadScene("Game2");
        }
    }

    public void LobiyeGit()
    {
        PhotonNetwork.LeaveLobby();
    }
}
