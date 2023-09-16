using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class JerryAnimController : MonoBehaviour
{
    private float speed;

    private Vector3 velocity;
    private Vector3 lastPos;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Use transform to calculate velocity
        velocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        // Active Side, Back, or Front animation trigger based on direction of movement
        if (velocity.x > 0)
        {
            // Check if vertical velocity is larger or horizontal 
            if (Mathf.Abs(velocity.y) > Mathf.Abs(velocity.x))
            {
                // Check if vertical velocity is positive or negative
                if (velocity.y > 0)
                {
                    animator.Play("Back");
                }
                else
                {
                    animator.Play("Front");
                }
            }
            else
            {
                animator.Play("Side");
                // flip the sprite if moving left
                if (velocity.x > 0)
                {
                    rend.flipX = true;
                }
                else
                {
                    rend.flipX = false;
                }
            }
            // find the magnitude of the velocity and set the animation speed to that
            animator.speed = velocity.magnitude / 20;
        }
    }
}

