using UnityEngine;

namespace Core
{
    public interface IWallCell
    {
        public Vector2 Position { get; }
        public void Enable(Vector2 position, float height);
        public void Move(float distance);
        public void Disable();
    }
}