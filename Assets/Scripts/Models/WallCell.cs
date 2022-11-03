using Core;
using UnityEngine;

namespace Models
{
    public class WallCell : MonoBehaviour, IWallCell
    {
        [SerializeField] private GameObject _upWall;
        public Vector2 Position { get; }
        
        public void Enable(Vector2 position, float height)
        {
            transform.position = position;
            Vector2 wallPosition = _upWall.transform.localPosition;
            wallPosition.y = height;
            _upWall.transform.localPosition = wallPosition;
        }

        public void Move(float distance)
        {
            var position = transform.position;
            position.x -= distance;
            transform.position = position;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}