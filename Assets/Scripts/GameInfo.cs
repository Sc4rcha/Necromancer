using UnityEngine;

public class GameInfo
{
    public enum SUIT { Hearts, Diamonds, Clubs, Spades}

    public class Card 
    {
        public SUIT Suit;
        public int Value;
        public Sprite Sprite;
        public Sprite SpriteHand;
        public Sprite SuitSprite;

        public bool IsEnemy { get { return Suit == SUIT.Spades || Suit == SUIT.Clubs; } }

        public Card(SUIT suit, int value, Sprite sprite, Sprite spriteHand, Sprite suitSprite) 
        {
            Suit = suit;
            Value = value;
            Sprite = sprite;
            SpriteHand = spriteHand;
            SuitSprite = suitSprite;
        }

    }
}
