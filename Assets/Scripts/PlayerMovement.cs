using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private KeyCode lastHitKey;
    public float moveSpeed = 5.0f;
    [SerializeField] Transform movePoint;

    public LayerMask moveStoppers;
    public Animator animator;

    [SerializeField] private float rayCheckDistance = 1.0f;
    [SerializeField] private Vector2 moveDirectionVector = Vector2.right;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {

            if (Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, moveStoppers))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }

            if (Math.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, moveStoppers))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            lastHitKey = KeyCode.W;
            moveDirectionVector=Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastHitKey = KeyCode.A;
            moveDirectionVector=Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastHitKey = KeyCode.S;
            moveDirectionVector=Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastHitKey = KeyCode.D;
            moveDirectionVector=Vector2.right;
        }
        CheckForCube();
    
    }

    void CheckForCube() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirectionVector, rayCheckDistance);

        //using cubes
        if (Input.GetKeyDown(KeyCode.Z)&&hit && hit.collider.CompareTag("Cube")) {
            Debug.Log($"Ouch, just hit {hit.collider.name}!!");
            var pc = hit.collider.gameObject.GetComponent<PuzzleCube>();
            pc.pressed=true;
        }

        //using clue sheets
        if (Input.GetKeyDown(KeyCode.Z)&&hit && hit.collider.CompareTag("ClueSheet")) {
            var clue = hit.collider.gameObject.GetComponent<ClueSheetBehavior>();
            Debug.Log("A");
            clue.sheet.SetActive(true);
        }

        //picking up Phi
        if (Input.GetKeyDown(KeyCode.Z)&&hit && hit.collider.CompareTag("Phi")) {
            var clue = hit.collider.gameObject.GetComponent<ClueSheetBehavior>();
            
        }
    }

}
