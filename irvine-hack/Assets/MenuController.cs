using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviourPunCallbacks
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton;

  private void Awake()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = VersionName;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start() {
        UsernameMenu.SetActive(true);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("connected");
    }

    public void ChangeUserNameInput() {
        if (UsernameInput.text.Length >= 3) {
            StartButton.SetActive(true);
        } else {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName() {
        UsernameMenu.SetActive(false);
        PhotonNetwork.NickName = UsernameInput.text;
    }

    public void CreateGame() {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 5 }, null);
    }

    public void JoinGame() {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("MainGame");
    }
}