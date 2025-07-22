using UnityEngine;
using Photon.Pun;

public class SpawnerCharacters : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] weapons;
    [SerializeField] Transform[] weaponSpawnPoints;
    [SerializeField] float weaponRespawnTime = 10f;
    

   



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(character.name, spawnPoints[PhotonNetwork.CountOfPlayers - 1].position, spawnPoints[PhotonNetwork.CountOfPlayers - 1].rotation);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWeaponsStart()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            PhotonNetwork.Instantiate(weapons[i].name, weaponSpawnPoints[i].position, weaponSpawnPoints[i].rotation);
        }
    }
}
