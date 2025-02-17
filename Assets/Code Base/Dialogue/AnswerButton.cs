using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private Sprite wosActiveSprite;
    private Button _button;
    private PlayerAnswer _playerAnswer;

    public void Setup(PlayerAnswer answer)
    {
        _playerAnswer = answer;
        _button = GetComponent<Button>();
        _button.GetComponentInChildren<TMP_Text>().text = _playerAnswer.text;
        if (_playerAnswer.wosActive)
        {
            _button.image.sprite = wosActiveSprite;
        }
        if (answer.isExit)
        {
            _button.onClick.AddListener(EndDialogue);
            return;
        }
        _button.onClick.AddListener(SendValue);
    }
    
    private void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
        DialogueLog.Instance.AddPhrase(_playerAnswer.text, Characters.Hero);
        DialogueUI.Instance.ClearAnswers();
    }

    private void SendValue()
    {
        _playerAnswer.wosActive = true;
        DialogueManager.Instance.NextNode(_playerAnswer.toNode);
        ValueManager.Instance.AddValue(_playerAnswer.value);
        DialogueLog.Instance.AddPhrase(_playerAnswer.text,Characters.Hero);
        DialogueUI.Instance.ClearAnswers(); 
    }
}
