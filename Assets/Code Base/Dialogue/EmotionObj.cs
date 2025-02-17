using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EmotionObj", menuName = "Scriptable Objects/EmotionObj")]
public class EmotionObj : ScriptableObject
{
    public Emotion[] npc = new Emotion[Enum.GetNames(typeof(Characters)).Length];
    
    [Serializable]
    public class Emotion
    {
        [FormerlySerializedAs("npc")] public Characters characters;
        public EmotionsSprites[] sprites = new EmotionsSprites[Enum.GetNames(typeof(CharactersEmotion)).Length];
    }
    
    [Serializable]
    public class EmotionsSprites
    {
        [FormerlySerializedAs("npcEmotion")] [SerializeField] private CharactersEmotion charactersEmotion;
        [SerializeField] private Sprite[] sprites;
        public CharactersEmotion CharactersEmotion => charactersEmotion;
        public Sprite[] Sprites => sprites;
    }
}
