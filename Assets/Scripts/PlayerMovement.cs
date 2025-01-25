using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rbody;
    public Collider2D collider;
    public Collider2D ground;
    public Collider2D platform;
    public float speed;
    public float jumpForce;
    float jumpPower;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        //collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementLR = Input.GetAxis ("Horizontal");
        if (Input.GetButtonDown("Jump") && (collider.bounds.Intersects(ground.bounds) || collider.bounds.Intersects(platform.bounds)))
        {
            jumpPower = jumpForce;
        }
        else
        {
            if (jumpPower > 0)
            {
                jumpPower -= 1f;
            }
        }

        rbody.velocity = new Vector2 (movementLR * speed, jumpPower);
    }
}
