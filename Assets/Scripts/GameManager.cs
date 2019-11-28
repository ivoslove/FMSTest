using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool startGame;

    private bool isOver;

    private List<Character> allCharacters;

    private static GameManager instance;


    public Transform RedTransform;
    public Transform RedTransform2;
    public Transform BlueTransform1;
    public Transform BlueTransform2;

    public Text ResulText;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public List<Character> AllCharacters => allCharacters;
    void Start()
    {
        Init();
        StartCoroutine("DelayStartGame", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            for (int i = 0; i < allCharacters.Count; i++)
            {
                allCharacters[i].Update(allCharacters.Where(p => p != allCharacters[i]).ToList());

            }
        }

        var redTeam = allCharacters.Where(p => p.Tag == "Red" && p.IsAlive).ToList();
        var blueTeam = allCharacters.Where(p => p.Tag == "Blue" && p.IsAlive).ToList();
        if (blueTeam.Count == 0 && redTeam.Count > 0)
        {
            isOver = true;
            ResulText.color = Color.red;
            ResulText.text = "红方胜！";
            Debug.Log("红方胜");
        }
        else if (redTeam.Count == 0 && blueTeam.Count > 0)
        {
            isOver = true;
            ResulText.color = Color.blue;
            ResulText.text = "蓝方胜！";
            Debug.Log("蓝方胜");
        }
        else if (redTeam.Count == 0 && blueTeam.Count == 0)
        {
            Debug.Log("平局");
            ResulText.text = "平局！";
        }


    }


    void Init()
    {
        instance = this;
         allCharacters  = new List<Character>();
        SkeletonKing skeletonKing1 = NpcFactory.CreateNpc<SkeletonKing>();
        skeletonKing1.Tag = "Red";
        allCharacters.Add(skeletonKing1);

        skeletonKing1.TransformNode.position = RedTransform.position;
        skeletonKing1.TransformNode.forward = Vector3.back;

        SkeletonKing skeletonKing2 = NpcFactory.CreateNpc<SkeletonKing>();
        skeletonKing2.Tag = "Blue";
        allCharacters.Add(skeletonKing2);
        skeletonKing2.TransformNode.position = BlueTransform1.position;
        skeletonKing2.TransformNode.forward = Vector3.forward;

        ZolrikMercenary zolrikMercenary = NpcFactory.CreateNpc<ZolrikMercenary>();
        zolrikMercenary.Tag = "Blue";
        allCharacters.Add(zolrikMercenary);
        zolrikMercenary.TransformNode.position = BlueTransform2.position;
        zolrikMercenary.TransformNode.forward = Vector3.forward;


        ZolrikMercenary zolrikMercenary2 = NpcFactory.CreateNpc<ZolrikMercenary>();
        zolrikMercenary2.Tag = "Red";
        allCharacters.Add(zolrikMercenary2);
        zolrikMercenary2.TransformNode.position = RedTransform2.position;
        zolrikMercenary2.TransformNode.forward = Vector3.forward;

        SkeletonKing skeletonKing3 = NpcFactory.CreateNpc<SkeletonKing>();
        skeletonKing3.Tag = "Blue";

        allCharacters.Add(skeletonKing3);
    }

    IEnumerator DelayStartGame(float second)
    {
        yield return new WaitForSeconds(second);
        startGame = true;
    }
}


public static class GloableValue
{
    public static float RotateLerpSpeed = 15f;
}