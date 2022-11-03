using System.Linq;
using Balance.Data;
using UnityEngine;

namespace Balance.Config
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "BalanceConfig", order = 0)]
    public class BalanceConfig : ScriptableObject
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private DifficultConfig[] _difficultConfigs;
        
        public BalanceData Get()
        {
            return new BalanceData(_levelConfig.Get(), _playerConfig.Get(), _difficultConfigs.Select(v => v.Difficult).ToArray());
        }
    }
}