using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    public PlayerMovement player;
    public Collider2D groundCheck;
    public Collider2D platform;
    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (groundCheck.bounds.Intersects(platform.bounds) && player.Jump && player.Move.y < 0)
        { 
            platform.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        platform.enabled = true;
    }
}
