using UnityEngine;

namespace Core
{
    public interface ITimeProvider
    {
        public float DeltaTime { get; }
        public float WorldTime { get; }
    }

    public class TimeProvider : ITimeProvider, IDropable
    {
        public float DeltaTime { get; private set;}
        public float WorldTime { get; private set;}

        public void Update()
        {
            DeltaTime = Time.deltaTime;
            WorldTime += DeltaTime;
        }

        public void Drop()
        {
            WorldTime = 0;
            DeltaTime = 0;
        }
    }
}