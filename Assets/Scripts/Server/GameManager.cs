using UnityEngine;
using Photon.Pun; // Importa Photon PUN, que permite funcionalidades multijugador basadas en red.
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// La clase GameManager hereda de MonoBehaviourPunCallbacks para manejar eventos de Photon.
public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField playerNickname;
    [SerializeField] private string setName = "";
    [SerializeField] GameObject connecting;
    void Start()
    {

        connecting.SetActive(false);
       
    }

    public void UpdateText()
    {
        setName = playerNickname.text;
        PhotonNetwork.LocalPlayer.NickName = setName;
    }

    public void EnterButton()
    {
        if (setName != "")
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            connecting.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    // Callback que se ejecuta automáticamente cuando la conexión con el servidor maestro de Photon es exitosa.
    public override void OnConnectedToMaster()
    {
        Debug.Log("I'm Connected to the server!");
        SceneManager.LoadScene("Lobby");
       
    }

    // // Callback que se ejecuta cuando se ha unido exitosamente a una sala.
    // public override void OnJoinedRoom()
    // {
    //     // Carga la escena llamada "Floor layout" de forma sincronizada para todos los jugadores.
    //     PhotonNetwork.LoadLevel("Floor layout");
    // }

    // // Callback que se ejecuta si falló al unirse a una sala aleatoria.
    // public override void OnJoinRandomFailed(short returnCode, string message)
    // {
    //     // Si no hay salas disponibles, crea una nueva sala con el nombre "Arena1".
    //     PhotonNetwork.CreateRoom("Arena1");
    // }
}
