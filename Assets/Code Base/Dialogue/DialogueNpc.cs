using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DialogueNpc : InteractiveObject
{
    [SerializeField] private string npcName;
    [SerializeField] private DialogueObj dialogueObj;
    [SerializeField] private DialogueType dialogueType;
    [SerializeField] private Transform pos;
    
    private DialogueObj _currentObj;
    private DialogueManager _dialogueManager;
    private int _currentObjIndex;
    private bool _isActive;

    public UnityEvent onDialogueFinished;
    
    protected override void Start()
    {
        base.Start();
        _dialogueManager = DialogueManager.Instance;
        _currentObj = dialogueObj;
    }
    
    public override void Hit()
    {
        if (_dialogueManager.IsDialogue == false)
        {
            StartCoroutine(ShowText());
        }
    }

    private void NextDialogue(DialogueObj dialogueObj)
    {
        if (dialogueObj == null) return;
        _currentObj = dialogueObj;
        _isActive = false;
    }
    
    public override void Interact()
    {
        if (!_isActive && _dialogueManager.IsDialogue == false)
        {
            _dialogueManager.StartDialogue(_currentObj, dialogueType);
            _isActive = true;
            OnePersonCamera.Instance.SetTarget(pos.transform,TypeMoveCamera.WithRotation,true,false);
            StartCoroutine(WaitDialogueEnd());
        }
    }
    
    private IEnumerator WaitDialogueEnd()
    {
        yield return new WaitWhile(()=> _dialogueManager.IsDialogue);
        onDialogueFinished.Invoke();
    }
}
