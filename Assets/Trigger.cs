
using UnityEngine;

public class Trigger : MonoBehaviour
{  
    [SerializeField] private DialogueObj dialogueObj;
    [SerializeField] private DialogueType dialogueType;
    private bool _isActive;
    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
        {
            DialogueManager.Instance.StartDialogue(dialogueObj,dialogueType);
            _isActive = true;
        }
    }
}
