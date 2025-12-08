using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    [SerializeField] Transform movePoint;
    bool movingH = false;
    bool movingV = false;

    public LayerMask moveStoppers;
    private Animator animator;

    [SerializeField] private float rayCheckDistance = 1.0f;
    [SerializeField] private Vector2 moveDirectionVector = Vector2.right;

    [SerializeField] public GameObject textBoxPrefab;

    void Start()
    {
        movePoint.parent = null;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (textBoxPrefab.activeInHierarchy)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("InputY", 0);
            animator.SetFloat("InputX", 0);
        }


        if (!textBoxPrefab.activeInHierarchy)
        {

            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
            {

                if (Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, moveStoppers) && !movingV)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        movingH = true;
                    }
                }
                else { movingH = false; }

                if (Math.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, moveStoppers) && !movingH)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        movingV = true;
                    }
                }
                else { movingV = false; }
            }

            if (movingH || movingV)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("InputY", 0);
                animator.SetFloat("InputX", 0);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (SceneManager.GetActiveScene().name == "BossDefeatRoom"))
            {
                animator.SetFloat("LastInputY", 1);
                animator.SetFloat("InputY", 1);
                moveDirectionVector = Vector2.up;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetFloat("LastInputX", -1);
                animator.SetFloat("InputX", -1);
                moveDirectionVector = Vector2.left;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetFloat("LastInputY", -1);
                animator.SetFloat("InputY", -1);
                moveDirectionVector = Vector2.down;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetFloat("LastInputX", 1);
                animator.SetFloat("InputX", 1);
                moveDirectionVector = Vector2.right;
            }

            if (moveDirectionVector != Vector2.up && moveDirectionVector != Vector2.down)
            { animator.SetFloat("LastInputY", 0); }

            if (moveDirectionVector != Vector2.left && moveDirectionVector != Vector2.right)
            { animator.SetFloat("LastInputX", 0); }

            CheckForCube();

        }
    }

    void CheckForCube() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirectionVector*rayCheckDistance);

        //using cubes
        if (Input.GetKeyDown(KeyCode.Z)&&hit && hit.collider.CompareTag("Cube")) {
            Debug.Log($"{hit.collider.name} pressed");
            var pc = hit.collider.gameObject.GetComponent<PuzzleCube>();
            pc.pressed=true;
        }

        //using clue sheets
        if (Input.GetKeyDown(KeyCode.Z)&&hit && hit.collider.CompareTag("ClueSheet")) {
            var clue = hit.collider.gameObject.GetComponent<ClueSheetBehavior>();
            //Debug.Log("A");
            clue.sheet.SetActive(true);
            moveSpeed=0.0f;
        }

        //picking up Phi
        if (Input.GetKeyDown(KeyCode.Z)&&hit && hit.collider.CompareTag("Phi")) {
            var phi = hit.collider.gameObject;
            phi.SetActive(false);
            Debug.Log("frien");

            if (!textBoxPrefab.activeInHierarchy)
            {
                textBoxPrefab.SetActive(true);
            }

        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawRay(transform.position, moveDirectionVector*rayCheckDistance);
    }

/*
    public void Move(InputAction.CallbackContext context){
        

        if(context.canceled){
            
            
            animator.SetFloat("LastInputY",movePoint.position.y);
        }
        moveDirectionVector = context.ReadValue<Vector2>();
        animator.SetFloat("InputX",movePoint.position.x);
        animator.SetFloat("InputY",movePoint.position.y);
    }
    */

}
