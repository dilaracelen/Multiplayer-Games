using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hareket : MonoBehaviour
{
    PhotonView pw;
    TMPro.TextMeshProUGUI yaz�Text;

    private void Start()
    {
        yaz�Text = GameObject.Find("Canvas/Panel/yaz�Text").GetComponent<TMPro.TextMeshProUGUI>();
        pw =  GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(7, 0, 0);
                InvokeRepeating("OyuncuKontrol", 0, 0.5f);
            }
            else if(!PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(-7, 0, 0);
            }
        }
    }

    void OyuncuKontrol()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            pw.RPC("Yaz�Sil", RpcTarget.All, null);
            GameObject.Find("Top").GetComponent<PhotonView>().RPC("Ba�la", RpcTarget.All, null);
            CancelInvoke("OyuncuKontrol");
        }
    }

    [PunRPC]
    public void Yaz�Sil()
    {
        yaz�Text.text = null;
    }

    private void Update()
    {
        if (pw.IsMine)
            HareketEt();
    }

    void HareketEt()
    {
        float dikey = Input.GetAxis("Mouse Y") * Time.deltaTime * 7;
        transform.Translate(0, dikey, 0);
    }

    [PunRPC]
    public void OyuncuKa�t�()
    {
        InvokeRepeating("OyuncuKontrol", 0, 0.5f);
        yaz�Text.text = "OYUNCU BEKLENIYOR...";
        GameObject.FindWithTag("Top").GetComponent<PhotonView>().RPC("OyuncuKa�t�", RpcTarget.All, null);
    }
}
