using System.Collections.Generic;
using Core.Level;

namespace Core
{
    public class WallPool : IPool<IWallCell>
    {
        private IWallFactory _wallFactory;
        private Queue<IWallCell> _cells = new Queue<IWallCell>();

        public WallPool(IWallFactory wallFactory)
        {
            _wallFactory = wallFactory;
        }
        
        public void Add(IWallCell obj)
        {
            _cells.Enqueue(obj);
        }

        public IWallCell Get()
        {
            if (_cells.TryDequeue(out var cell))
            {
                return cell;
            }

            return _wallFactory.Get();
        }
    }
}