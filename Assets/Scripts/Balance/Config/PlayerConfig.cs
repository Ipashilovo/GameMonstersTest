using System;
using System.Linq;
using Balance.Data;
using Entities;
using UnityEngine;

namespace Balance.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Balance/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private StatsByDifficult[] _statsByDifficults;
        [SerializeField] private Vector2 _startPosition;
        [Serializable]
        private class StatsByDifficult
        {
            [SerializeField] private DifficultConfig _difficult;
            [SerializeField] private float _speedStep;
            [SerializeField] private float _timeToSpeedUp;
            [SerializeField] private float _baseSpeed;
            public Difficult Difficult => _difficult.Difficult;
            public float TimeToSpeedUp => _timeToSpeedUp;
            public float BaseSpeed => _baseSpeed;
            public float SpeedStep => _speedStep;
        }
        public PlayerData Get()
        {
            var result = _statsByDifficults.ToDictionary(k => k.Difficult,
                v => new PlayerStats(v.BaseSpeed, v.SpeedStep, v.TimeToSpeedUp));
            return new PlayerData(result, _startPosition);
        }
    }
}