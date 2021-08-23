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
using UnityEngine.UI;
public class StartMenuManager : MonoBehaviour
{
    // public GameSettings gameSettings;
    // public GameObject startCanvas;
    // public GameObject restartCanvas;

    public Text participantIDinput;
    public Dropdown condition;
    public GameObject startButton;
    public Component nBackManager;
    //public GameSettings GameSettings;
    public GameObject startCanvas;
    public GameObject inGameCanvas;
    public Text trialCount;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(participantIDinput != null && condition.value != 0)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
        trialCount.text = "Trial Number: " + GameSettings.trialNumber.ToString();
    }

    public void StartMaze3D()
    {
        SceneManager.LoadScene("CornMaze 3D");
    }
    public void StartMaze2D()
    {
        SceneManager.LoadScene("CornMaze 2D");
    }
    public void LoadMiddleMenu()
    {
        SceneManager.LoadScene("MiddleMenu");
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void StartTraining()
    {
        nBackManager.GetComponent<NBackManager>().enabled = true;
        GameSettings.participantID = participantIDinput.text;
        if(condition.value == 1)
        {
            GameSettings.condition = "1-Back";
        }
        else if(condition.value == 2)
        {
            GameSettings.condition = "2-Back";
        }
        GameSettings.nBackVal = condition.value;
        startCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
    }

    public void QuitApp()
    {
        if(Application.isEditor)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
    public void test() 
    {
        {
            print("test");
        }
    }
}
