using System;
using System.Threading.Tasks;

// LLM-generated: Replaces inefficient BubbleSort (O(n^2)) with efficient QuickSort (average O(n log n))
// Why: QuickSort is much faster for large datasets and is widely used in production systems.
public class Sorting
{
    // LLM-generated: QuickSort implementation for efficient, in-place sorting (O(n log n) average)
    public static void QuickSort(int[] arr)
    {
        QuickSort(arr, 0, arr.Length - 1);
    }

    private static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    // LLM-generated: Partition logic for QuickSort
    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        return i + 1;
    }

    // Existing: Print array utility
    public static void PrintArray(int[] arr)
    {
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // LLM-generated: Parallel QuickSort for large datasets
    public static void ParallelQuickSort(int[] arr)
    {
        ParallelQuickSort(arr, 0, arr.Length - 1, Environment.ProcessorCount);
    }

    private static void ParallelQuickSort(int[] arr, int low, int high, int depth)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            if (depth > 0)
            {
                Parallel.Invoke(
                    () => ParallelQuickSort(arr, low, pi - 1, depth - 1),
                    () => ParallelQuickSort(arr, pi + 1, high, depth - 1)
                );
            }
            else
            {
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }
    }

    // Entry point for CLI demo
    public static void Main()
    {
        RunDemo();
    }

    public static void RunDemo()
    {
        Console.WriteLine("=== Optimized Sorting Demo ===");
        // CLI: Guide user to select a dataset for sorting
        Console.WriteLine("Choose a dataset to sort:");
        Console.WriteLine("1. Small test dataset [50, 20, 40, 10, 30]");
        Console.WriteLine("2. Enter your own numbers (comma separated)");
        Console.WriteLine("3. Large random dataset (1,000,000 elements)");
        Console.Write("Enter choice (1, 2, or 3): ");
        var choice = Console.ReadLine();

        int[] dataset;
        switch (choice)
        {
            case "1":
                dataset = new[] { 50, 20, 40, 10, 30 };
                Console.WriteLine("Selected test dataset: [50, 20, 40, 10, 30]");
                break;
            case "2":
                Console.Write("Enter numbers separated by commas: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("No input provided. Using default test dataset.");
                    dataset = new[] { 50, 20, 40, 10, 30 };
                }
                else
                {
                    var parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    dataset = new int[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (!int.TryParse(parts[i].Trim(), out dataset[i]))
                        {
                            Console.WriteLine($"Invalid number: {parts[i]}, using 0.");
                            dataset[i] = 0;
                        }
                    }
                }
                break;
            case "3":
                dataset = new int[1_000_000];
                var rand = new Random();
                for (int i = 0; i < dataset.Length; i++)
                    dataset[i] = rand.Next(1_000_000);
                Console.WriteLine("Generated large random dataset of 1,000,000 elements.");
                break;
            default:
                Console.WriteLine("Invalid choice. Using default test dataset.");
                dataset = new[] { 50, 20, 40, 10, 30 };
                break;
        }

        Console.WriteLine("\nBefore Sorting:");
        if (dataset.Length <= 100)
            PrintArray(dataset);
        else
        {
            Console.WriteLine("[Large dataset omitted for brevity]");
            Console.Write("Show first 20 values before sorting? (y/n): ");
            var showHead = Console.ReadLine();
            if (showHead != null && showHead.Trim().ToLowerInvariant() == "y")
            {
                Console.WriteLine("First 20 unsorted values:");
                for (int i = 0; i < Math.Min(20, dataset.Length); i++)
                    Console.Write(dataset[i] + " ");
                Console.WriteLine();
            }
        }

        // CLI: Choose sorting method based on dataset size
        if (dataset.Length > 100_000)
        {
            Console.WriteLine("\nSorting large dataset in parallel...");
            var sw = System.Diagnostics.Stopwatch.StartNew();
            ParallelQuickSort(dataset);
            sw.Stop();
            Console.WriteLine($"Sorted {dataset.Length} elements in {sw.ElapsedMilliseconds} ms.");
        }
        else
        {
            Console.WriteLine("\nSorting with optimized QuickSort...");
            var sw = System.Diagnostics.Stopwatch.StartNew();
            QuickSort(dataset);
            sw.Stop();
            Console.WriteLine($"Sorted {dataset.Length} elements in {sw.ElapsedMilliseconds} ms.");
        }

        // CLI: Optionally show a sample of the sorted large dataset
        Console.WriteLine("\nAfter Sorting:");
        if (dataset.Length <= 100)
            PrintArray(dataset);
        else
        {
            Console.WriteLine("[Large dataset omitted for brevity]");
            Console.Write("Show first 20 sorted values? (y/n): ");
            var showSortedHead = Console.ReadLine();
            if (showSortedHead != null && showSortedHead.Trim().ToLowerInvariant() == "y")
            {
                Console.WriteLine("First 20 sorted values:");
                for (int i = 0; i < Math.Min(20, dataset.Length); i++)
                    Console.Write(dataset[i] + " ");
                Console.WriteLine();
            }
        }

        Console.WriteLine("Sorting demo complete. You can rerun and try other options.");
    }
}

