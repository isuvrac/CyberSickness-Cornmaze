using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{

    #region Public Variables
    public GameObject Camera;
    #endregion

    #region Private Variables
    private const float _distance = 2.0f;
    private GameObject FollowCanvas;
    #endregion

    #region Public Methods
    public void HardHeadLock(GameObject obj)
    {
        var lookPos = Camera.transform.position + Camera.transform.forward * _distance;
        lookPos.y = 0;
        obj.transform.position = lookPos;

        float yRotation = Camera.transform.eulerAngles.y;
        obj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

    }
    public void HeadLock(GameObject obj, float speed)
    {
        speed = Time.deltaTime * speed;
        Vector3 posTo = Camera.transform.position + (Camera.transform.forward * _distance);
        obj.transform.position = Vector3.SlerpUnclamped(obj.transform.position, posTo, speed);
        Quaternion rotTo = Quaternion.LookRotation(obj.transform.position - Camera.transform.position);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotTo, speed);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        FollowCanvas = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        HardHeadLock(FollowCanvas);
    }
}
