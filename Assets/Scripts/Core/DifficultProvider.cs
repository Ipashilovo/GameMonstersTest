using Entities;
using State;

namespace Core
{
    public class DifficultProvider : IDifficultProvider
    {
        private readonly Difficult[] _difficults;
        public Difficult Selected { get; private set; }

        public DifficultProvider(Difficult[] difficults)
        {
            _difficults = difficults;
        }
        
        public Difficult[] GetAll()
        {
            return _difficults;
        }

        public void Select(Difficult difficult)
        {
            Selected = difficult;
        }
    }

    public interface IDifficultProvider
    {
        public Difficult Selected { get; }
        public Difficult[] GetAll();

        public void Select(Difficult difficult);
    }
}