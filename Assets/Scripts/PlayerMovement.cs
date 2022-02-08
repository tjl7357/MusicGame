using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Fields
    [SerializeField] private float speed;
    private PlayerControls playerControls;
    private InputAction moveVert;
    private InputAction moveHori;
    private Vector3 dir;
    



    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        moveVert = playerControls.Player.MoveVert;
        moveHori = playerControls.Player.MoveHori;
    }


    private void Update()
    {
        dir = Vector3.zero;
        dir.x += moveHori.ReadValue<float>();
        dir.y += moveVert.ReadValue<float>();
        gameObject.transform.position += dir * speed * Time.deltaTime;
    }
}
