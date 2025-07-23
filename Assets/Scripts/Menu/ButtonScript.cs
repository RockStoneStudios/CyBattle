using UnityEngine;
using Photon.Pun;

public class ButtonScript : MonoBehaviour
{
    private GameObject[] players;
    private int myId;
    [SerializeField] GameObject panel;
    private GameObject namesObject;

    private void Start()
    {
        Cursor.visible = true;
        panel = GameObject.Find("Choose Panel");
        namesObject = GameObject.Find("NamesBg");
    }


    public void SelectButton(int buttonNumber)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PhotonView>().IsMine)
            {
                myId = players[i].GetComponent<PhotonView>().ViewID;
                break;
            }
        }
        GetComponent<PhotonView>().RPC("SelectedColor", RpcTarget.AllBuffered, buttonNumber, myId);
        Cursor.visible = false;
        Debug.Log(panel);
        panel.SetActive(false);
    }

    [PunRPC]
    void SelectedColor(int buttonNumber, int myId)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<DisplayColor>().viewId[buttonNumber] = myId;
            players[i].GetComponent<DisplayColor>().ChooseColor();
        }
        namesObject.GetComponent<Timer>().BeginTimer();
        gameObject.transform.gameObject.SetActive(false);


    }
   
}
