using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentScreenManager : MonoBehaviour
{
    public Text trialNumber;
    public Text target;
    public Text shouldClick;
    public Text particpantID;
    public NBackManager nBackman;

    // Start is called before the first frame update
    void Start()
    {
        particpantID.text = "Participant ID: " + GameSettings.participantID;
    }

    // Update is called once per frame
    void Update()
    {
       target.text = "Target: " + nBackman.currentObject.name;
       trialNumber.text = "Trial Number: " + GameSettings.trialNumber.ToString();
       shouldClick.text = "Is N-Back?: " + nBackman.shouldClick.ToString();
    }

}
