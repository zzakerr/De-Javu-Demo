using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private Button _button;
    private int _nextNode;
    private int _value;

    public void SetProperties(string question,int nextNode, int value,bool isExit)
    {
        _value = value;
        _nextNode = nextNode;
        _button = GetComponent<Button>();
        _button.GetComponentInChildren<TMP_Text>().text = question;
        if (isExit)
        {
            _button.onClick.AddListener(EndDialogue);
            return;
        }
        _button.onClick.AddListener(SendValue);
    }

    private void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
        DialogueUI.Instance.ClearAnswers();
    }

    private void SendValue()
    {
        DialogueManager.Instance.ChangeNode(_nextNode);
        DialogueUI.Instance.ClearAnswers();
    }
}
