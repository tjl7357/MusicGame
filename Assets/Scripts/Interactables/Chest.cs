using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    // Fields
    [SerializeField] private int moneyCount;
    private PlayerMovement playerRef;


    private void Awake()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }


    public override void Interact()
    {
        playerRef.UpdateMoney(moneyCount);
    }
}
