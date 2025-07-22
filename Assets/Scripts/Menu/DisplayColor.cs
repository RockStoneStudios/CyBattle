using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class DisplayColor : MonoBehaviour
{
    public int[] buttonNumbers;
    public int[] viewId;
    public Color32[] colors;

    private GameObject namesObject;

    private void Start() {
        namesObject = GameObject.Find("NamesBg");
    }


    public void ChooseColor()
    {
        GetComponent<PhotonView>().RPC("AssignColor", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void AssignColor()
    {
        for (int i = 0; i < viewId.Length; i++)
        {
            if (gameObject.GetComponent<PhotonView>().ViewID == viewId[i])
            {
                gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = colors[i];
                namesObject.GetComponent<NickNames>().names[i].text = gameObject.GetComponent<PhotonView>().Owner.NickName;
            }
        }
    }


}
