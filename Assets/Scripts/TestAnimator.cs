using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetInteger("Idle",1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetInteger("Idle", 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetInteger("Idle", 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animator.SetInteger("Idle", 4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            animator.SetInteger("Idle", 5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            animator.SetInteger("Idle", 6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            animator.SetInteger("Idle", 7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            animator.SetInteger("Idle", 8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            animator.SetInteger("Idle", 9);
        }
    }
}
