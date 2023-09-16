using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    bool onJerry = false;
    public bool OnJerry => onJerry;

    [SerializeField] float speed = 5f;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] SpriteRenderer rend;
    
    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // -1 to 1
        float vertical = Input.GetAxis("Vertical"); // -1 to 1
        rb.velocity = Vector2.ClampMagnitude(new Vector2(horizontal, vertical) * speed, speed); 
        
        // Rotate GameObject in direction of movement
        if (rb.velocity.magnitude > 0)
        {
            rend.transform.up = rb.velocity;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        onJerry = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        onJerry = false;
    }
}
