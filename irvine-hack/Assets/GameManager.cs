using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    public GameObject DisconnectUI;
    public Text PingText;
    private bool Off = false;

    public GameObject PlayerFeed;
    public GameObject FeedGrid;

    private void Awake() {
        GameCanvas.SetActive(true);
    }

    public void SpawnPlayer() {
        float randomValue = Random.Range(-1f, 1f);
        Debug.Log("Fuck");

        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }

    private void CheckInput() {
        if (Off && Input.GetKeyDown(KeyCode.Escape)) {
            DisconnectUI.SetActive(false);
            Off = false;
        } else if (!Off && Input.GetKeyDown(KeyCode.Escape)) {
            DisconnectUI.SetActive(true);
            Off = true;
        }
    }

    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    private void OnPhotonPlayerConnected(Player player) {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name = "joined the game";
        obj.GetComponent<Text>().color = Color.green;
    }

    private void OnPhotonPlayerDisconnected(Player player) {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name = "left the game";
        obj.GetComponent<Text>().color = Color.red;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        PingText.text = "Ping: " + PhotonNetwork.GetPing();
    }
}