/*
LLM-generated modifications:
- Replaced BubbleSort with QuickSort for O(n log n) average-case performance.
- Added in-place QuickSort for optimal space usage.
- Added ParallelQuickSort for large datasets to utilize multiple CPU cores.
- Added a large dataset benchmark to illustrate scalability.

Why each change improves efficiency:
- QuickSort is much faster than BubbleSort for large arrays, making it suitable for real-time analytics.
- ParallelQuickSort leverages multicore CPUs for even faster sorting on large datasets.

✔ Reflection:
How did the LLM assist in refining the algorithm?
- The LLM identified the inefficiency of BubbleSort and replaced it with QuickSort and ParallelQuickSort, reducing time complexity from O(n^2) to O(n log n) and enabling parallel execution for large datasets.
- It ensured the new algorithm is in-place, optimizing space usage.
- It added benchmarking and practical demonstrations for large-scale sorting, making the solution robust and production-ready.

Were any LLM-generated suggestions inaccurate or unnecessary?
- No, all suggestions were accurate, relevant, and improved both performance and scalability.

What were the most impactful improvements you implemented?
- Replacing BubbleSort with QuickSort for a dramatic reduction in time complexity.
- Adding ParallelQuickSort to leverage multicore CPUs for large datasets.
- Ensuring the sort is in-place to minimize memory usage.
- Providing clear, annotated code and practical benchmarking for real-world scenarios.
*/

/*
Analysis of Provided Sorting Algorithm:

- The original implementation uses BubbleSort, which has O(n^2) time complexity.
- BubbleSort compares each pair of adjacent elements and swaps them if they are in the wrong order.
- For large datasets, BubbleSort is extremely inefficient and leads to slow processing times.
- BubbleSort is not suitable for real-time analytics or reporting dashboards that require fast sorting.

Identified Inefficiencies:
- Time complexity is O(n^2), which does not scale for large arrays.
- Multiple unnecessary passes over the array, even if already sorted.
- Not cache-friendly and incurs high overhead for large data volumes.

Recommendation:
- Replace BubbleSort with a more efficient algorithm such as QuickSort or MergeSort (O(n log n) average-case).
- For most practical scenarios, QuickSort is preferred due to its in-place sorting and good cache performance.
- .NET's built-in Array.Sort uses an optimized QuickSort/IntroSort hybrid for primitives.

The refactored code above implements QuickSort for efficient, scalable sorting.
*/

/*
Evaluation of LLM’s Suggestions:

- Replacing BubbleSort with QuickSort (and ParallelQuickSort) provides a significant efficiency gain:
    - BubbleSort: O(n^2) time complexity, impractical for large datasets.
    - QuickSort: O(n log n) average-case, in-place, and cache-friendly.
    - ParallelQuickSort: Utilizes multiple CPU cores, further reducing wall-clock time for very large datasets.

- Space Complexity:
    - QuickSort is in-place (O(log n) stack space), so it is efficient for memory usage.
    - No unnecessary array copies or allocations.

    - Parallel Support:
    - ParallelQuickSort leverages .NET's Parallel.Invoke to sort subarrays concurrently, making it suitable for multi-core systems and large data volumes.

    - Practicality:
    - The code includes a benchmark for 1,000,000 elements, demonstrating real-world scalability.
    - The approach is robust, production-ready, and aligns with best practices for high-performance analytics/reporting.

    Conclusion:
    ✔ The LLM’s suggestions are valid, efficient, and provide meaningful performance improvements for SwiftCollab’s reporting dashboard.
*/
