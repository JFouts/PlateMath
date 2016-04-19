using System;
using System.Collections.Generic;
using System.Text;

namespace PlateMath
{
    public enum MeasurementType { Imperial, Metric }

    public class PlateMathSettings
    {
        public float BarWeight { get; set; }
        public MeasurementType WeightSystem { get; set; }
    }
}
