using Core;
using Initialize;

namespace UI
{
    public class Dependencys
    {
        public ScoreProvider ScoreProvider { get; }
        public IStarter Starter { get; }
        public EndGameProvider EndGameProvider { get; }
        public DifficultProvider DifficultProvider { get; }

        public Dependencys(EndGameProvider endGameProvider, ScoreProvider scoreProvider, IStarter starter, DifficultProvider difficultProvider)
        {
            EndGameProvider = endGameProvider;
            ScoreProvider = scoreProvider;
            Starter = starter;
            DifficultProvider = difficultProvider;
        }
    }
}