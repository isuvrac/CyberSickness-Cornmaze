using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NBackManager : MonoBehaviour
{
    private int numTrials;
    public int currentTrial = 0;
    [SerializeField]
    private GameObject[] objects;
    [SerializeField]
    private Transform[] spawnLocations;
    private float timeBetweenObjects = 3f;
    private GameObject objectToLoad;
    public GameObject currentObject = null;
    public string[] nBackOrder;
    [SerializeField]
    private AudioSource playerAudioSource;
    [SerializeField]
    private AudioClip goodSound;
    [SerializeField]
    private AudioClip badSound;
    private DateTime trialStartTime;
    private DateTime trialResponseTime;
    public bool shouldClick = false;
    private bool objectClicked = false;
    private bool responseCorrect;
    public static int nBackVal;
    public GameObject rController;
    public GameObject lController;
    // Start is called before the first frame update
    void Start()
    {
        numTrials = spawnLocations.Length;
        nBackOrder = new string[numTrials];
        nBackVal = GameSettings.nBackVal;
        if(nBackVal == 0)
        {
            shouldClick = true;
        }
        if(nBackVal == 2)
        {
            shouldClick = false;
        }
        if(GameSettings.isTraining == false)
        {
            System.IO.File.WriteAllText("Data/TaskTracker_ID_" + GameSettings.participantID + ".csv", "ID,Condition,Trial Number,Object,ShouldClick,Clicked,Response Time,Correct" + "\n");
        }
        nBackVal = GameSettings.nBackVal;

        if(nBackVal != 0)
        {
            if(GameSettings.handedness == "Right")
            {
                rController.SetActive(true);
            }
            else
            {
                lController.SetActive(true);
            }
        }
        AdvanceNBack();
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenObjects -= Time.deltaTime;
        if(timeBetweenObjects < 0 && currentTrial < numTrials)
        {
            AdvanceNBack();
            timeBetweenObjects = 3f;
        }
    }

    public int ObjectNumberGenerator()
    { 
        int ret = 0;

        ret = UnityEngine.Random.Range(0,objects.Length);
        
        return ret;
    }

    public void AdvanceNBack()
    {
        objectToLoad = objects[ObjectNumberGenerator()];
        if(currentObject != null)
        {
            currentObject.SetActive(false);
            nBackOrder.SetValue(currentObject.name,currentTrial-1);
            TaskTracker();
            objectClicked = false;
        }
        if(currentTrial <= numTrials)
        {
            currentObject = Instantiate(objectToLoad,spawnLocations[currentTrial],false);    
        }
        
        if(nBackVal == 0)
        {
            shouldClick = false;
        }
        else if(nBackVal == 1)
        {
            if(currentObject.name == "NBackCow(Clone)")
            {
                shouldClick = true;
            }
            else
            {
                shouldClick = false;
            }
        }
        else if(currentTrial >= nBackVal)
        {
            if(currentObject.name == nBackOrder[currentTrial - nBackVal])
            {   
                // The object should be clicked
                shouldClick = true;
            }
            else
            {
                // The object should NOT be clicked
                shouldClick = false;
            }
        }

        currentTrial++;
        GameSettings.trialNumber = currentTrial;
        trialStartTime = DateTime.Now;
    }

    public void PointerClicked() 
    {
        currentObject.SetActive(false);
        objectClicked = true;
        trialResponseTime = DateTime.Now;

        if(shouldClick)
        {
            //Successfully clicked the right object, play good sound
            playerAudioSource.clip = goodSound;
            playerAudioSource.Play();
            print("good job");
        }
        else
        {
            //Clicked the wrong object, play bad sound
            playerAudioSource.clip = badSound;
            playerAudioSource.Play();
            print("bad job");
        }
        
        //timeBetweenObjects = 3f;
        //AdvanceNBack();
        print("DESTROYED");
    }

    public void TaskTracker()
    {
        if(shouldClick == objectClicked)
        {
            responseCorrect = true;
        }
        else
        {
            responseCorrect = false;
        }
        if(objectClicked)
        {
            System.IO.File.AppendAllText("Data/TaskTracker_ID_" + GameSettings.participantID + ".csv", GameSettings.participantID + 
             "," + GameSettings.condition + "," + currentTrial.ToString() + "," + currentObject.name.ToString() +
             "," + shouldClick.ToString() + "," + objectClicked.ToString() + 
             "," + (-trialStartTime.Subtract(trialResponseTime).TotalSeconds).ToString() + 
             "," + responseCorrect.ToString() + "\n");
        }
        else
        {
            System.IO.File.AppendAllText("Data/TaskTracker_ID_" + GameSettings.participantID + ".csv", GameSettings.participantID + 
             "," + GameSettings.condition + "," + currentTrial.ToString() +
             "," + currentObject.name.ToString() + "," + shouldClick.ToString() + 
             "," + objectClicked.ToString() + 
             "," + "0," + responseCorrect.ToString() + "\n");
        }
        
    }

}
