using System.Collections.Generic;

namespace TLBSimulation
{
    /// <summary>
    /// Stores overall simulation performance metrics
    /// </summary>
    public class SimulationResults
    {
        /// <summary>
        /// Total number of address translations
        /// </summary>
        public int TotalLookups { get; set; }

        /// <summary>
        /// Number of TLB hits
        /// </summary>
        public int Hits { get; set; }

        /// <summary>
        /// Hit ratio of the TLB
        /// </summary>
        public double HitRatio { get; set; }

        /// <summary>
        /// Detailed list of individual translations
        /// </summary>
        public List<TranslationResult> Translations { get; set; }
    }
}