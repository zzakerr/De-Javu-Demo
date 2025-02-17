using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class DialogueUI : SingletonBase<DialogueUI>
{
    
    [Space]
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text npcDialogueText;
    [SerializeField] private TMP_Text playerDialogueText;
    [SerializeField] private Image portraitImage;
    
    [Space]
    [SerializeField] private Transform answersContainer;
    [SerializeField] private Transform npcDialogueTextContainer;
    [SerializeField] private Transform playerDialogueTextContainer;
    [SerializeField] private Transform buttonLog;
    
    [Space]
    [SerializeField] private GameObject answerButtonPrefab;
    
    [Space]
    [SerializeField] private EmotionObj emotionsObj;
    
    private void Awake()
    {
        Init();
        ClearAnswers();
    }

    public void SetTextProperties(Characters characters,CharactersEmotion emotion,string text)
    {
        var portrait = FindEmotions(characters,emotion);
        
        switch (characters)
        {
            case Characters.Hero:
                SwitchPlayerDialogue(true);
                SwitchNpcDialogue(false);
                playerDialogueText.text = text;
                break;
            case Characters.April:
                SwitchPlayerDialogue(false);
                SwitchNpcDialogue(true);
                npcDialogueText.text = text;
                npcNameText.text = characters.ToString();
                portraitImage.sprite = portrait;
                break;
            case Characters.Patient:
                SwitchPlayerDialogue(false);
                SwitchNpcDialogue(true);
                npcDialogueText.text = text;
                npcNameText.text = characters.ToString();
                portraitImage.sprite = portrait;
                break;
        }

        DialogueLog.Instance.AddPhrase(text,characters);
    }

    private void SwitchPlayerDialogue(bool value)
    {
        playerDialogueTextContainer.gameObject.SetActive(value);
    }

    private void SwitchNpcDialogue(bool value)
    {
        npcDialogueTextContainer.gameObject.SetActive(value);
    }
    
    
    public void CreateAnswer(PlayerAnswer[] playerAnswers)
    {
        foreach (var t in playerAnswers)
        {
            var buttonPrefab = Instantiate(answerButtonPrefab, answersContainer);
            buttonPrefab.GetComponent<AnswerButton>().Setup(t);
        }
    }
    
    public void ClearAnswers()
    {
        for (int i = 0; i < answersContainer.childCount; i++)
        {
            Destroy(answersContainer.GetChild(i).gameObject);
        }
    }
    
    public void EndDialogue()
    {
        ClearAnswers();
        SwitchNpcDialogue(false);
        SwitchPlayerDialogue(false);
    }
    
    private Sprite FindEmotions(Characters characters,CharactersEmotion emotion)
    {
        foreach (var emotions in emotionsObj.npc)
        {
            if (emotions.characters != characters) continue;
            foreach (var type in emotions.sprites)
            {
                if (type.CharactersEmotion == emotion)
                {
                    return type.Sprites[Random.Range(0, type.Sprites.Length)];
                }
            }
        }
        return null;
    }
}
