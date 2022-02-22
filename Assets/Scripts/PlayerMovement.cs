using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Fields
    [SerializeField] private float speed;
    [SerializeField] private GameObject uiCanvas;
    private PlayerControls playerControls;
    private InputAction moveVert;
    private InputAction moveHori;
    private Vector3 dir;

    // UI Fields
    private UIManager uiManager;
    private int health;
    private int money;
    


    // Unity Awake Function
    private void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        moveVert = playerControls.Player.MoveVert;
        moveHori = playerControls.Player.MoveHori;

        // Setup UI
        uiManager = uiCanvas.GetComponent<UIManager>();
        health = 6;
        money = 0;
        uiManager.UpdateHearts(health);
    }

    // Unity Update Function
    private void Update()
    {
        dir = Vector3.zero;
        dir.x += moveHori.ReadValue<float>();
        dir.y += moveVert.ReadValue<float>();
        gameObject.transform.position += dir * speed * Time.deltaTime;
    }
}
