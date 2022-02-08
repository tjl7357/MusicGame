using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    // Fields
    [SerializeField] private Vector2 moveDirection;
    private bool alreadyMoved = false;

    // Methods
    public void Move()
    {
        if (!alreadyMoved)
        {
            // Moves the rock to its desired position
            Vector3 position = gameObject.transform.position;
            gameObject.transform.position = new Vector3(position.x + moveDirection.x, position.y + moveDirection.y);
            alreadyMoved = true;
        }
        
    }
}
