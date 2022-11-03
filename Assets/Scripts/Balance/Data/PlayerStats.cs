namespace Balance.Data
{
    public struct PlayerStats
    {
        public readonly float BaseVecrticalSpeed;
        public readonly float SpeedStep;
        public readonly float TimeToUpSpeed;

        public PlayerStats(float baseVecrticalSpeed, float speedStep, float timeToUpSpeed)
        {
            BaseVecrticalSpeed = baseVecrticalSpeed;
            SpeedStep = speedStep;
            TimeToUpSpeed = timeToUpSpeed;
        }
    }
}