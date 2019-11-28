using UnityEngine;
using System.Collections;



public class CameraControl : MonoBehaviour
{
    public Transform target;

    public float Speed = 200.0f;//速度


    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    public Quaternion rotation;
    private Vector3 position;

    public float X;//初始化角度X轴
    public float Y;//初始化角度Y轴
    /// <summary>
    /// 初始化距离
    /// </summary>
    public float CameDistance;

    private GameObject refObj;

    private Transform CameraTransform;
    void Start()
    {
       
        //refObj =new GameObject("Ref");
        //refObj.transform.SetParent(target);
        //refObj.transform.localEulerAngles=Vector3.zero;
        CameraTransform = Camera.main.transform;
        CameDistance = Vector3.Distance(CameraTransform.position, target.position);


        X = target.eulerAngles.y;
        Y = target.eulerAngles.x;
    }
    void OnEnable()
    {


    }



    void LateUpdate()
    {

        if (Input.GetMouseButton(1))
        {
            float offsetX = Input.GetAxis("Mouse X")*Speed;
             float offsetY = Input.GetAxis("Mouse Y") * Speed;
  
             Quaternion quaternion=Quaternion.Euler(offsetY,offsetX,0);

             Vector3 dir = (Camera.main.transform.position - target.position).normalized;
             Vector3 newDir = quaternion * dir;
             Vector3 currentPos = target.position + newDir * CameDistance;
            currentPos=new Vector3(currentPos.x,Mathf.Clamp(currentPos.y, target.position.y, target.position.y + Mathf.Cos(60 / 180f * Mathf.PI) * CameDistance),currentPos.z);

            CameraTransform.position = currentPos;
            CameraTransform.forward= target.position - CameraTransform.position;

       
        }

     





    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}