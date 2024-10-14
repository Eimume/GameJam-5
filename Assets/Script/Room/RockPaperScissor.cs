using UnityEngine;

[CreateAssetMenu(fileName = "RockPaperScissor", menuName = "RockPaperScissor/Random")]
public class RockPaperScissor : ScriptableObject
{
    public string choiceName;
    public RockPaperScissor[] beats;
    public Sprite icon;
    //public int choiceValue;


}
/*
[CreateAssetMenu(fileName = "Rock", menuName = "RockPaperScissor/Rock")]
public class Rock : RockPaperScissor
{
    int choiceValue = 1;
}

[CreateAssetMenu(fileName = "Paper", menuName = "RockPaperScissor/Paper")]
public class Paper : RockPaperScissor
{
    int choiceValue = 2;
}

[CreateAssetMenu(fileName = "Scissor", menuName = "RockPaperScissor/Scissor")]
public class Scissor : RockPaperScissor
{
    int choiceValue = 3;
}
*/

