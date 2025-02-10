using UnityEngine;

public class DialogueNpc : InteractiveObject
{
    [SerializeField] private string npcName;
    [SerializeField] private Sprite portrait;
    [SerializeField] private DialogueObj dialogueObj;
    [SerializeField] private Transform pos;
    
    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueObj,0,portrait);
        OnePersonCamera.Instance.SetTarget(pos.transform,TypeMoveCamera.WithRotation,true,false);
        CharacterInputController.Instance.enabled = false;
    }
}
