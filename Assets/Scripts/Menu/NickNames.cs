using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;

public class NickNames : MonoBehaviourPunCallbacks
{
    public TMP_Text[] names;
    public Image[] healthBars;
    private GameObject waitObject;

    private void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].gameObject.SetActive(false);
            healthBars[i].gameObject.SetActive(false);
        }
        waitObject = GameObject.Find("Waiting Bg");
    }


    public void Leaving()
    {
        StartCoroutine("BackToLobby");
    }

    IEnumerator BackToLobby()
    {
        yield return new WaitForSeconds(0.54f);
        PhotonNetwork.LoadLevel("Lobby");

    }

    public void returnToLobby()
    {
        waitObject.SetActive(false);
        RoomExit();
    }


    void RoomExit()
    {
        StartCoroutine(ToLobby());
    }

    IEnumerator ToLobby()
    {
        yield return new WaitForSeconds(0.47f);
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}

































