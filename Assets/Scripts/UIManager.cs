using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Fields
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private GameObject moneyText;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject heartPrefab;

    [SerializeField] private GameObject dialogText;
    [SerializeField] private GameObject dialogImg;

    private int money;
    private Image[] healthBar;
    private List<GameObject> notes;

    private TextMeshProUGUI moneyTextElement;

    private TextMeshProUGUI dialogTextElement;
    private Image dialogBack;

    // Start is called before the first frame update
    void Awake()
    {
        CreateHealthBar(3);
        notes = new List<GameObject>();

        moneyTextElement = moneyText.GetComponent<TextMeshProUGUI>();
        dialogTextElement = dialogText.GetComponent<TextMeshProUGUI>();
        dialogBack = dialogImg.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Creates the health bar images based on how many hearts the player has
    /// </summary>
    /// <param name="num">The number of heart icons to make</param>
    private void CreateHealthBar(int num)
    {
        Image[] hearts = new Image[num];
        for (int i = 0; i < num; i++)
        {
            GameObject heartInst = Instantiate(heartPrefab);
            heartInst.transform.SetParent(gameObject.transform);
            RectTransform heartTransform = heartInst.GetComponent<RectTransform>();
            heartTransform.anchoredPosition = new Vector3(5 + 55 * i, 0, 0);
            heartTransform.localScale = new Vector3(1f, 1f);
            hearts[i] = heartInst.GetComponent<Image>();
        }

        healthBar = hearts;
    }

    /// <summary>
    /// Updates the health bar on the UI
    /// </summary>
    /// <param name="health">The health to set the bar to</param>
    public void UpdateHearts(int health)
    {
        // Full Hearts
        int numFilled = health / 2;
        for (int i = 0; i < numFilled; i++)
        {
            healthBar[i].sprite = fullHeart;
        }

        // Half Hearts
        if (health % 2 == 1)
        {
            healthBar[numFilled].sprite = halfHeart;
            numFilled++;
        }

        // Empty Hearts
        for (int i = numFilled; i < healthBar.Length; i++)
        {
            healthBar[i].sprite = emptyHeart;
        }
    }

    /// <summary>
    /// Sets the money value for the UI
    /// </summary>
    /// <param name="money">The value to set the money to</param>
    public void UpdateMoney(int money)
    {   
        moneyTextElement.text = money.ToString("D4");
    }

    /// <summary>
    /// Creates a note and adds it to the UI
    /// </summary>
    /// <param name="note">The number the note is representing</param>
    public void AddNote(int note)
    {
        if (notes.Count == 6) ClearNotes();
        GameObject noteInst = Instantiate(notePrefab);
        noteInst.transform.SetParent(gameObject.transform);
        RectTransform noteTransform = noteInst.GetComponent<RectTransform>();
        noteTransform.anchoredPosition = new Vector3(-100 + 40 * notes.Count, -150 + note * 20, 0);
        noteTransform.localScale = new Vector3(1.0f, 1.0f);
        notes.Add(noteInst);

        // Triggers the Coroutine
        if (notes.Count == 6) StartCoroutine(MusicClear(2.0f));
    }

    /// <summary>
    /// Clears all of the notes from the list and destorys them.
    /// </summary>
    public void ClearNotes()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            Destroy(notes[i]);
        }

        notes.Clear();
    }

    /// <summary>
    /// Updates the dialog in the dialog box
    /// </summary>
    /// <param name="dialog">The dialog to add</param>
    public void UpdateDialog(string dialog)
    {
        dialogBack.enabled = true;
        dialogTextElement.text = dialog;
        StartCoroutine(DialogCooldown(2.0f));
    }

    /// <summary>
    /// Waits a certain amount of time and then clears the dialog
    /// </summary>
    /// <param name="time">The time to wait</param>
    IEnumerator DialogCooldown(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        dialogBack.enabled = false;
        dialogTextElement.text = "";
    }

    /// <summary>
    /// Waits a certain amount of time and then clears the notes if the notes are still full
    /// </summary>
    /// <param name="time">The time to wait</param>
    IEnumerator MusicClear(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (notes.Count == 6) ClearNotes();
    }
}
