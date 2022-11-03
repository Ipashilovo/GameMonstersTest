using System;
using System.Linq;
using Balance.Data;
using Entities;
using UnityEngine;

namespace Balance.Config
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Balance/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private float _minHeightPosition = 1;
        [SerializeField] private float _maxHeightPosition = 10;
        [SerializeField] private float _positionToDisable = -15;
        [SerializeField] private float _distanceToSpawn = 20;
        [SerializeField] private float _startPosition;
        [SerializeField] private LevelStatsByDifficult[] _difficultyConfigs;
        [SerializeField] private float _maxHeight = 2;
        [SerializeField] private float _minHeight = 5;

        [Serializable]
        private class LevelStatsByDifficult
        {
            [SerializeField] private DifficultConfig levelStatsByDifficult;
            [SerializeField] private float _speed;
            public Difficult Difficult => levelStatsByDifficult.Difficult;
            public float Speed => _speed;
        }

        public LevelData Get()
        {
            return new LevelData(_positionToDisable, _distanceToSpawn, _startPosition, _minHeightPosition,
                _maxHeightPosition, _minHeight, _maxHeight,
                _difficultyConfigs.ToDictionary(k => k.Difficult, v => new LevelSpeedData(v.Speed)));
        }
    }
}