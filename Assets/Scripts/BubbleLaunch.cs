using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleLaunch : MonoBehaviour
{
    public float speed = 10f;
    public Animator animator;
    public SharkMovement shark;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                shark = collision.gameObject.GetComponent<SharkMovement>();
                shark.isBubbled = true;
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(Pop());
            }
        }
    }
    IEnumerator Pop()
    {
        animator.SetBool("isPopped", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
