using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    bool onJerry = false;
    public bool OnJerry => onJerry;

    [SerializeField] float speed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] List<Animator> anim;

    [SerializeField] SpriteRenderer rend;
    [SerializeField] GameObject duplicateLeft;
    [SerializeField] GameObject duplicateRight;
    [SerializeField] GameObject duplicateTop;
    [SerializeField] GameObject duplicateBottom;
    [SerializeField] GameObject duplicateTopLeft;
    [SerializeField] GameObject duplicateTopRight;
    [SerializeField] GameObject duplicateBottomLeft;
    [SerializeField] GameObject duplicateBottomRight;



    public Transform topLeftAnchor;
    public Transform bottomRightAnchor;

    private float _screenWidth;
    private float _screenHeight;
    private float _leftXExtent;
    private float _rightXExtent;
    private float _topYExtent;
    private float _bottomYExtent;

    // Start is called before the first frame update
    void Start()
    {
        var topLeftPos = topLeftAnchor.position;
        var topRightPos = bottomRightAnchor.position;

        _leftXExtent = topLeftPos.x;
        _rightXExtent = topRightPos.x;
        _topYExtent = topLeftPos.y;
        _bottomYExtent = topRightPos.y;

        _screenWidth = _rightXExtent - _leftXExtent;
        _screenHeight = _topYExtent - _bottomYExtent;

        duplicateBottom.transform.localPosition = new Vector3(0f, _bottomYExtent - _topYExtent, 0f);
        duplicateTop.transform.localPosition = new Vector3(0f, _topYExtent - _bottomYExtent, 0f);
        duplicateLeft.transform.localPosition = new Vector3(_leftXExtent - _rightXExtent, 0f, 0f);
        duplicateRight.transform.localPosition = new Vector3(_rightXExtent - _leftXExtent, 0f, 0f);
        duplicateBottomLeft.transform.localPosition = new Vector3(_leftXExtent - _rightXExtent, _bottomYExtent - _topYExtent, 0f);
        duplicateBottomRight.transform.localPosition = new Vector3(_rightXExtent - _leftXExtent, _bottomYExtent - _topYExtent, 0f);
        duplicateTopLeft.transform.localPosition = new Vector3(_leftXExtent - _rightXExtent, _topYExtent - _bottomYExtent, 0f);
        duplicateTopRight.transform.localPosition = new Vector3(_rightXExtent - _leftXExtent, _topYExtent - _bottomYExtent, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // -1 to 1
        float vertical = Input.GetAxis("Vertical"); // -1 to 1
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isSprinting)
        {
            rb.velocity = Vector2.ClampMagnitude(new Vector2(horizontal, vertical) * sprintSpeed, sprintSpeed);
        }
        else
        {
            rb.velocity = Vector2.ClampMagnitude(new Vector2(horizontal, vertical) * speed, speed);
        }

        // // Rotate GameObject in direction of movement
        // if (rb.velocity.magnitude > 0)
        // {
        //     rend.transform.up = rb.velocity;
        // }


        Vector2 normalizedPos = new Vector2(rb.transform.position.x, rb.transform.position.y) - new Vector2(_leftXExtent, _bottomYExtent);

        normalizedPos.x = Mathf.Repeat(normalizedPos.x, _screenWidth);
        normalizedPos.y = Mathf.Repeat(normalizedPos.y, _screenHeight);

        var newPos = normalizedPos + new Vector2(_leftXExtent, _bottomYExtent);

        rb.transform.position = new Vector3(newPos.x, newPos.y, 0f);

        // // set duplicate rotation the same as original
        // duplicateBottom.transform.rotation = rend.transform.rotation;
        // duplicateTop.transform.rotation = rend.transform.rotation;
        // duplicateLeft.transform.rotation = rend.transform.rotation;
        // duplicateRight.transform.rotation = rend.transform.rotation;
        // duplicateBottomLeft.transform.rotation = rend.transform.rotation;
        // duplicateBottomRight.transform.rotation = rend.transform.rotation;
        // duplicateTopLeft.transform.rotation = rend.transform.rotation;
        // duplicateTopRight.transform.rotation = rend.transform.rotation;

        UpdateAnimation();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger");
        onJerry = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        onJerry = false;
    }

    void UpdateAnimation()
    {
        // Active Side, Back, or Front animation trigger based on direction of movement
        if (rb.velocity.magnitude > 0)
        {
            foreach (var a in anim)
            {
                // Check if vertical velocity is larger or horizontal 
                if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
                {
                    // Check if vertical velocity is positive or negative
                    if (rb.velocity.y > 0)
                    {
                        a.Play("Back");
                    }
                    else
                    {
                        a.Play("Front");
                    }
                }
                else
                {
                    a.Play("Side");
                    // flip the sprite if moving left
                    if (rb.velocity.x > 0)
                    {
                        rend.flipX = true;
                    }
                    else
                    {
                        rend.flipX = false;
                    }
                }
                a.speed = rb.velocity.magnitude / 3;
            }
        }
    }
}
