using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Vector3 worldPosition;
    private Vector3 screenPosition;
    public GameObject crosshair;


    void Start()
    {
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = 3f;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;
        crosshair.transform.position = Input.mousePosition;
    }
}
