namespace TLBSimulation
{
    /// <summary>
    /// Represents the result of a single address translation
    /// </summary>
    public class TranslationResult
    {
        /// <summary>
        /// Indicates if the translation was a TLB hit
        /// </summary>
        public bool IsHit { get; set; }

        /// <summary>
        /// Virtual Page Number
        /// </summary>
        public int VPN { get; set; }

        /// <summary>
        /// Physical Page Number
        /// </summary>
        public int PPN { get; set; }
    }
}