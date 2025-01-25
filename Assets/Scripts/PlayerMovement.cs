using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Rigidbody2D rbody;
    public Collider2D collider;
    public Collider2D ground;
    public Transform respawnPoint; 
    public float speed;
    public float jumpForce;
    float jumpPower;
    public float movementLR;
    public BubbleLaunch bubble;
    public Transform shotPosition;
    public SpriteRenderer sprite;
    public bool isRight = false;
    public bool jumpPressed;
    public bool isGrounded;
    public bool isJumping;
    public bool isMoving;
    public bool isShooting;
    public Animator animator;
    public int score = 0;
    public TextMeshProUGUI scoreText;

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
        if (Input.GetButtonDown("Jump") && (collider.bounds.Intersects(ground.bounds) && Input.GetAxisRaw("Vertical") >= 0))
        {
            jumpPressed = true;
        }      

        //firing bubbles
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Fire());
        }

        if (Input.inputString != "")
        {
            Debug.Log(Input.inputString);
        }
    }

    void FixedUpdate()
    {
        if(movementLR > 0 && !sprite.flipY)
        {
            //shotPosition.transform.rotation = 180f;
            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z + 180);
            gameObject.transform.rotation = Quaternion.Euler(rot);
            sprite.flipY = true;
        }
        else if (movementLR < 0 && sprite.flipY)
        {
            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z + 180);
            gameObject.transform.rotation = Quaternion.Euler(rot);
            sprite.flipY = false;
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

        if(movementLR != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        rbody.velocity = new Vector2(movementLR * speed, jumpPower);

        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isShooting", isShooting);
        animator.SetFloat("vertDirection", rbody.velocity.y);
    }

    public void runDeath()
    {
        StartCoroutine(Death());
    }

    IEnumerator Fire()
    {
        isShooting = true;
        Instantiate(bubble, shotPosition.position, shotPosition.rotation);
        yield return new WaitForSeconds(2f);
        isShooting = false;
    }

    IEnumerator Death()
    {
        renderer.enabled = false;
        gameObject.transform.position = respawnPoint.position;
        yield return new WaitForSeconds(2f);
        renderer.enabled = true;
    }

    public void addScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}
