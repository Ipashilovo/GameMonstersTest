using System.Collections.Generic;
using Entities;

namespace Balance.Data
{
    public class LevelData
    {
        public IReadOnlyDictionary<Difficult, LevelSpeedData> SpeedByLevel { get; }
        public float PositionToDisable { get;  }
        public float DistanceToSpawn { get;  }
        public float StartPosition { get;  }
        public float MinHeight { get;  }
        public float MaxHeight { get;  }
        public float MinHeightPosition { get;  }
        public float MaxHeightPosition { get;  }

        public LevelData(float positionToDisable, float distanceToSpawn, float startPosition, float minHeightPosition, float maxHeightPosition, float minHeight, float maxHeight, IReadOnlyDictionary<Difficult, LevelSpeedData> speedByLevel)
        {
            PositionToDisable = positionToDisable;
            DistanceToSpawn = distanceToSpawn;
            StartPosition = startPosition;
            MinHeightPosition = minHeightPosition;
            MaxHeightPosition = maxHeightPosition;
            MinHeight = minHeight;
            MaxHeight = maxHeight;
            SpeedByLevel = speedByLevel;
        }
    }
}