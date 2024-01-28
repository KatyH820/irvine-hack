using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviourPun
{
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;

    private void Awake() {
        if (photonView.IsMine) {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.NickName;
        } else {
            PlayerNameText.text = photonView.Owner.NickName;
            PlayerNameText.color = Color.cyan;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) {
            CheckInput();
        }
    }

    private void CheckInput() {
        var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // transform.position += move * MoveSpeed * Time.deltaTime;
        Debug.Log(MoveSpeed);
        Debug.Log(move.x);
        Debug.Log(move.y);
        rb.velocity = new Vector2(move.x * MoveSpeed, move.y * MoveSpeed);

        if (Input.GetKeyDown(KeyCode.A)) {
            photonView.RPC("FlipTrue", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            photonView.RPC("FlipFalse", RpcTarget.AllBuffered);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.W)) {
            anim.SetBool("isRunningUp", true);
        } else {
            anim.SetBool("isRunningUp", false);
        }
        
        if (Input.GetKey(KeyCode.S)) {
            anim.SetBool("isRunningDown", true);
        } else {
            anim.SetBool("isRunningDown", false);
        }
    }

    [PunRPC]
    private void FlipTrue() {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipFalse() {
        sr.flipX = false;
    }
}
