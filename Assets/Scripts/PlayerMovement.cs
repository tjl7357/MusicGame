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
    private bool damageable;

    // UI Fields
    private UIManager uiManager;
    private int health;
    private int money;

    // Death fields
    private Vector3 returnPosition;


    // Properties
    public bool Damageable
    {
        get { return damageable; }
    }

    public Vector3 ReturnPosition
    {
        set { returnPosition = value; } // NOTE: Should only be adjusted by checkpoints
    }


    // Unity Awake Function
    private void Awake()
    {
        nearbyObjects = new List<GameObject>(2);
        damageable = true;

        playerControls = new PlayerControls();
        playerControls.Enable();
        moveVert = playerControls.Player.MoveVert;
        moveHori = playerControls.Player.MoveHori;
        interact = playerControls.Player.Interact;
        interact.performed += Interact;

        // Setup UI
        uiManager = uiCanvas.GetComponent<UIManager>();
        health = 6;
        money = 0;

        // Starting return position
        returnPosition = gameObject.transform.position;
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

    // Adds or subtracts a certain amount of health from the player
    public void UpdateHealth(int update)
    {
        if (update < 0 && damageable)
        {
            damageable = false;
            StartCoroutine(DmgInvuln(3.0f));
            health += update;
        }
        else if (update > 0)
        {
            health += update;
        }
        uiManager.UpdateHearts(health);
        if (health <= 0) PlayerDeath();
    }

    // Keypress that triggers an interactable object
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


    private void PlayerDeath()
    {
        uiManager.UpdateDialog("You Have Died!");
        gameObject.transform.position = returnPosition;
    }

    /// Damage invlun coroutine
    IEnumerator DmgInvuln(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        damageable = true;
    }
}
