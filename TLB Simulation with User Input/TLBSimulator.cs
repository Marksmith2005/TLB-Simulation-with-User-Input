using System;
using System.Collections.Generic;

namespace TLBSimulation
{
    /// <summary>
    /// Manages the TLB simulation process
    /// </summary>
    public class TLBSimulator
    {
        // TLB instance for translations
        private TLB tlb;

        /// <summary>
        /// Constructor initializes TLB
        /// </summary>
        public TLBSimulator()
        {
            tlb = new TLB();
        }

        /// <summary>
        /// Runs the full TLB simulation for given addresses
        /// </summary>
        /// <param name="addresses">Virtual Page Numbers to simulate</param>
        /// <returns>Comprehensive simulation results</returns>
        public SimulationResults RunSimulation(int[] addresses)
        {
            List<TranslationResult> translations = new List<TranslationResult>();
            int totalLookups = 0;
            int hits = 0;

            foreach (var addr in addresses)
            {
                totalLookups++;
                var result = TranslateAddress(addr);
                translations.Add(result);

                if (result.IsHit)
                    hits++;
            }

            return new SimulationResults
            {
                TotalLookups = totalLookups,
                Hits = hits,
                HitRatio = (double)hits / totalLookups,
                Translations = translations
            };
        }

        /// <summary>
        /// Translate a single address through the TLB
        /// </summary>
        /// <param name="vpn">Virtual Page Number to translate</param>
        /// <returns>Translation result details</returns>
        private TranslationResult TranslateAddress(int vpn)
        {
            // Try to find in TLB
            var ppn = tlb.Lookup(vpn);

            if (ppn.HasValue)
            {
                // TLB Hit
                return new TranslationResult
                {
                    IsHit = true,
                    VPN = vpn,
                    PPN = ppn.Value
                };
            }
            else
            {
                // TLB Miss - Simulate page table lookup
                int simulatedPPN = SimulatePageTableLookup(vpn);

                // Insert into TLB
                tlb.Insert(vpn, simulatedPPN);

                return new TranslationResult
                {
                    IsHit = false,
                    VPN = vpn,
                    PPN = simulatedPPN
                };
            }
        }

        /// <summary>
        /// Simulate a page table lookup (simplified)
        /// </summary>
        /// <param name="vpn">Virtual Page Number to look up</param>
        /// <returns>Simulated Physical Page Number</returns>
        private int SimulatePageTableLookup(int vpn)
        {
            // Simple PPN generation based on VPN
            return vpn % 16384; // Assuming 14-bit PPN (2^14 pages)
        }
    }
}