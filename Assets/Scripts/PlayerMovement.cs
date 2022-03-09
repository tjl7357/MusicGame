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
    public List<GameObject> nearbyObjects;
    private PlayerControls playerControls;
    private InputAction moveVert;
    private InputAction moveHori;
    private InputAction interact;
    private Vector3 dir;

    // UI Fields
    private UIManager uiManager;
    private int health;
    private int money;
    


    // Unity Awake Function
    private void Awake()
    {
        nearbyObjects = new List<GameObject>(2);

        playerControls = new PlayerControls();
        playerControls.Enable();
        moveVert = playerControls.Player.MoveVert;
        moveHori = playerControls.Player.MoveHori;
        interact = playerControls.Player.Interact;
        interact.performed += Interact;

        // Setup UI
        uiManager = uiCanvas.GetComponent<UIManager>();
        health = 6;
        money = 534;
        
    }

    // Unity Start Function
    private void Start()
    {
        uiManager.UpdateHearts(health);
        uiManager.UpdateMoney(money);
    }

    // Unity Update Function
    private void Update()
    {
        dir = Vector3.zero;
        dir.x += moveHori.ReadValue<float>();
        dir.y += moveVert.ReadValue<float>();
        gameObject.transform.position += dir * speed * Time.deltaTime;
    }

    // Adds a certain amount of money to the player's bank
    public void UpdateMoney(int update)
    {
        money += update;
        if (money < 0) money = 0;
        Debug.Log(money);
        uiManager.UpdateMoney(money);
    }


    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("E Pressed");
        for (int i = 0; i < nearbyObjects.Count; i++)
        {
            if (nearbyObjects[i].CompareTag("Interactable"))
            {
                Debug.Log("Chest Found");
                nearbyObjects[i].GetComponent<Interactable>().Interact();
                break;
            }
        }
    }
}
