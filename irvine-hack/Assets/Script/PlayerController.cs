using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;

    // public LayerMask solidObjectsLayer;
    // public LayerMask interactableLayer;


    // private Animator animator;

    // Awake is called when the script instance is being loaded.
    // private void Awake()
    // {
    // 	animator = GetComponent<Animator>();
    // }

    // public void HandleUpdate()
    public void Update()
    {
        if (!isMoving)
        {

            //record any movement (then store inputs)
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");




            // if user type in other keys that that are not zero
            if (input != Vector2.zero)
            {
                // animator.SetFloat("moveX", input.x);
                // animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x; // increment to x position
                targetPos.y += input.y; // increment to y position
                StartCoroutine(Move(targetPos));

                // if (isWalkable(targetPos))
                // {
                //     StartCoroutine(Move(targetPos)); // running constantly in game
                // }
            }
        }
        // animator.SetBool("isMoving", isMoving);

        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     Interact();
        // }

    }

    // void Interact()
    // {
    //     var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
    //     var interactPos = transform.position + facingDir;
    //     // Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

    //     var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
    //     if (collider != null)
    //     {
    //         collider.GetComponent<Interactable>()?.Interact();
    //     }
    // }

    // function running on Routine manner
    IEnumerator Move(Vector3 targetPos)
    {
        // any movement that bigger zero, it means that user moves
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // take original position and move to target position
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
    // private bool isWalkable(Vector3 targetPos)
    // {
    //     // if there is any solid object or interactable object, then return false
    //     if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
    //     {
    //         return false;
    //     }
    //     return true;
    // }

}
