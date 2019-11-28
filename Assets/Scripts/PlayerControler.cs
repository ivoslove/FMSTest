using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private SkeletonKing player;
    private float rotateSpeed=10f;
    private Vector3 rotate;
    private Vector3 targetPoint;
    private int index = 1;
    void Start()
    {
        player=new SkeletonKing();
        rotate = player.TransformNode.forward;
        targetPoint = player.TransformNode.position;

        RuntimeAnimatorController controller = player.Animator.runtimeAnimatorController;
        AnimationClip walkClip = controller.animationClips.FirstOrDefault(
            p => p.name.Equals("Walk"));
        walkClip.AddEvent(new AnimationEvent()
        {

            functionName = "FinishedOnceWalk",
            time = walkClip.length,

        });
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(/*"当前normalizedTime：" +*/ player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, 1<<8 ))
            {
                Vector3 point = hit.point;
                Vector3 targetVec = point - player.TransformNode.position;
                targetVec.y = 0;
                rotate=targetVec.normalized;
                targetPoint=new Vector3(point.x, player.TransformNode.position.y, point.z);
           
                player.Animator.SetInteger("Idle", 5);
            }
        }

        if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") &&
            player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {

            player.Animator.SetInteger("Idle", 5);
            Debug.Log(/*"当前normalizedTime：" +*/ player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        if (Vector3.Distance(player.TransformNode.position,targetPoint)<=0.01f)
        {
            targetPoint = player.TransformNode.position;
            rotate = player.TransformNode.forward;

            player.Animator.SetInteger("Idle",1);


        }
        else
        {
            player.TransformNode.forward=Vector3.Lerp(player.TransformNode.forward, rotate, Time.deltaTime * rotateSpeed);
            //player.TransformNode.rotation = Quaternion.Lerp(player.TransformNode.rotation, qua, Time.deltaTime * rotateSpeed);
            player.TransformNode.position += (targetPoint - player.TransformNode.position).normalized * player.MoveSpeed * Time.deltaTime;
        }
    }


}
