using System;
using UnityEngine;
using UnityEngine.Events;
using static DialogueEditor;

public class DialogueManager : SingletonBase<DialogueManager>
{
   private DialogueNode[] _nodes;
   private DialogueUI _dialogueUI;
   private DialogueNode _currentNode;
   private Sprite _currentPortrait;
   private int _currenNpcTextIndex;
   private int _maxNpcText;
   private int _maxNode;

   private void Awake()
   {
      Init();
   }

   private void Start()
   {
      _dialogueUI = DialogueUI.Instance;
      EndDialogue();
   }
   
   public void StartDialogue(DialogueObj dialogue,int startNodeIndex , Sprite portrait)
   {
      _nodes = dialogue.Dialogue;
      _currentPortrait = portrait;
      ChangeNode(startNodeIndex);
   }

   public void EndDialogue()
   {
      Debug.Log("Ending Dialogue");
      _currenNpcTextIndex = 0;
      _dialogueUI.EndDialogue();
      enabled = false;
   }

   public void ChangeNode(int nodeIndex)
   {
      _currentNode = _nodes[nodeIndex];
      _currenNpcTextIndex = 0;
      if (_currentNode.npcText.Length == 0)
      {
         CreateAnswers();
         return;
      }
      _maxNpcText = _currentNode.npcText.Length;
      enabled = true;
      _dialogueUI.SetProperties(_currentNode.npcText[_currenNpcTextIndex],_currentPortrait);
   }
   
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Mouse0))
      {
         if (_currenNpcTextIndex + 1 < _maxNpcText)
         {
            _currenNpcTextIndex++;
            _dialogueUI.SetProperties(_currentNode.npcText[_currenNpcTextIndex],_currentPortrait);
         }
         else CreateAnswers();
      }
   }

   private void CreateAnswers()
   {
      if (_currentNode.playerAnswer[0].exit)
      {
         EndDialogue();
         return;
      }
      
      if (_currentNode.playerAnswer[0].text == "")
      {
         ChangeNode(_currentNode.playerAnswer[0].toNode);
         return;
      }
      _dialogueUI.CreateAnswer(_currentNode.playerAnswer);
      enabled = false;
   }
}
