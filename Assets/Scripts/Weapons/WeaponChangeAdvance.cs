using UnityEngine;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using Photon.Pun;

public class WeaponChangeAdvance : MonoBehaviour
{
    public TwoBoneIKConstraint leftHand;
    public TwoBoneIKConstraint rightHand;
    private CinemachineCamera cam;
    private GameObject camObject;
    public MultiAimConstraint[] aimObjects;
    private Transform aimTarget;

    public RigBuilder rig;
    public Transform[] leftTargets;
    public Transform[] rightTargets;
    public GameObject[] weapons;
    private int weaponNumber = 0;


    void Start()
    {
        camObject = GameObject.Find("PlayerCam");
        // aimTarget = GameObject.Find("AimRef").transform;
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            cam = camObject.GetComponent<CinemachineCamera>();
            cam.Follow = gameObject.transform;
            cam.LookAt = gameObject.transform;
            // Invoke("SetLookAt", 0.1f);

        }
        else
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    void SetLookAt()
    {
        if (aimTarget != null)
        {
            for (int i = 0; i < aimObjects.Length; i++)
            {
                var target = aimObjects[i].data.sourceObjects;
                target.SetTransform(0, aimTarget.transform);
                aimObjects[i].data.sourceObjects = target;
            }
            rig.Build();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            weaponNumber++;
            if (weaponNumber > weapons.Length - 1)
            {
                weaponNumber = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[weaponNumber].SetActive(true);
            leftHand.data.target = leftTargets[weaponNumber];
            rightHand.data.target = rightTargets[weaponNumber];
            rig.Build();
         
        }
    }
}
