using System;
using Balance.Data;
using UnityEngine;

namespace Core.PlayerSystem
{
    public class Player : IDisposable, IUpdatable, IDropable
    {
        private readonly IDifficultProvider _difficultProvider;
        private readonly PlayerData _playerData;
        private readonly ITimeProvider _timeProvider;
        private IPlayerModel _playerModel;
        private float _currentSpeed;
        private PlayerData _data;
        private PlayerStats _currentStats;
        private float _timeToUpSpeed;

        public event Action Dead;

        public Player(IPlayerModel playerModel, IDifficultProvider difficultProvider, PlayerData playerData, ITimeProvider timeProvider)
        {
            _difficultProvider = difficultProvider;
            _playerData = playerData;
            _timeProvider = timeProvider;
            _playerModel = playerModel;
            playerModel.Hited += OnHit;
        }

        public void Enable()
        {
            _playerModel.Enable();
            _currentStats = _playerData.StatsByDifficult[_difficultProvider.Selected];
            _currentSpeed = _currentStats.BaseVecrticalSpeed;
            SetEndTime();
        }

        public void Dispose()
        {
            _playerModel.Hited -= OnHit;
        }

        public void Updating()
        {
            int scale;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                scale = 1;
            }
            else
            {
                scale = -1;
            }

            float value = _currentSpeed * _timeProvider.DeltaTime * scale;
            _playerModel.Move(value);
            if (_timeToUpSpeed <= _timeProvider.WorldTime)
            {
                UpSpeed();
            }
        }

        private void UpSpeed()
        {
            SetEndTime();
            _currentSpeed += _currentStats.SpeedStep;
        }

        private void SetEndTime()
        {
            _timeToUpSpeed = _timeProvider.WorldTime + _currentStats.TimeToUpSpeed;
        }

        private void OnHit()
        {
            _playerModel.Disable();
            Dead?.Invoke();
        }

        public void Drop()
        {
            Enable();
        }
    }
}