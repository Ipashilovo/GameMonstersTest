using System;
using Core;
using Initialize;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private DifficultScreen _difficultScreen;
        private IStarter _dependencysStarter;

        private void Start()
        {
            _startButton.onClick.AddListener(() =>
            {
                _dependencysStarter?.Enable();
                gameObject.SetActive(false);
            });
        }

        public void Bind(IStarter dependencysStarter, DifficultProvider difficultProvider)
        {
            _dependencysStarter = dependencysStarter;
            _difficultScreen.Bind(difficultProvider, () => _startButton.gameObject.SetActive(true));
        }
    }
}