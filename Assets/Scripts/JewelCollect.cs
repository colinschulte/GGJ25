using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelCollect : MonoBehaviour
{
    public Animator animator;
    public int value;
    public PlayerMovement player;
    public Collider2D collider;
    public AudioSource gemGet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("connect");
        if (collision.gameObject.CompareTag("Player"))
        {
            //player = collision.gameObject.GetComponent<PlayerMovement>();
            StartCoroutine(Collect());
            player.addScore(value);
        }
    }
    IEnumerator Collect()
    {
        collider.enabled = false;
        gemGet.Play();
        animator.SetBool("isCollected", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
