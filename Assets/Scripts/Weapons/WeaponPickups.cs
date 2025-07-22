using UnityEngine;
using Photon.Pun;
using System.Collections;

public class WeaponPickups : MonoBehaviour
{
    private AudioSource audioPlayer;
    [SerializeField] float respawnTime = 5.4f;
    [SerializeField] int weaponType = 1;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<PhotonView>().RPC("PlayPickupAudio", RpcTarget.All);
            gameObject.GetComponent<PhotonView>().RPC("TurnOff", RpcTarget.All);
        }
    }

    [PunRPC]
    void PlayPickupAudio()
    {
        audioPlayer.Play();
    }

    [PunRPC]
    void TurnOff()
    {
        if (weaponType == 1)
        {
            gameObject.transform.GetComponent<Renderer>().enabled = false;
            gameObject.transform.GetComponent<Collider>().enabled = false;

        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetComponent<Collider>().enabled = false;
        }
          StartCoroutine(WaitToRespawn());
    }

    [PunRPC]
    void TurnOn()
    {
        if (weaponType == 1)
        {
            gameObject.transform.GetComponent<Renderer>().enabled = true;
            gameObject.transform.GetComponent<Collider>().enabled = true;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetComponent<Collider>().enabled = true;
        }
      
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        gameObject.GetComponent<PhotonView>().RPC("TurnOn", RpcTarget.All);
    }








}
