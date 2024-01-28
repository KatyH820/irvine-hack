using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPun, IPunObservable
{
    public Player plMove;
    public PhotonView photonView;
    public GameObject BubbleSpeechObject;
    public Text UpdatedText;

    private InputField ChatInputField;
    private bool DisableSend;

    private void Awake() {
        ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) {
            if (!DisableSend && ChatInputField.isFocused) {
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Slash)) {
                    photonView.RPC("SendMessage", RpcTarget.AllBuffered, ChatInputField.text);
                    BubbleSpeechObject.SetActive(true);

                    ChatInputField.text = "";
                    DisableSend = true;
                }
            }
        }
    }

    [PunRPC]
    private void SendMessage(string message) {
        UpdatedText.text = message;

        StartCoroutine("Remove");
    }

    IEnumerator Remove() {
        yield return new WaitForSeconds(4f);
        BubbleSpeechObject.SetActive(false);
        DisableSend = false;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(BubbleSpeechObject.active);
        } else if (stream.IsReading) {
            BubbleSpeechObject.SetActive((bool)stream.ReceiveNext());
        }
    }
}
