using UnityEngine;
using Photon.Pun;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text minutesText;
    public TMP_Text secondsText;
    public int minutes = 4;
    public int seconds = 59;

    public void BeginTimer()
    {
        GetComponent<PhotonView>().RPC("Count", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void Count()
    {
        BeginCounting();
    }

    void BeginCounting()
    {
        CancelInvoke();
        InvokeRepeating("TimeCountDown", 1, 1);
    }

    void TimeCountDown()
    {
        if (seconds > 10)
        {
            seconds -= 1;
            secondsText.text = seconds.ToString();
        }
        else if (seconds > 0 && seconds < 11)
        {
            seconds -= 1;
            secondsText.text = "0" + seconds.ToString();
        }
        else if (seconds == 0 && minutes > 0)
        {
            secondsText.text = "0" + seconds.ToString();
            minutes -= 1;
            seconds = 59;
            minutesText.text = minutes.ToString();
            secondsText.text = seconds.ToString();
        }
    }








    
}
