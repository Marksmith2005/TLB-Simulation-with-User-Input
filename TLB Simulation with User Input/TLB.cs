using System;
using System.Collections.Generic;
using System.Linq;

namespace TLBSimulation
{
    /// <summary>
    /// Represents the Translation Lookaside Buffer
    /// Manages page translations with a fixed capacity
    /// </summary>
    public class TLB
    {
        // List to store TLB entries
        private List<TLBEntry> entries = new List<TLBEntry>();

        // Fixed capacity of TLB
        private const int capacity = 4;

        /// <summary>
        /// Lookup a Virtual Page Number in the TLB
        /// </summary>
        /// <param name="vpn">Virtual Page Number to look up</param>
        /// <returns>Corresponding Physical Page Number if found, null otherwise</returns>
        public int? Lookup(int vpn)
        {
            // Find and return matching entry
            var entry = entries.FirstOrDefault(e => e.VPN == vpn);
            return entry != null ? entry.PPN : (int?)null;
        }

        /// <summary>
        /// Insert a new entry into the TLB
        /// Removes oldest entry if TLB is at capacity
        /// </summary>
        /// <param name="vpn">Virtual Page Number to insert</param>
        /// <param name="ppn">Physical Page Number to insert</param>
        public void Insert(int vpn, int ppn)
        {
            // Remove oldest entry if at capacity
            if (entries.Count >= capacity)
                entries.RemoveAt(0);

            // Add new entry
            entries.Add(new TLBEntry { VPN = vpn, PPN = ppn });
        }
    }

    /// <summary>
    /// Represents a single entry in the Translation Lookaside Buffer
    /// </summary>
    public class TLBEntry
    {
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