using UnityEngine;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using System.Collections;


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
    private GameObject testForWeapons;
    private Image weaponIcon;
    private TMP_Text ammoAmtText;
    public Sprite[] weaponIcons;
    public int[] ammoAmts;
    public GameObject[] muzzleFlash;
    private string shooterName; 
    private string gotShotName; 
    public float[] damageAmts;


    void Start()
    {
        weaponIcon = GameObject.Find("WeaponUI").GetComponent<Image>();
        ammoAmtText = GameObject.Find("AmmoAmt").GetComponent<TMP_Text>();
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
        testForWeapons = GameObject.Find("Weapon1Pickup(Clone)");
        if (testForWeapons == null)
        {
            var spawner = GameObject.Find("SpawnerPlayer");
            spawner.GetComponent<SpawnerCharacters>().SpawnWeaponsStart();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponent<DisplayColor>().PlayGunShot(GetComponent<PhotonView>().Owner.NickName, weaponNumber);
                gameObject.GetComponent<PhotonView>().RPC("GunMuzzleFlash", RpcTarget.All);
                RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                        if (Physics.Raycast(ray, out hit, 500))
                        {
                    if (hit.transform.gameObject.GetComponent<PhotonView>() != null)
                    {
                        gotShotName = hit.transform.gameObject.GetComponent<PhotonView>().Owner.NickName;
                     
                    }
                    if (hit.transform.gameObject.GetComponent<DisplayColor>() != null)
                    {
                           hit.transform.gameObject.GetComponent<DisplayColor>().DeliverDamage(hit.transform.gameObject.GetComponent<PhotonView>().Owner.NickName, damageAmts[weaponNumber]);
                    }
                        shooterName = GetComponent<PhotonView>().Owner. NickName; Debug.Log(gotShotName + " got hit by " + shooterName);
                }
                          gameObject.layer = LayerMask. NameToLayer("Default");
            }
            
        }
        if (Input.GetMouseButtonDown(1) && gameObject.GetComponent<PhotonView>().IsMine)
        {
            // weaponNumber++;
            gameObject.GetComponent<PhotonView>().RPC("Change", RpcTarget.AllBuffered);
            if (weaponNumber > weapons.Length - 1)
            {
                weaponIcon.GetComponent<Image>().sprite = weaponIcons[0];
                ammoAmtText.text = ammoAmts[0].ToString();
                weaponNumber = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[weaponNumber].SetActive(true);
            weaponIcon.GetComponent<Image>().sprite = weaponIcons[weaponNumber];
            ammoAmtText.text = ammoAmts[weaponNumber].ToString();
            leftHand.data.target = leftTargets[weaponNumber];
            rightHand.data.target = rightTargets[weaponNumber];
            rig.Build();

        }
    }


    [PunRPC]
    void GunMuzzleFlash()
    {
        muzzleFlash[weaponNumber].SetActive(true);
        StartCoroutine(MuzzleOff());
    }


    [PunRPC]
    public void Change()
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



    IEnumerator MuzzleOff()
    {
        yield return new WaitForSeconds(0.031f);
        gameObject.GetComponent<PhotonView>().RPC("MuzzleFlashOff", RpcTarget.All);
       
    }

    [PunRPC]
    void MuzzleFlashOff()
    {
     muzzleFlash[weaponNumber].SetActive(false);
    }

}










