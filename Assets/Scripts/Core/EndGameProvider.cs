using System;
using System.Collections.Generic;
using Core.PlayerSystem;
using Entities;
using State;
using UnityEngine.SceneManagement;

namespace Core
{
    public class EndGameProvider : IDisposable
    {
        private readonly Player _player;
        private readonly PlayerState _state;
        private readonly ITimeProvider _timeProvider;
        private readonly ScoreProvider _scoreProvider;
        private readonly List<IDropable> _dropables;
        public event Action Complite;

        public bool IsEnd { get; private set; }

        public EndGameProvider(Player player, PlayerState state, ITimeProvider timeProvider, ScoreProvider scoreProvider, List<IDropable> dropables)
        {
            _player = player;
            _state = state;
            _timeProvider = timeProvider;
            _scoreProvider = scoreProvider;
            _dropables = dropables;
            player.Dead += OnPlayerDead;
        }

        public void Restart()
        {
            List<int> values = new List<int>();
            Span<int> span = new Span<int>(values);
            _state.TryCount += new Amount(1);
            foreach (var dropable in _dropables)
            {
                dropable.Drop();
            }
            IsEnd = false;
        }

        public void OnPlayerDead()
        {
            IsEnd = true;
            _scoreProvider.SetScore(new Amount((int)_timeProvider.WorldTime));
            Complite?.Invoke();
        }

        public void Dispose()
        {
            _player.Dead -= OnPlayerDead;
        }
    }
}