using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentChest : Interactable
{
    // Fields
    [SerializeField] private Instrument storedInstrument;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private UIManager uiManager;
    private InstrumentManagement playerRef;
    private SpriteRenderer spriteRenderer;
    private bool opened;



    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<InstrumentManagement>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        opened = false;
    }

    // Overried function for interactions
    // Gives the player money and changes the chest sprite
    public override void Interact()
    {
        if (!opened)
        {
            playerRef.CurInstrument = storedInstrument;
            spriteRenderer.sprite = openedSprite;
            uiManager.UpdateDialog($"Picked Up {storedInstrument.ToString()}");
            opened = true;
        }
    }
}
