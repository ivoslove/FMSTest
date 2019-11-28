
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TransferMatrix : MonoBehaviour
{

    [Header("起始位置")]
    public Vector3 StartPos;
    [Header("结束位置")]
    public Vector3 EndPos;
    [Header("起始方向")]
    public Vector3 StartVec;
    [Header("结束方向")]
    public Vector3 EndVec;
    [Header("缩放比例")]
    public Vector3 Scale;
    // Use this for initialization
    void Start()
    {

        //平移矩阵
        Vector4[] moveTransfer = {
            new Vector4(1,0,0,EndPos.x - StartPos.x),
            new Vector4(0,1,0,EndPos.y - StartPos.y),
            new Vector4(0,0,1,EndPos.z - StartPos.z),
            new Vector4(0,0,0,1)
        };
        Matrix4x4 moveTransferMatrix = new Matrix4x4();
        for (int i = 0; i < moveTransfer.Length; i++)
        {
            moveTransferMatrix.SetRow(i, moveTransfer[i]);
        }


        //计算两个向量间的欧拉角
        Vector3 eular = Quaternion.FromToRotation(StartVec, EndVec).eulerAngles;

        Debug.LogFormat("Eular0:{0}", eular);

        //eular.x = Vector3.Angle(Vector3.ProjectOnPlane(StartVec, Vector3.right), Vector3.ProjectOnPlane(EndVec, Vector3.right));
        //eular.y = Vector3.Angle(Vector3.ProjectOnPlane(StartVec, Vector3.up), Vector3.ProjectOnPlane(EndVec, Vector3.up));
        //eular.z = Vector3.Angle(Vector3.ProjectOnPlane(StartVec, Vector3.forward), Vector3.ProjectOnPlane(EndVec, Vector3.forward));

        //Debug.LogFormat("Eular0:{0}", eular);

        //x轴旋转矩阵
        float angleX = eular.x/* == 90f ? 0 : eular.x*/;
        float cosX = (float)Math.Cos((float)Math.PI / 180 * angleX);
        float sinX = (float)Math.Sin((float)Math.PI / 180 * angleX);
        Vector4[] rotateTransferX = {
            new Vector4(1,  0,      0,      0),
            new Vector4(0,  cosX,   -sinX,   0),
            new Vector4(0,  sinX,   cosX,  0),
            new Vector4(0,  0,      0,      1)
        };
        Matrix4x4 rotateTransferXMatrix = new Matrix4x4();
        for (int i = 0; i < rotateTransferX.Length; i++)
        {
            rotateTransferXMatrix.SetRow(i, rotateTransferX[i]);
        }


        //y轴旋转矩阵
        float angleY = eular.y/* == 90f ? 0 : eular.y*/;

        float cosY = (float)Math.Cos((float)Math.PI / 180 * angleY);
        float sinY = (float)Math.Sin((float)Math.PI / 180 * angleY);
        Vector4[] rotateTransferY = {
            new Vector4(cosY,  0,   sinY,   0),
            new Vector4(0,     1,   0,      0),
            new Vector4(-sinY, 0,   cosY,   0),
            new Vector4(0,     0,   0,      1)
        };
        Matrix4x4 rotateTransferYMatrix = new Matrix4x4();
        for (int i = 0; i < rotateTransferY.Length; i++)
        {
            rotateTransferYMatrix.SetRow(i, rotateTransferY[i]);
        }

        //Z轴旋转矩阵
        float angleZ = eular.z/* == 90f ? 0 : eular.z*/;

        float cosZ = (float)Math.Cos((float)Math.PI / 180 * angleZ);
        float sinZ = (float)Math.Sin((float)Math.PI / 180 * angleZ);
        Vector4[] rotateTransferZ = {
            new Vector4(cosZ,   -sinZ,  0,      0),
            new Vector4(sinZ,   cosZ,   0,      0),
            new Vector4(0,      0,      1,      0),
            new Vector4(0,      0,      0,      1)
        };
        Matrix4x4 rotateTransferZMatrix = new Matrix4x4();
        for (int i = 0; i < rotateTransferZ.Length; i++)
        {
            rotateTransferZMatrix.SetRow(i, rotateTransferZ[i]);
        }

        //缩放矩阵
        Vector4[] scaleransfer = {
            new Vector4(Scale.x,      0,          0,          0),
            new Vector4(0,            Scale.y,    0,          0),
            new Vector4(0,            0,          Scale.z,    0),
            new Vector4(0,            0,          0,          1)
        };
        Matrix4x4 scaleTransferMatrix = new Matrix4x4();
        for (int i = 0; i < scaleransfer.Length; i++)
        {
            scaleTransferMatrix.SetRow(i, scaleransfer[i]);
        }


        //Unity中的旋转矩阵：
        //Unity中按照  Z、X、Y轴的顺序进行旋转，下式乘法顺序不可变
        Matrix4x4 rotateMatrix = rotateTransferYMatrix * rotateTransferXMatrix * rotateTransferZMatrix;

        //最终变换矩阵
        Matrix4x4 transferMatrix = moveTransferMatrix * rotateMatrix * scaleTransferMatrix;


        Matrix4x4 matScale = new Matrix4x4();

        matScale.SetTRS(Vector3.zero, Quaternion.Euler(Vector3.zero), new Vector3(Scale.x, Scale.y, Scale.z));
        Debug.Log("---------------------------\n");
        Debug.Log("测试缩放：\n" + scaleTransferMatrix.ToString("0.00"));
        Debug.Log("参照：\n" + matScale.ToString("0.00"));

        Matrix4x4 matMove = new Matrix4x4();
        matMove.SetTRS(EndPos - StartPos, Quaternion.Euler(Vector3.zero), Vector3.one);
        Debug.Log("---------------------------\n");
        Debug.Log("测试平移：\n" + moveTransferMatrix.ToString("0.00"));
        Debug.Log("参照：\n" + matMove.ToString("0.00"));

        Matrix4x4 matRoX = new Matrix4x4();
        matRoX.SetTRS(Vector3.zero, Quaternion.AngleAxis(angleX, Vector3.right), Vector3.one);
        Debug.Log("---------------------------\n");
        Debug.Log("测试旋转矩阵X：\n" + rotateTransferXMatrix.ToString("0.00"));
        Debug.Log("参照：\n" + matRoX.ToString("0.00"));

        Matrix4x4 matRoY = new Matrix4x4();
        matRoY.SetTRS(Vector3.zero, Quaternion.AngleAxis(angleY, Vector3.up), Vector3.one);
        Debug.Log("---------------------------\n");
        Debug.Log("测试旋转矩阵Y：\n" + rotateTransferYMatrix.ToString("0.00"));
        Debug.Log("参照：\n" + matRoY.ToString("0.00"));

        Matrix4x4 matRoZ = new Matrix4x4();
        matRoZ.SetTRS(Vector3.zero, Quaternion.AngleAxis(angleZ, Vector3.forward), Vector3.one);
        Debug.Log("---------------------------\n");
        Debug.Log("测试旋转矩阵Z：\n" + rotateTransferZMatrix.ToString("0.00"));
        Debug.Log("参照：\n" + matRoZ.ToString("0.00"));

        Debug.Log("---------------------------\n");

        //利用U3D内置方法算出由向量v1到向量v2的变换矩阵，用来参照
        Matrix4x4 referenceMat = new Matrix4x4();
        referenceMat.SetTRS(EndPos - StartPos, Quaternion.Euler(eular), new Vector3(Scale.x, Scale.y, Scale.z));

        Debug.Log("变换矩阵 : \n" + transferMatrix.ToString("0.00"));
        Debug.Log("参照矩阵 : \n" + referenceMat.ToString("0.00"));


        List<Vector3> meshList = new List<Vector3>();
        Mesh n_mesh = GetComponent<MeshFilter>().mesh;

        var trans = GameObject.Find("Capsule (END)").transform;
        for (int i = 0; i < n_mesh.vertexCount; i++)
        {
            //meshList.Add(transferMatrix.MultiplyPoint(n_mesh.vertices[i]));
            meshList.Add(trans.localToWorldMatrix*( n_mesh.vertices[i])+new Vector4((trans.position - transform.position).x, (trans.position - transform.position).y, (trans.position - transform.position).z, 0));
        }
        n_mesh.SetVertices(meshList);
    }
}
