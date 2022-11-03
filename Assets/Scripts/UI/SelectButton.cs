using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        private Action _callback;

        private void Start()
        {
            _button.onClick.AddListener(() => _callback?.Invoke());
        }

        public void Bind(string text, Action callback)
        {
            _callback = callback;
            _text.text = text;
            gameObject.SetActive(true);
        }

        private void Reset()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _button = GetComponent<Button>();
        }
    }
}