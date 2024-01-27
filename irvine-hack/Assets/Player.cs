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
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A)) {
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            sr.flipX = false;
        }
    }
}
