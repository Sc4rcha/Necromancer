using System.Collections.Generic;
using UnityEngine;
using static GameInfo;

public class GameInfo
{
    public enum SUIT { Hearts, Diamonds, Clubs, Spades}

    public class Card 
    {
        public string Id;
        public SUIT Suit;
        public int Value;
        public Sprite Sprite;
        public Sprite SpriteHand;

        public bool IsEnemy { get { return Suit == SUIT.Spades || Suit == SUIT.Clubs; } }

        public Card(string id, SUIT suit, int value, Sprite sprite, Sprite spriteHand) 
        {
            Id = id;
            Suit = suit;
            Value = value;
            Sprite = sprite;
            SpriteHand = spriteHand;
        }

    }

    [System.Serializable]
    public class StorySegment
    {
        public bool IsComplete;
        public string[] cardCodes;

        public StorySegment (int length)
        {
            IsComplete = false;
            cardCodes = new string[length];
        }
    }
}

namespace SAVE
{

    [System.Serializable]
    public class GameSave
    { 
        public bool TutorialComplete;
        public int ScoreBest;
        public List<string> CardsUnlocked;
        public StorySegment[] StorySegments;

        public GameSave() 
        {
            TutorialComplete = false;
            ScoreBest = 0;
            CardsUnlocked = new List<string>();
            StorySegments = new StorySegment[6];
        }

        public void SetupStorySegments(SoStorySegment[] reference) 
        {
            for (int i = 0; i < reference.Length; i++)
                StorySegments[i] = new StorySegment(reference[i].CorrectOrder.Length);
        }
        public bool CheckStorySegment(int index, SoStorySegment reference) 
        {
            for (int i = 0; i < reference.CorrectOrder.Length; i++)
            {
                if (StorySegments[index].cardCodes[i] != reference.CorrectOrder[i].Id)
                    return false;
            }

            return true;
        }
    }

    public class  SaveManager
    {
        public const string SAVE_KEY = "Save_1.0";

        public static void Save(GameSave gameSave)
        {
            string json = JsonUtility.ToJson(gameSave, true);

            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();

            Debug.Log("Game Saved");
        }
        public static GameSave Load()
        {
            if (!PlayerPrefs.HasKey(SAVE_KEY))
                return null;

            string json = PlayerPrefs.GetString(SAVE_KEY);

            Debug.Log("Game Loaded");

            return JsonUtility.FromJson<GameSave>(json);
        }
        public static void ClearSave()
        {
            PlayerPrefs.DeleteKey(SAVE_KEY);
        }
        public static bool CheckSave()
        {
            return PlayerPrefs.HasKey(SAVE_KEY);
        }
    }
}

