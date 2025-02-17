using System.Collections;
using UnityEngine;

public enum DialogueType
{
   None = 0,
   WithCharacter = 1,
   WithoutCharacter = 2,
}

public class DialogueManager : SingletonBase<DialogueManager>
{
   [SerializeField] private float maxTimeText = 6f;
   private DialogueUI _dialogueUI;
   private EffectControl _effectControl;
   
   private DialogueNode[] _nodes;
   private DialogueNode _currentNode;
   private int _currentNodeIndex;
   private int _currenNpcTextIndex;
   private int _maxNpcText;
   public bool IsDialogue { get; private set; }
   private void Awake()
   {
      Init();
   }

   private void Start()
   {
      _dialogueUI = DialogueUI.Instance;
      _effectControl = EffectControl.Instance;
   }
   
   public void StartDialogue(DialogueObj dialogue,DialogueType dialogueType)
   {
      if (IsDialogue) return;
      
      _nodes = dialogue.Dialogue;
      _currentNodeIndex = 0;
      Debug.Log("Dialogue Begin");
      IsDialogue = true;
      switch (dialogueType)
      {
         case DialogueType.WithoutCharacter:
            StartCoroutine(DialogueWithoutCharacter());
            break;
         case DialogueType.WithCharacter:;
            StartCoroutine(DialogueWithCharacter());
            break;
      }
   }
   
   public void NextNode(int nodeIndex)
   {
      _currentNodeIndex = nodeIndex;
      _currentNode = _nodes[_currentNodeIndex];
      _currenNpcTextIndex = 0;
      _maxNpcText = _currentNode.npcText.Length;
   }

   private IEnumerator DialogueWithoutCharacter()
   {
      NextNode(_currentNodeIndex);
      while (_currenNpcTextIndex < _maxNpcText)
      {
         var currentNpcNode = _currentNode.npcText[_currenNpcTextIndex];
         var npcText = currentNpcNode.text;
         var emotion = currentNpcNode.emotion;
         var npcName = currentNpcNode.name;
         var effect = currentNpcNode.effect;
         _dialogueUI.SetTextProperties(npcName,emotion,npcText);
         _effectControl.StartEffect(effect);
         _currenNpcTextIndex++;
         var waitTime = npcText.Length*0.3f;
         if (waitTime > maxTimeText) waitTime = maxTimeText;
         Debug.Log(waitTime);
         yield return new WaitForSeconds(waitTime);
      }
      EndDialogue();
   }

   private IEnumerator DialogueWithCharacter()
   {
      while (true)
      {
         NextNode(_currentNodeIndex);
         while (_currenNpcTextIndex < _maxNpcText)
         {
            var currentNpcNode = _currentNode.npcText[_currenNpcTextIndex];
            var npcText = currentNpcNode.text;
            var emotion = currentNpcNode.emotion;
            var npcName = currentNpcNode.name;
            var effect = currentNpcNode.effect;
            _dialogueUI.SetTextProperties(npcName,emotion,npcText);
            _effectControl.StartEffect(effect);
            _currenNpcTextIndex++;
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.Space));
         }
      
         if (_currentNode.playerAnswer.Length > 0)
         {
            if (_currentNode.playerAnswer[0].isExit) break; 
            if (_currentNode.playerAnswer[0].text == "")
            {
               _currentNodeIndex = _currentNode.playerAnswer[0].toNode;
               continue;
            }
            
            _dialogueUI.CreateAnswer(_currentNode.playerAnswer);
            var currentNode = _currentNodeIndex;
            yield return new WaitUntil(()=> currentNode != _currentNodeIndex);
            continue;
         }

         if (_currentNodeIndex + 1 < _nodes.Length)
         {
            _currentNodeIndex++;
            continue;
         }
         break;
      }
      Player.Instance.ReturnCamera();
      EndDialogue();
   }
   
   public void EndDialogue()
   {
      _dialogueUI.EndDialogue();
      _nodes = null;
      _currentNode = null;
      _currentNodeIndex = 0;
      _currenNpcTextIndex = 0;
      _maxNpcText = 0;
      Debug.Log("Dialogue End");
      IsDialogue = false;
   }
}
