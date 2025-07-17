using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private bool IsJump = true;
    private Rigidbody rb;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        // if (movement != Vector3.zero)
        // {

        //    Vector3 rotateY = new Vector3(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.fixedDeltaTime, 0);
        //     rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateY));
        // }
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f)
        {
                Vector3 rotateY = new Vector3(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.fixedDeltaTime, 0);
                rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateY));
        }

        rb.MovePosition(rb.position + transform.forward * Input.GetAxis
        ("Vertical") * moveSpeed * Time.deltaTime + transform.right *
        Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        anim.SetFloat("BlendV", Input.GetAxis("Vertical"));
        anim.SetFloat("BlendH", Input.GetAxis("Horizontal"));

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsJump)
        {
            IsJump = false;
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            StartCoroutine(JumpAgainRoutine());
        }
    }

    IEnumerator JumpAgainRoutine() {
        yield return new WaitForSeconds(1.1f);
        IsJump = true;
    }






}
