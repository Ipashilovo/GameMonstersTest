using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndScreen : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _openDifficult;
        [SerializeField] private Button _restartButton;
        [SerializeField] private DifficultScreen _difficultScreen;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _tryCount;
        private EndGameProvider _endGameProvider;
        private ScoreProvider _scoreProvider;

        private void Start()
        {
            _openDifficult.onClick.AddListener(() => _difficultScreen.gameObject.SetActive(!_difficultScreen.gameObject.activeSelf));
            _restartButton.onClick.AddListener(() =>
            {
                _endGameProvider?.Restart();
                gameObject.SetActive(false);
            });
        }

        public void Bind(EndGameProvider endGameProvider, DifficultProvider difficultProvider, ScoreProvider scoreProvider)
        {
            _difficultScreen.Bind(difficultProvider);
            _scoreProvider = scoreProvider;
            _endGameProvider = endGameProvider;
            endGameProvider.Complite += OnComplete;
        }

        private void OnComplete()
        {
            _tryCount.text = $"Попыток - {_scoreProvider.TryCount.Value.ToString()}";
            _score.text = $"МаксОчков - {_scoreProvider.Score.Value.ToString()}";
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            _endGameProvider.Complite -= OnComplete;
        }
    }
}