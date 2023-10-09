using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Top : MonoBehaviour
{
    Rigidbody rb;
    PhotonView pw;

    int oyuncu1Skor = 0;
    int oyuncu2Skor = 0;

    public TMPro.TextMeshProUGUI oyuncu1Text;
    public TMPro.TextMeshProUGUI oyuncu2Text;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pw = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Baþla()
    {
        rb.velocity = new Vector3 (5, 5, 0);
        SkorGöster();
    }

    public void SkorGöster()
    {
        oyuncu1Text.text = PhotonNetwork.PlayerList[0].NickName + ": " + oyuncu1Skor.ToString();
        oyuncu2Text.text = PhotonNetwork.PlayerList[1].NickName + ": " + oyuncu2Skor.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pw.IsMine)
        {
            if (collision.gameObject.name == "Oyuncu1_Kale")
                pw.RPC("Gol", RpcTarget.AllBuffered, 0, 1);

            else if (collision.gameObject.name == "Oyuncu2_Kale")
                pw.RPC("Gol", RpcTarget.AllBuffered, 1, 0);
        }
    }

    [PunRPC]
    public void Gol(int oyuncuBir, int oyuncuÝki)
    {
        oyuncu1Skor += oyuncuBir;
        oyuncu2Skor += oyuncuÝki;

        SkorGöster();

        Servis();
    }

    public void Servis()
    {
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(-1, 0, 0);

        rb.velocity = new Vector3(5, 5, 0);
    }

    public void OyuncuKaçtý()
    {
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, 0, 0);
    }
}
