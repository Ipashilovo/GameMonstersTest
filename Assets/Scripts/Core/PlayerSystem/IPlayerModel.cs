using System;
using UnityEngine;

namespace Core.PlayerSystem
{
    public interface IPlayerModel
    {
        public Vector2 Position { get; }
        
        public event Action Hited;
        
        public void Move(float direction);
        public void Enable();
        public void Disable();
    }
}