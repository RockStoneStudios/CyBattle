using UnityEngine;
using Photon.Pun; // Importa Photon PUN, que permite funcionalidades multijugador basadas en red.

// La clase GameManager hereda de MonoBehaviourPunCallbacks para manejar eventos de Photon.
public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Habilita la sincronización automática de escenas entre todos los jugadores.
        PhotonNetwork.AutomaticallySyncScene = true;

        // Inicia la conexión con los servidores de Photon usando los valores de PhotonServerSettings.
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        // Método de Unity que se ejecuta cada frame.
        // En este caso está vacío, pero puede usarse para lógica del juego más adelante.
    }

    // Callback que se ejecuta automáticamente cuando la conexión con el servidor maestro de Photon es exitosa.
    public override void OnConnectedToMaster()
    {
        Debug.Log("I'm Connected to the server!");

        // Intenta unirse a una sala aleatoria existente.
        PhotonNetwork.JoinRandomRoom();
    }

    // Callback que se ejecuta cuando se ha unido exitosamente a una sala.
    public override void OnJoinedRoom()
    {
        // Carga la escena llamada "Floor layout" de forma sincronizada para todos los jugadores.
        PhotonNetwork.LoadLevel("Floor layout");
    }

    // Callback que se ejecuta si falló al unirse a una sala aleatoria.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // Si no hay salas disponibles, crea una nueva sala con el nombre "Arena1".
        PhotonNetwork.CreateRoom("Arena1");
    }
}
