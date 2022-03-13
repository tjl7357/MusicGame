using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Enum to limit what notes the player can play
public enum Instrument
{
    NoInstrument = 0,
    InstrumentBase = 3,
    FirstAttachment = 4,
    SecondAttachment = 5,
    FullInstrument = 6
}

public class InstrumentManagement : MonoBehaviour
{
    // Fields
    [SerializeField] private GameObject notesObject;
    [SerializeField] private AudioSource songs;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioClip[] songClips;
    [SerializeField] private AudioClip songSuccess;
    private Instrument curInstrument;
    private PlayerMovement playerMove;
    private PlayerControls playerControls;
    private InputAction playNote;
    private string playedSong;
    
    private AudioSource[] notes;


    public Instrument CurInstrument
    {
        get { return curInstrument; }
        set { curInstrument = value; }
    }


    // Unity Awake function
    private void Awake()
    {
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerControls = new PlayerControls();
        playNote = playerControls.Player.PlayNote;
        notes = notesObject.GetComponents<AudioSource>();
        curInstrument = Instrument.NoInstrument;
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
        playerMove.nearbyObjects.Add(collision.gameObject);
        Debug.Log("Object Added: " + collision.name);
    }

    /// <summary>
    /// When an object leaves, remove it from the nearby objects list
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMove.nearbyObjects.Remove(collision.gameObject);
        Debug.Log("Object Removed: " + collision.name);
    }

    /// <summary>
    /// Adds the played note to the song.
    /// </summary>
    /// <param name="context">The context of the note played</param>
    private void NotePlayed(InputAction.CallbackContext context)
    {
        if (curInstrument != Instrument.NoInstrument)
        {
            // Parses out the number and adds to song
            int curNum;
            int.TryParse(context.control.name, out curNum);

            if (curNum <= (int)curInstrument)
            {
                notes[curNum - 1].Play();
                uiManager.AddNote(curNum);
                playedSong += curNum;

                // Once a full song is played, checks to see if is valid song
                if (playedSong.Length == 6)
                {
                    Debug.Log(playedSong);
                    CheckSong();
                    playedSong = "";
                }
            }
        }
        else uiManager.UpdateDialog("You Have No Instrument to Play");
        
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
                uiManager.UpdateDialog("Song Of Movement Played");
                StartCoroutine(DelayAudio(songClips[0]));
                for (int i = 0; i < playerMove.nearbyObjects.Count; i++)
                {
                    if (playerMove.nearbyObjects[i].CompareTag("Moveable"))
                    {
                        songs.PlayOneShot(songSuccess);
                        playerMove.nearbyObjects[i].GetComponent<Moveable>().Move();
                        Debug.Log("Rock Moved");
                        break;
                    }
                }
                break;

            // Song of Lifting (Subject to Change)
            // Make Stairs an object not on a tilemap
            case "123123":
                uiManager.UpdateDialog("Song of Lifting Played");
                for (int i=0; i < playerMove.nearbyObjects.Count; i++)
                {
                    if (playerMove.nearbyObjects[i].CompareTag("Raisable"))
                    {
                        
                    }
                }
                break;

            // If the player plays notes that don't represent a song
            default:
                uiManager.UpdateDialog("No Song Played");
                break;
        }
    }

    /// <summary>
    /// Delays the playing of an audio source
    /// </summary>
    /// <param name="source">The AudioSource to play music from</param>
    IEnumerator DelayAudio(AudioClip clip)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        songs.PlayOneShot(clip);
        Debug.Log("PlaySong");
    }
}
