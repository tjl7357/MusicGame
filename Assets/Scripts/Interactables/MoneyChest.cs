using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyChest : Interactable
{
    // Fields
    [SerializeField] private int moneyCount;
    [SerializeField] private Sprite openedSprite;
    private UIManager uiManager;
    private PlayerMovement playerRef;
    private SpriteRenderer spriteRenderer;
    private bool opened;

    // Unity Awake Function
    private void Awake()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        uiManager = GameObject.Find("PlayerUI").GetComponent<UIManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        opened = false;
    }

    // Overried function for interactions
    // Gives the player money and changes the chest sprite
    public override void Interact()
    {
        if (!opened)
        {
            playerRef.UpdateMoney(moneyCount);
            spriteRenderer.sprite = openedSprite;
            uiManager.UpdateDialog($"You Found {moneyCount} Coins!");
            opened = true;
        }
    }
}
