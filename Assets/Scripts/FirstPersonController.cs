using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Vector3 = UnityEngine.Vector3;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] Camera controllerCamera;
    [SerializeField] float playerSpeed;
    [SerializeField] float mouseSensitivity;

    [SerializeField] float jumpMultiplier=2;
    CharacterController controller;
    private Vector3 velocity;
    private Rigidbody playerRb;
    

    float yRotation=0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        controller=GetComponent<CharacterController>();
        playerRb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        updateCamera();
        Move();
        UpdateGravity();
        Jump();

    }
    void updateCamera(){
        float mouseXInput=Input.GetAxis("Mouse X");
        float mouseYInput=Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0,mouseXInput*Time.deltaTime*mouseSensitivity,0));
        yRotation+=mouseYInput*mouseSensitivity*Time.deltaTime;
        controllerCamera.transform.eulerAngles=new Vector3(yRotation*-1,transform.eulerAngles.y,0);
    }
    void Move(){
        float verticalInput=Input.GetAxis("Vertical");
        float horizontalInput=Input.GetAxis("Horizontal");
        Vector3 verticalMovement=verticalInput*transform.forward*Time.deltaTime*playerSpeed;
        Vector3 horizontalMovement=horizontalInput*transform.right*Time.deltaTime*playerSpeed;
        Vector3 movement=verticalMovement+horizontalMovement+(velocity*Time.deltaTime);
        controller.Move(movement);
        

    }
    void UpdateGravity(){
        if(controller.isGrounded){
            velocity=new Vector3(0,-1,0);
        }else{
            Vector3 gravity=Physics.gravity;
            float mass=playerRb.mass;
            velocity=new Vector3(0,velocity.y+(gravity*mass*Time.deltaTime).y,0);
        }
    }

    void Jump(){
        if(Input.GetKeyDown(KeyCode.Space)&&controller.isGrounded){
            velocity.y=velocity.y+playerSpeed*jumpMultiplier;
        }
    }
    void knewDown(){
        if(Input.GetKey(KeyCode.LeftShift)){
            controller.height=1.1f;
        }else{
            controller.height=2f;
        }
    }
}
