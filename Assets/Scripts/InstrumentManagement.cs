using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Elements to Add
//    - Sound Effects
//    - Limiting playable notes
//    - Limitng playable songs (Maybe, maybe not, maybe selective)
//    - Add sound effects

public class InstrumentManagement : MonoBehaviour
{
    // Fields
    [SerializeField] private GameObject notesObject;
    [SerializeField] private AudioSource song1;
    private PlayerControls playerControls;
    private InputAction playNote;
    private string playedSong;
    private List<GameObject> nearbyObjects;
    private AudioSource[] notes;

    // Unity Awake function
    private void Awake()
    {
        playerControls = new PlayerControls();
        playNote = playerControls.Player.PlayNote;
        nearbyObjects = new List<GameObject>(2);
        notes = notesObject.GetComponents<AudioSource>();
    }

    // Unity OnEnable function
    private void OnEnable()
    {
        playerControls.Enable();
        playNote.performed += NotePlayed;
    }

    // Unity OnDisable function
    private void OnDisable()
    {
        playNote.performed -= NotePlayed;
        playerControls.Disable();
    }

    /// <summary>
    /// When a trigger enters the player's radius, adds it to a nearby objects list
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nearbyObjects.Add(collision.gameObject);
        Debug.Log("Object Added: " + collision.name);
    }

    /// <summary>
    /// When an object leaves, remove it from the nearby objects list
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        nearbyObjects.Remove(collision.gameObject);
        Debug.Log("Object Removed: " + collision.name);
    }

    /// <summary>
    /// Adds the played note to the song.
    /// </summary>
    /// <param name="context">The context of the note played</param>
    private void NotePlayed(InputAction.CallbackContext context)
    {
        // Parses out the number and adds to song
        int curNum;
        int.TryParse(context.control.name, out curNum);
        playedSong += curNum;

        if (curNum < 4)
        {
            notes[curNum - 1].Play();
        }

        // Once a full song is played, checks to see if is valid song
        if (playedSong.Length == 6)
        {
            Debug.Log(playedSong);
            CheckSong();
            playedSong = "";
        }
    }

    /// <summary>
    /// Checks to see if the current song is real
    /// </summary>
    private void CheckSong()
    {
        switch (playedSong)
        {
            // Song of Movement (Subject To Change)
            case "132132":
                //Debug.Log("Rock Moved");
                //rock.GetComponent<Moveable>().Move();
                for (int i = 0; i < nearbyObjects.Count; i++)
                {
                    if (nearbyObjects[i].CompareTag("Moveable"))
                    {
                        nearbyObjects[i].GetComponent<Moveable>().Move();
                        song1.Play();
                        Debug.Log("Rock Moved");
                        break;
                    }
                }
                break;

            // If the player plays notes that don't represent a song
            default:
                Debug.Log("You Played Nothing");
                break;
        }
    }
}