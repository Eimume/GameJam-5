using UnityEngine;

[CreateAssetMenu(fileName = "NewActionType", menuName = "Action/ActionType")]
public class ActionType : ScriptableObject
{
    public string actionName;  // Name of the action (e.g., Normal Attack, Special Block)
    public bool isAttack;      // Is this an attack?
    public bool isSpecial;     // Is this a special attack or block? 
}
