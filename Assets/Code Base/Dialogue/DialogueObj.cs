using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class DialogueObj : ScriptableObject
{
   [SerializeField]private DialogueNode[] dialogue;
   public DialogueNode[] Dialogue => dialogue;
   public void SetDialogue(DialogueNode[] dialogue)
   {
      this.dialogue = dialogue;
   }
}
