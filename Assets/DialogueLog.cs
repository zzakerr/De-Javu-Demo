using System.Collections.Generic;
using UnityEngine;

public class DialogueLog : SingletonBase<DialogueLog>
{
    [SerializeField] private GameObject dialogueLogBox;
    [SerializeField] private PhraseUI phrasePrefab;
    [SerializeField] private Transform phraseContainer;
    [SerializeField] private int maxPhraseCount;
    
    private List<PhraseUI> _phraseList;

    private void Awake()
    {
        Init();
        
    }

    private void Start()
    {
        _phraseList = new List<PhraseUI>();
        ClearLog();
        HideLog();
    }

    public void SwitchLog()
    {
        dialogueLogBox.SetActive(!dialogueLogBox.activeInHierarchy);
        OnePersonCamera.Instance.CameraLock(dialogueLogBox.activeInHierarchy);
    }
    
    public void ShowLog()
    {
        dialogueLogBox.SetActive(true);
        OnePersonCamera.Instance.CameraLock(dialogueLogBox.activeInHierarchy);
    }

    public void HideLog()
    {
        dialogueLogBox.SetActive(false);
        OnePersonCamera.Instance.CameraLock(dialogueLogBox.activeInHierarchy);
    }
    
    public void AddPhrase(string phrase,Sprite portrait)
    {
        var phraseUI = Instantiate(phrasePrefab, phraseContainer);
        phraseUI.Setup(phrase, portrait);
        if (_phraseList.Count >= maxPhraseCount)
        {
            Destroy(_phraseList[0]);
            _phraseList.RemoveAt(0);
        }
        _phraseList.Add(phraseUI);
    }
    
    private void ClearLog()
    {
        _phraseList.Clear();
        for (int i = 0; i < phraseContainer.childCount; i++)
        {
            Destroy(phraseContainer.GetChild(i).gameObject);
        }
    }
}
