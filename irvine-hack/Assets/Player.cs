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
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public bool isMoving;
    float speedX, speedY;
    public bool IsGrounded = false;
    public float moveSpeed;
    public float JumpForce;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (photonView.IsMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            PlayerNameText.text = photonView.Owner.NickName;
            PlayerNameText.color = Color.cyan;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
     public void FixedUpdate()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        Debug.Log("testestst " + Input.GetAxisRaw("Horizontal"));
        UnityEngine.Vector2 direction = new UnityEngine.Vector2(speedX, speedY).normalized;

        bool success = MovePlayer(direction);

        if (!success)
        {
            // Try Left / Right
            success = MovePlayer(new UnityEngine.Vector2(direction.x, 0));

            if (!success)
            {
                success = MovePlayer(new UnityEngine.Vector2(0, direction.y));
            }
        }
    }

     public bool MovePlayer(UnityEngine.Vector2 direction)
    {
        int count = rb.Cast(
           direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
           movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
           castCollisions, // List of collisions to store the found collisions into after the Cast is finished
           moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0 && (direction.x != 0 || direction.y != 0))
        {
            isMoving = true;
            anim.SetFloat("moveX", direction.x);
            anim.SetFloat("moveY", direction.y);
            anim.SetBool("isMoving", isMoving);

            UnityEngine.Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);

            return false;
        }
    }

    // private void CheckInput()
    // {
    //     var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //     // transform.position += move * MoveSpeed * Time.deltaTime;
    //     Debug.Log(MoveSpeed);
    //     Debug.Log(move.x);
    //     Debug.Log(move.y);
    //     rb.velocity = new Vector2(move.x * MoveSpeed, move.y * MoveSpeed);

    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         photonView.RPC("FlipTrue", RpcTarget.AllBuffered);
    //     }

    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         photonView.RPC("FlipFalse", RpcTarget.AllBuffered);
    //     }

    //     if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
    //     {
    //         anim.SetBool("isRunning", true);
    //     }
    //     else
    //     {
    //         anim.SetBool("isRunning", false);
    //     }

    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         anim.SetBool("isRunningUp", true);
    //     }
    //     else
    //     {
    //         anim.SetBool("isRunningUp", false);
    //     }

    //     if (Input.GetKey(KeyCode.S))
    //     {
    //         anim.SetBool("isRunningDown", true);
    //     }
    //     else
    //     {
    //         anim.SetBool("isRunningDown", false);
    //     }
    // }

    // [PunRPC]
    // private void FlipTrue()
    // {
    //     sr.flipX = true;
    // }

    // [PunRPC]
    // private void FlipFalse()
    // {
    //     sr.flipX = false;
    // }
}
