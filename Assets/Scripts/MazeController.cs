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

public class MazeController : MonoBehaviour
{
    public GameObject topHalf;
    public GameObject bottomHalf;
    public GameObject bottomRamp;
    public GameObject topRamp;
    public GameObject bottomFirstHalf;
    public GameObject bottomSeccondHalf;
    public bool automatedMovement = true;
    public bool constantSpeed;
    public bool automatedRotation = true;
    public GameObject playerRig;
    public GameObject playerCam;
    public GameObject slideObject;
    public GameObject elevator;
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpSpeed;
    public float gravity;
    public int targetWaypoint;
    public CharacterController controller;
    public MovePlayer movePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetWaypoint <= 29)
        {
            topHalf.SetActive(true);
            bottomHalf.SetActive(false);
            topRamp.SetActive(false);
            bottomRamp.SetActive(false);

        }
        else
        {
            topHalf.SetActive(false);
            bottomHalf.SetActive(true);
            topRamp.SetActive(true);
            bottomRamp.SetActive(true);

        }
        if(targetWaypoint > 40)
        {
            bottomFirstHalf.SetActive(false);
            bottomSeccondHalf.SetActive(true);
        }
        else
        {
            bottomSeccondHalf.SetActive(false);
        }
    }
}
