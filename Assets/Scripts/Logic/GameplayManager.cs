using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2023
{
    public class GameplayManager : MonoBehaviour
    {
        public GameState GameState { get; private set; } = new ();

        private void Start()
        {
            GameState.PlayerDeck.AddCards(new List<CardLogic>{new ("Test Card 1"), new ("Test Card 1"), 
                                                                new ("Test Card 1"), new ("Test Card 1"), 
                                                                new ("Test Card 2"), new ("Test Card 2"), 
                                                                new ("Test Card 2"), new ("Test Card 2")});
            GameState.PlayerDeck.Shuffle();
        }
    }
}
