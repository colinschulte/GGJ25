using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float swimSpeed;
    public float floatSpeed;
    public bool isDanger = true;
    public bool isBubbled = false;
    public bool isEating = false;
    public Animator animator;
    public Animator bubbleAnimator;
    public Vector3 goalTransform;
    public SpriteRenderer sprite;
    public SpriteRenderer bubbleSprite;

    public AudioSource Chomp;
    public AudioSource PopA;


    // Start is called before the first frame update
    void Start()
    {
        goalTransform = new Vector3(14, gameObject.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isBubbled)
        {
            bubbleSprite.enabled = true;
            isDanger = false;
            rbody.velocity = new Vector2(0, floatSpeed);
        }
        animator.SetBool("isBubbled", isBubbled);
        animator.SetBool("isEating", isEating);
    }

    private void FixedUpdate()
    {
        if (isDanger)
        {
            if (gameObject.transform.position != goalTransform)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goalTransform, swimSpeed);
            }
            else
            {
                goalTransform = new Vector3(0 - goalTransform.x, gameObject.transform.position.y, 0);
                sprite.flipX = !sprite.flipX;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDanger && collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            Chomp.Play();
            player.runDeath(gameObject.tag);
            isEating = true;
            isDanger = false;
            StartCoroutine(Chewing());
        }

        if (collision.gameObject.CompareTag("Surface"))
        {
            sprite.enabled = false;
            PopA.Play();
            bubbleAnimator.SetBool("isPopped", true);
        }
    }
    IEnumerator Chewing()
    {
        yield return new WaitForSeconds(1.02f);
        isEating = false;
        isDanger = true;
    }
}
