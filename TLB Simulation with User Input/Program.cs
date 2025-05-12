using TLBSimulation;

/// <summary>
/// Main program entry point for TLB Simulation
/// </summary>
class Program
{
    /// <summary>
    /// Main method to run the TLB simulation
    /// </summary>
    /// <param name="args">Command-line arguments</param>
    static void Main(string[] args)
    {
        // Create simulator
        TLBSimulator simulator = new TLBSimulator();

        // User input and simulation
        Console.WriteLine("TLB Simulation Program");
        Console.WriteLine("Enter Virtual Page Numbers (space-separated):");
        Console.WriteLine("Example: 10 20 30 40 10 20 50 60 70 10");

        // Read and process input with error handling
        int[] addresses = ParseAddressInput();

        // Perform simulation
        SimulationResults results = simulator.RunSimulation(addresses);

        // Display results
        DisplaySimulationResults(results);
    }

    /// <summary>
    /// Parses address input with robust error handling
    /// </summary>
    /// <returns>Array of parsed virtual page numbers</returns>
    static int[] ParseAddressInput()
    {
        while (true)
        {
            try
            {
                // Read input
                string input = Console.ReadLine().Trim();

                // Check if input is empty
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    continue;
                }

                // Split and parse input
                int[] addresses = input
                    .Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s =>
                    {
                        // Attempt to parse each address
                        if (!int.TryParse(s, out int address))
                        {
                            throw new FormatException($"Invalid input: '{s}' is not a valid integer.");
                        }

                        // Optional: Add additional validation if needed
                        if (address < 0)
                        {
                            throw new ArgumentException($"Invalid input: '{s}' must be a non-negative integer.");
                        }

                        return address;
                    })
                    .ToArray();

                // Validate that at least one address was parsed
                if (addresses.Length == 0)
                {
                    Console.WriteLine("No valid addresses found. Please try again.");
                    continue;
                }

                return addresses;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing input: {ex.Message}");
                Console.WriteLine("Please enter valid space-separated integers.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Input validation error: {ex.Message}");
                Console.WriteLine("Please enter non-negative integers.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.WriteLine("Please try again.");
            }
        }
    }

    /// <summary>
    /// Displays the results of the TLB simulation
    /// </summary>
    /// <param name="results">Simulation performance results</param>
    static void DisplaySimulationResults(SimulationResults results)
    {
        Console.WriteLine("\nSimulation Performance:");
        Console.WriteLine($"Total Lookups: {results.TotalLookups}");
        Console.WriteLine($"Hits: {results.Hits}");
        Console.WriteLine($"Hit Ratio: {results.HitRatio:P2}");

        Console.WriteLine("\nDetailed Translations:");
        foreach (var translation in results.Translations)
        {
            Console.WriteLine(translation.IsHit
                ? $"Hit: VPN {translation.VPN} -> PPN {translation.PPN}"
                : $"Miss: VPN {translation.VPN} -> PPN {translation.PPN}");
        }
    }
}
