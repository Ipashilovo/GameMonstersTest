using Core;
using Core.Level;
using UnityEngine;

namespace Models
{
    public class WallFactory : IWallFactory
    {
        private readonly WallCell _wallCell;

        public WallFactory(WallCell wallCell)
        {
            _wallCell = wallCell;
        }
        
        public IWallCell Get()
        {
            return Object.Instantiate(_wallCell);
        }
    }
}