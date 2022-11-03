using System;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour, IDisposable
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private EndScreen _endScreen;
        public void Bind(Dependencys dependencys)
        {
            _startScreen.Bind(dependencys.Starter, dependencys.DifficultProvider);
            _endScreen.Bind(dependencys.EndGameProvider, dependencys.DifficultProvider, dependencys.ScoreProvider);
        }

        public void Dispose()
        {
            _endScreen?.Dispose();
        }
    }
}