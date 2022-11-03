using System;
using Core;
using UnityEngine;

namespace UI
{
    public class DifficultScreen : MonoBehaviour
    {
        [SerializeField] private SelectButton[] _selectButtons;
        private DifficultProvider _difficultProvider;
        private Action _callback;

        public void Bind(DifficultProvider difficultProvider)
        {
            _difficultProvider = difficultProvider;
            var difficults = difficultProvider.GetAll();
            for (int i = 0; i < difficults.Length; i++)
            {
                var difficult = difficults[i];
                _selectButtons[i].Bind(difficult.Value, () =>
                {
                    difficultProvider.Select(difficult);
                    _callback?.Invoke();
                });
            }
        }
        
        public void Bind(DifficultProvider difficultProvider, Action callback)
        {
            _callback = callback;
            Bind(difficultProvider);
        }
    }
}