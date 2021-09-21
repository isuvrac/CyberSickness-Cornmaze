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
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    public MazeController mazeController;
    private GameObject player;
    private GameObject playerCam;
    public GameObject[] waypoints;
    private int startingWaypoint = 25;
    private int targetWaypoint = 26;
    private float moveSpeed;
    private float rotateSpeed;
    private bool constantSpeed;
    float timer = 0;
    private Quaternion lookRotation;
    private bool automatedMovement = true;
    private bool automatedRotation = true;
    private CharacterController controller;
    private GameObject cube;
    private GameObject elevator;
    private Vector3 slideAdjust;

    // Start is called before the first frame update
    void Start()
    {
        player = mazeController.playerRig;
        playerCam = mazeController.playerCam;
        automatedMovement = mazeController.automatedMovement;
        automatedRotation = mazeController.automatedRotation;
        moveSpeed = mazeController.moveSpeed;
        rotateSpeed = mazeController.rotateSpeed;
        constantSpeed = mazeController.constantSpeed;
        player.transform.position = waypoints[startingWaypoint].transform.position;
        player.transform.LookAt(waypoints[targetWaypoint].transform);
        controller = mazeController.controller;
        cube = mazeController.slideObject;
        //slideAdjust = new Vector3(.003f,-1.7f,1.51f);
        //slideAdjust = new Vector3(0f,2.63f,.478f);
        slideAdjust = new Vector3(0f, 0f,0f);
        elevator = mazeController.elevator;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * moveSpeed;     
        
        Vector3 moveDirection = waypoints[targetWaypoint].transform.position - player.transform.position;
        Vector3 movement = moveDirection.normalized * moveSpeed * Time.deltaTime;
        if(targetWaypoint == 61)
        {
            player.transform.parent = cube.transform;
            automatedMovement = false;
            automatedRotation = false;
            player.transform.localScale = new Vector3(1f, 1f,1f);
            player.transform.localPosition = slideAdjust;
        }
        else
        {
            cube.transform.DetachChildren();
            automatedMovement = true;
            automatedRotation = true;
        }
        // Automated Movement controls
        if(automatedMovement == true)
        {
            // Variable Speed
            if(constantSpeed == false)
            {
                //player.GetComponent<CharacterController>().Move(movement * timer);
                player.transform.position = Vector3.MoveTowards(player.transform.position,waypoints[targetWaypoint].transform.position,timer * moveSpeed/100);
            }
            // Constant Speed
            if(constantSpeed == true)
            {
                //player.GetComponent<CharacterController>().Move(movement);
                player.transform.position = Vector3.MoveTowards(player.transform.position,waypoints[targetWaypoint].transform.position,moveSpeed/100);
            }
        }

        if(Vector3.Distance(player.transform.position,waypoints[targetWaypoint].transform.position) < .1 && targetWaypoint < waypoints.Length - 1)
        {
            // Increment Targeted Waypoint for automated movement 
            targetWaypoint++;
            mazeController.targetWaypoint=targetWaypoint;

            // Resets variable speed movement modifier
            timer = 0;
            
            // Identify Look Rotation if autorotation enabled
            lookRotation = Quaternion.LookRotation(waypoints[targetWaypoint].transform.position - player.transform.position );
        }

        if(automatedRotation == true)
        {
            // Rotates player if autorotation enabled
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation,lookRotation,Time.deltaTime * rotateSpeed);
        }

        if(mazeController.targetWaypoint == 66)
        {
            SceneManager.LoadScene("MiddleMenu");
        }
    }
}
