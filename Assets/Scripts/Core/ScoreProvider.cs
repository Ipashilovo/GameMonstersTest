using Entities;
using State;

namespace Core
{
    public class ScoreProvider
    {
        private readonly PlayerState _state;
        
        public Amount Score => _state.MaxScore;
        public Amount TryCount => _state.TryCount;

        public ScoreProvider(PlayerState state)
        {
            _state = state;
        }

        public void SetScore(Amount amount)
        {
            if (amount >= Score)
            {
                _state.MaxScore = amount;
            }
        }
    }
}