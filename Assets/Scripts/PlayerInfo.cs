/*
MIT License

Copyright (c) 2020 Iowa State University, Nathan Sepich, Grace Freed, Michael Curtis, Kayla Dawson, Kelli Jackson, Liat Litwin

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public MazeController mazeController;
    private CharacterController controller;
    private float jumpSpeed = 8.0F;
    private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool onTrampoline = false;
    private GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        controller = mazeController.controller;
        //controller.enabled = false;
        gravity = mazeController.gravity;
        jumpSpeed = mazeController.jumpSpeed;
        cube = mazeController.slideObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(mazeController.targetWaypoint == 27 || mazeController.targetWaypoint == 58)
        {
            onTrampoline = true;
            controller.enabled = true;
        }
        else
        {
            onTrampoline = false;
            controller.enabled = false;
        }
        if (onTrampoline == true && controller.isGrounded) 
        {
            moveDirection.y = jumpSpeed;
        }
        if(onTrampoline)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
            controller.enabled = false;
        }
        if(mazeController.targetWaypoint == 61)
        {
            cube.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            cube.GetComponent<Rigidbody>().useGravity = false;
        }
    
    }
}
