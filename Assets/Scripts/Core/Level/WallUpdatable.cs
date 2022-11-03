using System;
using System.Collections.Generic;
using Balance.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Level
{
    public class WallUpdatable : IUpdatable, IDropable
    {
        private readonly LevelData _levelData;
        private readonly IDifficultProvider _difficultProvider;
        private readonly IPool<IWallCell> _pool;
        private readonly ITimeProvider _timeProvider;
        private Queue<IWallCell> _activeWalls = new Queue<IWallCell>();
        private float _distance;

        public WallUpdatable(ITimeProvider timeProvider, IPool<IWallCell> pool, LevelData levelData, IDifficultProvider difficultProvider)
        {
            _timeProvider = timeProvider;
            _pool = pool;
            _levelData = levelData;
            _difficultProvider = difficultProvider;
        }

        public void Updating()
        {
            float distance = _levelData.SpeedByLevel[_difficultProvider.Selected].Speed * _timeProvider.DeltaTime;
            _distance += distance;
            if (_distance >= _levelData.DistanceToSpawn)
            {
                _distance = 0;
                InitNewWall();
            }

            Span<IWallCell> wallCells = new Span<IWallCell>(_activeWalls.ToArray());
            foreach (var wallCell in wallCells)
            {
                wallCell.Move(distance);
            }

            if (_activeWalls.TryPeek(out var lastCell))
            {
                if (lastCell.Position.x <= _levelData.PositionToDisable)
                {
                    lastCell.Disable();
                    _pool.Add(lastCell);
                    _activeWalls.Dequeue();
                }
            }
        }

        private void InitNewWall()
        {
            var wall = _pool.Get();
            _activeWalls.Enqueue(wall);
            var startPoint = Random.Range(_levelData.MinHeightPosition, _levelData.MaxHeightPosition);
            var height = Random.Range(_levelData.MinHeight, _levelData.MaxHeight);
            wall.Enable(new Vector2(_levelData.StartPosition, startPoint), height);
        }

        public void Drop()
        {
            _distance = 0;
            foreach (var activeWall in _activeWalls)
            {
                activeWall.Disable();
                _pool.Add(activeWall);
            }
            _activeWalls.Clear();
        }
    }
}