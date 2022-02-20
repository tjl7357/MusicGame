using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Fields
    [SerializeField] private Text moneyText;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    private int health;
    private int money;
    private Image[] healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        healthBar = gameObject.GetComponentsInChildren<Image>();
        //UpdateHearts(6);
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the UI
        moneyText.text = "$" + money;
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
}
