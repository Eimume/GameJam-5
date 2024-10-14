using UnityEngine;
using UnityEngine.UI;

public class RockPaperScissorsEntity : MonoBehaviour
{
    public RockPaperScissor choice;
    public Image choiceIconUI;
    public RockPaperScissor[] allChoices;

    public void SetChoice(RockPaperScissor selectedChoice)
    {
        choice = selectedChoice;
        choiceIconUI.sprite = choice.icon;
        Debug.Log(gameObject.name + " chose " + choice.choiceName);
    }

    // Randomly select a choice (used by the enemy)
    public void RandomizeChoice()
    {
        int randomIndex = Random.Range(0, allChoices.Length);
        SetChoice(allChoices[randomIndex]);
    }

}
