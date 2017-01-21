using System;
using UnityEngine;

namespace Configuration
{
    [Serializable]
    public class RoundWaveConfig
    {
        public float Force;
        public float MaxRadius;
        public float Lifetime;
        public Gradient Color;
    }
}