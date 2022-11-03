using Entities;

namespace Balance.Data
{
    public class BalanceData
    {
        public PlayerData PlayerData { get; }
        public LevelData LevelData { get; }
        public Difficult[] Difficultes { get;  }

        public BalanceData(LevelData levelData, PlayerData playerData, Difficult[] difficultes)
        {
            LevelData = levelData;
            PlayerData = playerData;
            Difficultes = difficultes;
        }
    }
}