using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    //public Rigidbody theRB;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;
    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    public float playerPosX, playerPosY,playerPosZ;

    

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3 (playerPosX,playerPosY,playerPosZ);

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, moveDirection.y, Input.GetAxis("Vertical") * MoveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + ( transform.right *Input.GetAxis("Horizontal") * MoveSpeed);
        moveDirection = moveDirection.normalized * MoveSpeed;
        moveDirection.y = yStore ;
        if (controller.isGrounded)
        {
            //moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y = moveDirection.y +( Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
        //move the palyer in diffrent direction based on camera look direction 
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") !=0) 
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation =Quaternion.Slerp(playerModel.transform.rotation ,newRotation,rotateSpeed * Time.deltaTime);

        }
        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
    

}
