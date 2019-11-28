using UnityEngine;
using System.Collections;

public class SetLookRotationTest : MonoBehaviour
{
    public Transform A, B, C, D;
    Quaternion q1 = Quaternion.identity;

    void Update()
    {
        //不可直接使用C.rotation.SetLookRotation(A.position,B.position);
        q1.SetLookRotation(A.position, B.position);
        C.rotation = q1;
        //分别绘制A、B和C.right的朝向线
        //请在Scene视图中查看
        Debug.DrawLine(Vector3.zero, A.position, Color.red);
        Debug.DrawLine(Vector3.zero, B.position, Color.green);
        Debug.DrawLine(C.position, C.TransformPoint(Vector3.right * 2.5f), Color.yellow);
        Debug.DrawLine(C.position, C.TransformPoint(Vector3.forward * 2.5f), Color.gray);
        //分别打印C.right与A、B的夹角
        Debug.Log("C.right与A的夹角:" + Vector3.Angle(C.right, A.position));
        Debug.Log("C.right与B的夹角:" + Vector3.Angle(C.right, B.position));
        //C.up与B的夹角
        Debug.Log("C.up与B的夹角:" + Vector3.Angle(C.up, B.position));


        D.rotation = Quaternion.LookRotation(A.position, B.position);

        Debug.DrawLine(D.position, D.TransformPoint(Vector3.up * 2.5f), Color.green);
        Debug.DrawLine(D.position, D.TransformPoint(Vector3.forward * 2.5f), Color.blue);


    }
}
