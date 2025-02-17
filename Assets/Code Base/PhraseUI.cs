using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhraseUI : MonoBehaviour
{
    [SerializeField] private Image imageName;
    
    [SerializeField] private TMP_Text phraseText;
    [SerializeField] private TMP_Text nameText;

    [Space] 
    [SerializeField] private Sprite aprilNamePrefab;
    [SerializeField] private Sprite heroNamePrefab;
    [SerializeField] private Sprite patientNamePrefab;
    
    public void Setup(string phraseText, Characters characters)
    {
        switch (characters)
        {
            case Characters.April:
                nameText.text = "April";
                imageName.sprite = aprilNamePrefab;
                break;
            case Characters.Hero:
                nameText.text = "Hero";
                imageName.sprite = heroNamePrefab;
                break;
            case Characters.Patient:
                nameText.text = "Patient";
                imageName.sprite = patientNamePrefab;
                break;
        }
        this.phraseText.text = phraseText;
    }
    
}
