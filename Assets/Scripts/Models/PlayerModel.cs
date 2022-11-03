using System;
using Core;
using Core.PlayerSystem;
using UnityEngine;

namespace Models
{
    public class PlayerModel : MonoBehaviour, IPlayerModel
    {
        private Vector3 _startPosition;
        public Vector2 Position => transform.position;
        
        public event Action Hited;

        private void Start()
        {
            _startPosition = transform.position;
        }

        public void Move(float direction)
        {
            Vector2 position = transform.position;
            position.y += direction;
            transform.position = position;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            transform.position = _startPosition;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Hited?.Invoke();
        }
    }
}