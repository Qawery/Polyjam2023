using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        private float delay = 5.0f;
        public GameState GameState { get; private set; } = new ();

        private void Start()
        {
            GameState.PlayerDeck.AddCards(new List<CardLogicData>{new ("Test Card 1"), new ("Test Card 1"), 
                                                                new ("Test Card 1"), new ("Test Card 1"), 
                                                                new ("Test Card 2"), new ("Test Card 2"), 
                                                                new ("Test Card 2"), new ("Test Card 2")});
            GameState.PlayerDeck.Shuffle();
        }

        private void Update()
        {
            if (delay > 0.0f)
            {
                delay -= Time.deltaTime;
                if (delay <= 0.0f)
                {
                    GameState.PlayerHand.AddCards(GameState.PlayerDeck.TakeCards(GameState.PlayerDeck.NumberOfCardsInDeck));
                }
            }
        }
    }
}
