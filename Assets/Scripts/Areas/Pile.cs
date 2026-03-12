using System.Collections.Generic;
using UnityEngine;
using static GameInfo;

public class Pile : MonoBehaviour
{
    public RectTransform CardTransformUp;
    public GameObject CardObjectBot;
    public int CardWidth = 2;

    public List<Card> Cards;


    protected System.Random rng;


    public virtual void Setup() 
    {
        // setup variables
        Cards = new List<Card>();
        rng = new System.Random();
    }

    protected void Shuffle() 
    {
        // shuffle pile
        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
        }
    }
}
