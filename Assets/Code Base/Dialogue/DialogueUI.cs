using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueUI : SingletonBase<DialogueUI>
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;
    
    [SerializeField] private GameObject answerButtonPrefab;
    [SerializeField] private Transform answerButtonsContainer;
    [SerializeField] private Transform dialogueButtonsContainer;

    private int[,] _nextNodes;

    public UnityEvent<int> node;
    
    private void Awake()
    {
        Init();
        ClearAnswers();
        node.AddListener(Debag);
    }

    public void SetProperties(string text,Sprite portrait)
    {
        dialogueText.text = text;
        portraitImage.sprite = portrait;
        ShowDialogue();
    }

    public void DeleteText()
    {
        dialogueText.text = "";
    }

    private void ShowDialogue()
    {
        dialogueButtonsContainer.gameObject.SetActive(true);
        portraitImage.gameObject.SetActive(true);
        HideAnswers();
    }

    private void HideDialogue()
    {
        dialogueButtonsContainer.gameObject.SetActive(false);
        portraitImage.gameObject.SetActive(false);
    }

    private void ShowAnswers()
    {
        answerButtonsContainer.gameObject.SetActive(true);
        HideDialogue();
    }

    private void HideAnswers()
    {
        answerButtonsContainer.gameObject.SetActive(false);
    }

    private void Debag(int node)
    {
        Debug.Log(node);
    }
    
    private void I(int node)
    {
        this.node.Invoke(node);
    }
    
    public void CreateAnswer(PlayerAnswer[] playerAnswers)
    {
        foreach (var t in playerAnswers)
        {
            var buttonPrefab = Instantiate(answerButtonPrefab, answerButtonsContainer);
            buttonPrefab.GetComponent<AnswerButton>().SetProperties(t.text,t.toNode,t.value,t.exit);
        }
        ShowAnswers();
    }

    public void ClearAnswers()
    {
        HideAnswers();
        for (int i = 0; i < answerButtonsContainer.childCount; i++)
        {
            Destroy(answerButtonsContainer.GetChild(i).gameObject);
        }
    }
    
    public void EndDialogue()
    {
        ClearAnswers();
        HideDialogue();
    }
}
