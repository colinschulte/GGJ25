using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    public Collider2D groundCheck;
    public Collider2D platform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (groundCheck.bounds.Intersects(platform.bounds) && Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") < 0)
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
