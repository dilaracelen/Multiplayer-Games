using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Y�net : MonoBehaviourPunCallbacks
{
    static Y�net y�netici = null;

    private void Start()
    {
        if(y�netici == null)
        {
            y�netici = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        GameObject yeniOyuncu = PhotonNetwork.Instantiate("Tahta", Vector3.zero, Quaternion.identity, 0);
        yeniOyuncu.GetComponent<PhotonView>().Owner.NickName = "Misafir " + Random.Range(1, 10);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) 
    {
        GameObject.FindWithTag("Player").GetComponent<PhotonView>().RPC("OyuncuKa�t�", RpcTarget.All, null);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu2");
    }
}