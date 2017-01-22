using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu]
    public class Root : ScriptableObject
    {
        public const string ResourcePath = "DefaultSettings";

        public int LevelCount;
        public int BaseSavedPoints;
        public int BaseDrownedPoints;
        public RoundWaveConfig RoundWave;
        public LineWaveConfig LineWave;
        public HumanConfig Human;
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