﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class state
{
    public string name = "Stand";
    public float speed;
    public float height;
    public Vector3 center;
    public Vector3 camPos;
}

public class Pl_Controller : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    public float hor;
    public float ver;
    public int state;
    public bool running;

    public state[] states = new state[3];
    private state curState;

    public Transform adjTrans;

    private float adjvar = 1;

    void Update()
    {
            CheckState();
            CheckInput();
            if (controller.isGrounded)
            {
                ver = Input.GetAxis("Vertical");
                hor = Input.GetAxis("Horizontal");

                if (Mathf.Abs(ver) > 0.1f && Mathf.Abs(hor) > 0.1f)
                {
                    adjvar = 0.701f;
                }
                else
                {
                    adjvar = 1f;
                }

                moveDirection = new Vector3(hor * adjvar, -2f, ver * adjvar);
                moveDirection = transform.TransformDirection(moveDirection);
                Debug.Log(moveDirection);
                moveDirection *= speed;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (state == 0)
                    {
                        moveDirection.y = jumpSpeed;
                    }
                    else if (state == 1)
                    {
                        state = 0;
                    }
                    else if (state == 2)
                    {
                        state = 1;
                    }
                }

            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
            //Debug.Log(moveDirection);
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.C) && controller.isGrounded)
        {
            if (state == 0) state = 1;
            else if (state == 1) state = 0;
            //else if (state == 2) state = 1;
        }
        running = (controller.isGrounded && controller.velocity.magnitude > 1 && state == 0 && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S));
    }

    void CheckState()
    {
        if (running)
        {
            curState = states[0];
        }
        else if (state == 0)
        {
            curState = states[1];
        }
        else if (state == 1)
        {
            curState = states[2];
        }

        //speed = curState.speed;
        controller.height = curState.height;
        controller.center = curState.center;
        //adjTrans.localPosition = Vector3.Lerp(adjTrans.localPosition, curState.camPos, Time.deltaTime * 10);
    }
}
