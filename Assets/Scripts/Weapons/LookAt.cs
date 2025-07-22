using Photon.Pun;
using TMPro;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Vector3 worldPosition;
    private Vector3 screenPosition;
    public GameObject crosshair;
    // public TMP_Text nickName;


  

    void FixedUpdate()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = 3f;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;
        crosshair.transform.position = Input.mousePosition;
    }
}
