using Entities;
using UnityEngine;

namespace Balance.Config
{
    [CreateAssetMenu(fileName = "DifficultConfig", menuName = "Balance/DifficultConfig", order = 0)]
    public class DifficultConfig : ScriptableObject
    {
        [SerializeField] private string _difficult;

        public Difficult Difficult => new Difficult(_difficult);
    }
}