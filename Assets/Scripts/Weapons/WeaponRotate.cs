using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    [SerializeField] float speed = 20f;



    
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
