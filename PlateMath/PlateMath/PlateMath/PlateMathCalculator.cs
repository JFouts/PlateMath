using System;
using System.Collections.Generic;
using System.Text;

namespace PlateMath
{
    public class PlateMathCalculator
    {
        private float BarWeight;
        public float TotalWeight { get; private set; }

        public PlateMathCalculator()
        {
            BarWeight = 45f;
            TotalWeight = BarWeight;
        }

        public PlateMathCalculator(PlateMathSettings settings)
        {
            BarWeight = settings.BarWeight;
            TotalWeight = BarWeight;
        }

        public float AddWeight(float weight)
        {
            TotalWeight += weight * 2f;
            return TotalWeight;
        }

        public float RemoveWeight(float weight)
        {
            TotalWeight -= weight * 2f;
            return TotalWeight;
        }

        public float ClearWeight()
        {
            TotalWeight = BarWeight;
            return TotalWeight;
        }
    }
}
