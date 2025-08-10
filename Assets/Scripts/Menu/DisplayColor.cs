using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DisplayColor : MonoBehaviourPunCallbacks
{
    public int[] buttonNumbers;
    public int[] viewId;
    public Color32[] colors;

    private GameObject namesObject;
    private GameObject waitForPlayers;

    public AudioClip[] gunsShotSounds;

    private void Start()
    {
        namesObject = GameObject.Find("NamesBg");
        waitForPlayers = GameObject.Find("Waiting Bg");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GetComponent<PhotonView>().IsMine && waitForPlayers.activeInHierarchy == false)
            {
                RemoveData();
                RoomExit();
            }
        }


    }

    public void DeliverDamage(string name, float damageAmt) {
        GetComponent<PhotonView>().RPC("GunDamage", RpcTarget.AllBuffered, name,damageAmt);
    }

    [PunRPC]
        void GunDamage(string name, float damageAmt)
        {
            for (int i = 0; i < namesObject.GetComponent<NickNames>().names.Length; i++)
            {
                if (name == namesObject.GetComponent<NickNames>().names[i].text)
                {
                
                    namesObject.GetComponent<NickNames>().healthBars[i].gameObject.GetComponent<Image>().fillAmount -=  damageAmt;
                }
            }
        }

    void RemoveData()
    {
        GetComponent<PhotonView>().RPC("RemoveMe", RpcTarget.AllBuffered);
    }


    void RoomExit()
    {
        StartCoroutine(GetReadyToLeave());
    }


    public void ChooseColor()
    {
        GetComponent<PhotonView>().RPC("AssignColor", RpcTarget.AllBuffered);
    }

    public void PlayGunShot(string name, int weaponNumber)
    {
        GetComponent<PhotonView>().RPC("PlaySound", RpcTarget.All, name, weaponNumber);
    }

    [PunRPC]
    void PlaySound(string name, int weaponNumber)
    {
        for (int i = 0; i < namesObject.GetComponent<NickNames>().names.Length; i++)
        {
            if (name == namesObject.GetComponent<NickNames>().names[i].text)
            {
                GetComponent<AudioSource>().clip = gunsShotSounds[weaponNumber];
                GetComponent<AudioSource>().Play();
            }
        }
    }

    [PunRPC]
    void AssignColor()
    {
        for (int i = 0; i < viewId.Length; i++)
        {
            if (gameObject.GetComponent<PhotonView>().ViewID == viewId[i])
            {
                gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = colors[i];
                namesObject.GetComponent<NickNames>().names[i].gameObject.SetActive(true);
                namesObject.GetComponent<NickNames>().healthBars[i].gameObject.SetActive(true);
                namesObject.GetComponent<NickNames>().names[i].text = gameObject.GetComponent<PhotonView>().Owner.NickName;
            }
        }
    }


    [PunRPC]
    void RemoveMe()
    {
        for (int i = 0; i < namesObject.gameObject.GetComponent<NickNames>().names.Length; i++)
        {
            if (gameObject.GetComponent<PhotonView>().Owner.NickName == namesObject.GetComponent<NickNames>().names[i].text)
            {
                namesObject.GetComponent<NickNames>().names[i].gameObject.SetActive(false);
                namesObject.GetComponent<NickNames>().healthBars[i].gameObject.SetActive(false);
            }
        }
    }


    IEnumerator GetReadyToLeave()
    {
        yield return new WaitForSeconds(1);
        namesObject.GetComponent<NickNames>().Leaving();
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();

    }

}















