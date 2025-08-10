using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerCheck : MonoBehaviour
{
    public int maxPlayersInRoom = 1;
    public TMP_Text currentPlayers;
    public GameObject hint1, hint2;
    public GameObject enterButton;


    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersInRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            hint1.SetActive(false);
            hint2.SetActive(false);
            enterButton.SetActive(true);

        }
        if (enterButton.activeInHierarchy != true)
        {

            currentPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + maxPlayersInRoom.ToString();
        }
        else
        {
            currentPlayers.text = "";
        }
    }

    public void EnterTheArena()
    {
        gameObject.SetActive(false);
    }
}
