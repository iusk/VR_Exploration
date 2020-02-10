using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public Transform player;
    static Animator anim;
    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 30) {
            direction.y = 0;
            
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 5) {
                Walk();
            } else {
                Attack();
            }
        } else {
            Idle();
        }
    }

    private void Idle() {
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    }

    private void Walk() {
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isIdle", false);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { // don't move while attacking
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    private void Attack() {
        anim.SetBool("isAttacking", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
    }
}
