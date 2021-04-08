using UnityEngine;
using System.Text;
using System;


public class MotionSicknessOutput : MonoBehaviour {

    
    private StringBuilder csv = new StringBuilder();
    
    //variables for translational for actual head movement
    //private Vector3 actualP1;
    //private Vector3? actualV1 = null;
    //private Vector3 actualA1;

    //variables for angular for actual head movement
    //private Vector3 actualAP1;
    //private Vector3 actualAV1;
    //private Vector3 actualAA1;

    //variables for translational for virtual movement
    private Vector3 virtualP1;
    private Vector3? virtualV1 = null;
    private Vector3 virtualA1;

    //variables for angular for virtual movement
    private Vector3 virtualAP1;
    private Vector3 virtualAV1;
    private Vector3 virtualAA1;

    private Vector3 outputTranslational;
    private Vector3 outputRotational;

    //private GameObject actualHead;

    private DateTime startTime;


    // Use this for initialization
    void Start ()
    {
        
        //actualHead = GameObject.Find("Camera (eye)");
        startTime = DateTime.Now;

        System.IO.File.WriteAllText("opticFlow_" + startTime.ToString("yyyyMMddTHHmmssZ") + ".csv", "Time,px,py,pz,p_mag,rx,ry,rz,r_mag" + "\n");

        virtualP1 = this.gameObject.transform.position;
        //actualP1 = actualHead.transform.position;

        //actualAP1 = actualHead.transform.eulerAngles;
        virtualAP1 = this.transform.eulerAngles;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(virtualV1 == null)
        {
            //actualV1 = actualHead.transform.position - actualP1;
            virtualV1 = (this.gameObject.transform.position - virtualP1);

            //actualAV1 = actualHead.transform.eulerAngles - actualAP1;
            virtualAV1 = (this.gameObject.transform.eulerAngles - virtualAP1);
            return;
        }

        //actualA1 = actualHead.transform.position - actualP1 - (Vector3)actualV1;
        virtualA1 = (this.gameObject.transform.position - virtualP1 - (Vector3)virtualV1);

        //actualAA1 = actualHead.transform.eulerAngles - actualAP1 - actualAV1;
        virtualAA1 = (this.gameObject.transform.eulerAngles - virtualAP1 - virtualAV1);


        outputTranslational = virtualA1;
        outputRotational = virtualAA1;

        System.IO.File.AppendAllText("opticFlow_" + startTime.ToString("yyyyMMddTHHmmssZ") + ".csv",DateTime.Now.Subtract(startTime).TotalSeconds.ToString()+ "," + outputTranslational.x.ToString() + "," + outputTranslational.y.ToString() + "," + outputTranslational.z.ToString() + "," + outputTranslational.magnitude.ToString() + "," + outputRotational.x.ToString() + "," + outputRotational.y.ToString() + "," + outputRotational.z.ToString() + "," + outputRotational.magnitude.ToString() + "\n");

        virtualV1 = (this.gameObject.transform.position - virtualP1);
        virtualAV1 = (this.gameObject.transform.eulerAngles - virtualAP1);
        virtualP1 = this.gameObject.transform.position;
        virtualAP1 = this.gameObject.transform.eulerAngles;

        //Console.WriteLine(outputTranslational.ToString());
	}
}
