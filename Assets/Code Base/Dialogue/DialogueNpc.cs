using UnityEngine;

public class DialogueNpc : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private Sprite portrait;
    [SerializeField] private DialogueObj dialogueObj;

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.StartDialogue(dialogueObj,0,portrait);
        }
    }
}
