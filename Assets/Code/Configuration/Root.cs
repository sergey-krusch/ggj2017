using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu]
    public class Root : ScriptableObject
    {
        public const string ResourcePath = "DefaultSettings";

        public int BaseSavedPoints;
        public int BaseDrownedPoints;
        public float MaxHumanVelocity;
        public float MaxWaveAngularSpeedAddition;
        public float MaxAngularSpeed;
        public RoundWaveConfig RoundWave;
        public LineWaveConfig LineWave;
        public bool DeveloperMode;

        private static Root instance;
        public static Root Instance
        {
            get
            {
                if (instance == null)
                    instance = Resources.Load<Root>(ResourcePath);
                return instance;
            }
        }
    }
}