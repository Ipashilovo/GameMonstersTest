using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Balance.Data
{
    public class PlayerData
    {
        public IReadOnlyDictionary<Difficult, PlayerStats> StatsByDifficult { get; }
        public Vector2 StartPosition { get;  }

        public PlayerData(IReadOnlyDictionary<Difficult, PlayerStats> statsByDifficult, Vector2 startPosition)
        {
            StatsByDifficult = statsByDifficult;
            StartPosition = startPosition;
        }
    }
}