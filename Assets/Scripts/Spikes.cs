using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private PlayerMovement playerRef;
    private bool standing;


    private void Start()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        standing = false;
    }


    private void Update()
    {
        if (standing && playerRef.Damageable)
        {
            playerRef.UpdateHealth(-1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        standing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        standing = false;
    }

}
