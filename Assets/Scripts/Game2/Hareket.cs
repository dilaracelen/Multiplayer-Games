using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hareket : MonoBehaviour
{
    PhotonView pw;
    TMPro.TextMeshProUGUI yazýText;

    private void Start()
    {
        yazýText = GameObject.Find("Canvas/Panel/yazýText").GetComponent<TMPro.TextMeshProUGUI>();
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
            pw.RPC("YazýSil", RpcTarget.All, null);
            GameObject.Find("Top").GetComponent<PhotonView>().RPC("Baþla", RpcTarget.All, null);
            CancelInvoke("OyuncuKontrol");
        }
    }

    [PunRPC]
    public void YazýSil()
    {
        yazýText.text = null;
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
    public void OyuncuKaçtý()
    {
        InvokeRepeating("OyuncuKontrol", 0, 0.5f);
        yazýText.text = "OYUNCU BEKLENIYOR...";
        GameObject.FindWithTag("Top").GetComponent<PhotonView>().RPC("OyuncuKaçtý", RpcTarget.All, null);
    }
}
