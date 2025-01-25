using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rbody;
    public Collider2D collider;
    public Collider2D ground;
    public Collider2D platform;
    public Collider2D platform2;
    public Collider2D platform3;
    public float speed;
    public float jumpForce;
    float jumpPower;
    public float movementLR;
    public BubbleLaunch bubble;
    public Transform shotPosition;
    public SpriteRenderer sprite;
    public bool jumpPressed;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        //collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementLR = Input.GetAxis ("Horizontal");
        if (Input.GetButtonDown("Jump") && (collider.bounds.Intersects(ground.bounds) || ((collider.bounds.Intersects(platform.bounds) || collider.bounds.Intersects(platform2.bounds) || collider.bounds.Intersects(platform3.bounds)) && Input.GetAxisRaw("Vertical") >= 0)))
        {
            jumpPressed = true;
        }
        else
        {
            if (jumpPower > 0)
            {
                //jumpPower -= 1f;
            }
        }

        

        //firing bubbles
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bubble, shotPosition.position, shotPosition.rotation);
        }

        if (Input.inputString != "")
        {
            Debug.Log(Input.inputString);
        }
    }

    void FixedUpdate()
    {
        if(movementLR > 0 && !sprite.flipX)
        {
            //shotPosition.transform.rotation = 180f;
            Vector3 rot = shotPosition.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            shotPosition.rotation = Quaternion.Euler(rot);
            sprite.flipX = true;
        }
        else if (movementLR < 0 && sprite.flipX)
        {
            Vector3 rot = shotPosition.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            shotPosition.rotation = Quaternion.Euler(rot);
            sprite.flipX = false;
        }

        if (jumpPressed)
        {
            jumpPower = jumpForce;
            jumpPressed = false;
        }
        else
        {
            if (jumpPower > 0)
            {
                jumpPower -= 1f;
            }
        }
        rbody.velocity = new Vector2(movementLR * speed, jumpPower);
    }
}
