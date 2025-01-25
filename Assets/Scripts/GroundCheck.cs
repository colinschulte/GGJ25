using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            player.ground = collider;
            player.isGrounded = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = false;
        }
    }
}
