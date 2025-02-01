using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 Move;
    public bool Jump;
    public bool Attack;
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
    public TextMeshProUGUI livesText;
    public int lives = 3;
    public Image gameover;
    public GameObject jewels;
    public UnityEngine.UI.Button backButton;

    public AudioSource JumpB;
    public AudioSource Lose;
    public AudioSource Shoot;
    public AudioSource Win;
    public AudioSource Zap;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        //collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementLR = Move.x;
        if (Jump && collider.bounds.Intersects(ground.bounds))
        {
            if (Move.y >= 0)
            {
                jumpPressed = true;
            }
            else
            {
                //Jump = false;
            }
        }

        //firing bubbles
        if (Attack)
        {
            StartCoroutine(Fire());
            Attack = false;
        }

        if (jewels.transform.childCount <= 0)
        {
            StartCoroutine(NewLevel());
        }
    }

    void FixedUpdate()
    {
        if (movementLR > 0 && !sprite.flipY)
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
            JumpB.Play();
            jumpPower = jumpForce;
            jumpPressed = false;
            Jump = false;
        }
        else
        {
            if (jumpPower > 0)
            {
                jumpPower -= 1f;
            }
        }

        if (movementLR != 0)
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

        Jump = false;
    }

    public void runDeath(string tag)
    {
        StartCoroutine(Death(tag));
    }

    IEnumerator Fire()
    {
        isShooting = true;
        Shoot.Play();
        Instantiate(bubble, shotPosition.position, shotPosition.rotation);
        yield return new WaitForSeconds(0.2f);
        isShooting = false;
    }

    IEnumerator Death(string tag)
    {
        if (tag == "Jelly")
        {
            animator.SetBool("isShocked", true);
            Zap.Play();
            yield return new WaitForSeconds(1f);
        }

        renderer.enabled = false;
        gameObject.transform.position = respawnPoint.position;
        animator.SetBool("isShocked", false);
        yield return new WaitForSeconds(2f);
        if (lives > 0)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            renderer.enabled = true;
        }
        else
        {
            gameover.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);
            Lose.Play();
            Time.timeScale = 0;
        }
    }

    public void addScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
        //if (score >= 1800)
        //{
        //    StartCoroutine(NewLevel());
        //}
    }

    IEnumerator NewLevel()
    {
        Win.Play();
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(currentLevel + 1);
    }

    public void Back()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void OnMove(InputValue value)
    {
        Move = value.Get<Vector2>();
    }

    private void OnJump()
    {
        Jump = true;
    }

    private void OnAttack()
    {
        Attack = true;
    }
}