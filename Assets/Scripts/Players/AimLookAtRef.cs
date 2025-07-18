using UnityEngine;
using Photon.Pun;

public class AimLookAtRef : MonoBehaviour
{
    private GameObject lookAtObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookAtObject = GameObject.Find("AimRef");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<PhotonView>().IsMine)
        {
            transform.position = lookAtObject.transform.position;
        }
    }
}
