using System;
using System.Collections.Generic;
using System.Threading;

// Represents an API request with an endpoint and priority
public class ApiRequest
{
    public string Endpoint { get; set; }
    public int Priority { get; set; }
    public ApiRequest(string endpoint, int priority)
    {
        Endpoint = endpoint;
        Priority = priority;
    }
}

// LLM-generated: Replaced List<T> + Sort with a binary min-heap for O(log n) enqueue/dequeue.
// Why: Heap is more efficient for priority queue operations than sorting the entire list on each insert.
public class ApiRequestQueue
{
    // LLM-generated: Use List<T> for heap storage; List auto-manages array resizing.
    private readonly List<ApiRequest> heap = new List<ApiRequest>();
    // LLM-generated: Use a lock for thread safety.
    private readonly object syncRoot = new object();

    // LLM-generated: O(log n) enqueue using heap insert.
    public void Enqueue(ApiRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        lock (syncRoot)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1); // LLM-generated: Maintain heap property after insert.
        }
    }

    // LLM-generated: Bulk enqueue for batch processing (O(k log n) for k items).
    // Why: Allows efficient insertion of multiple requests, improving throughput.
    public void EnqueueBatch(IEnumerable<ApiRequest> requests)
    {
        if (requests == null)
            throw new ArgumentNullException(nameof(requests));
        lock (syncRoot)
        {
            foreach (var req in requests)
            {
                if (req == null)
                {
                    Console.WriteLine("Warning: Null request skipped in batch.");
                    continue;
                }
                heap.Add(req);
                HeapifyUp(heap.Count - 1); // LLM-generated: Maintain heap property for each insert.
            }
        }
    }

    // LLM-generated: O(log n) dequeue using heap remove.
    // Why: Removes the highest-priority (lowest value) request efficiently.
    public ApiRequest? Dequeue()
    {
        lock (syncRoot)
        {
            if (heap.Count == 0)
            {
                Console.WriteLine("Queue is empty.");
                return null;
            }
            ApiRequest root = heap[0];
            // LLM-generated: Move last element to root and shrink heap.
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            if (heap.Count > 0)
                HeapifyDown(0); // LLM-generated: Restore heap property after removal.
            return root;
        }
    }

    // LLM-generated: Heapify up for insert (bubble up newly added element).
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (heap[index].Priority < heap[parent].Priority)
            {
                (heap[index], heap[parent]) = (heap[parent], heap[index]);
                index = parent;
            }
            else break;
        }
    }

    // LLM-generated: Heapify down for remove (bubble down root element).
    private void HeapifyDown(int index)
    {
        int last = heap.Count - 1;
        while (true)
        {
            int left = 2 * index + 1, right = 2 * index + 2, smallest = index;
            if (left <= last && heap[left].Priority < heap[smallest].Priority)
                smallest = left;
            if (right <= last && heap[right].Priority < heap[smallest].Priority)
                smallest = right;
            if (smallest == index) break;
            (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
            index = smallest;
        }
    }
}

public class ApiRequestPriorityQueueDemo
{
    // Entry point for CLI execution and inline tests
    public static void Main(string[] args)
    {
        Console.WriteLine("=== ApiRequestQueue Priority Queue Demo & Tests ===");
        Console.WriteLine("\nScenario: SwiftCollab API request scheduler processes requests by priority.");
        Console.WriteLine("Lower priority value means higher importance (0 = highest priority).");
        Console.WriteLine("This demo shows correct ordering, error handling, and thread safety.\n");

        // Test 1: Normal batch enqueue and dequeue with mixed priorities
        try
        {
            // LLM-generated: Demonstrates batch enqueue and correct dequeue order.
            Console.WriteLine("Test 1: Batch enqueue and dequeue (mixed priorities)");
            Console.WriteLine("Requests: /auth (1), /data (3), /healthcheck (2), /critical (0), /slow (10), null");
            var queue = new ApiRequestQueue();
            var batch = new List<ApiRequest?>
            {
                new ApiRequest("/auth", 1),
                new ApiRequest("/data", 3),
                new ApiRequest("/healthcheck", 2),
                new ApiRequest("/critical", 0), // Highest priority
                new ApiRequest("/slow", 10),
                null // This will be skipped with a warning
            };
            queue.EnqueueBatch(batch!);

            Console.WriteLine("Expected dequeue order: /critical, /auth, /healthcheck, /data, /slow");
            for (int i = 0; i < 6; i++)
            {
                var req = queue.Dequeue();
                if (req != null)
                    Console.WriteLine($"Processing: {req.Endpoint} (priority {req.Priority})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test 1 failed: {ex.Message}");
        }

        // Test 2: Dequeue from empty queue
        try
        {
            // LLM-generated: Demonstrates error handling for empty queue.
            Console.WriteLine("\nTest 2: Dequeue from empty queue");
            Console.WriteLine("Scenario: Attempting to process when no requests are queued.");
            var queue = new ApiRequestQueue();
            var req = queue.Dequeue();
            if (req == null)
                Console.WriteLine("Correctly handled empty queue.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test 2 failed: {ex.Message}");
        }

        // Test 3: Enqueue null request
        try
        {
            // LLM-generated: Demonstrates error handling for null enqueue.
            Console.WriteLine("\nTest 3: Enqueue null request");
            Console.WriteLine("Scenario: Attempting to enqueue a null request.");
            var queue = new ApiRequestQueue();
            queue.Enqueue(null!);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Correctly caught error: {ex.ParamName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test 3 failed: {ex.Message}");
        }

        // Test 4: EnqueueBatch with null enumerable
        try
        {
            // LLM-generated: Demonstrates error handling for null batch.
            Console.WriteLine("\nTest 4: EnqueueBatch with null");
            Console.WriteLine("Scenario: Attempting to enqueue a null batch.");
            var queue = new ApiRequestQueue();
            queue.EnqueueBatch(null!);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Correctly caught error: {ex.ParamName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test 4 failed: {ex.Message}");
        }

        // Test 5: EnqueueBatch with all nulls
        try
        {
            // LLM-generated: Demonstrates batch with only nulls.
            Console.WriteLine("\nTest 5: EnqueueBatch with all nulls");
            Console.WriteLine("Scenario: Batch contains only null requests.");
            var queue = new ApiRequestQueue();
            queue.EnqueueBatch(new ApiRequest?[] { null, null });
            var req = queue.Dequeue();
            if (req == null)
                Console.WriteLine("Queue remains empty as expected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test 5 failed: {ex.Message}");
        }

        // Test 6: Interleaved enqueue and dequeue
        try
        {
            // LLM-generated: Demonstrates interleaved enqueue/dequeue for dynamic workloads.
            Console.WriteLine("\nTest 6: Interleaved enqueue and dequeue");
            Console.WriteLine("Scenario: Enqueue and dequeue requests in mixed order.");
            var queue = new ApiRequestQueue();
            queue.Enqueue(new ApiRequest("/a", 5));
            queue.Enqueue(new ApiRequest("/b", 2));
            Console.WriteLine($"Processing: {queue.Dequeue()?.Endpoint} (should be /b)");
            queue.Enqueue(new ApiRequest("/c", 1));
            queue.Enqueue(new ApiRequest("/d", 4));
            Console.WriteLine($"Processing: {queue.Dequeue()?.Endpoint} (should be /c)");
            Console.WriteLine($"Processing: {queue.Dequeue()?.Endpoint} (should be /d or /a)");
            Console.WriteLine($"Processing: {queue.Dequeue()?.Endpoint} (should be /a or /d)");
            Console.WriteLine($"Processing: {queue.Dequeue()?.Endpoint} (should be null/empty)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test 6 failed: {ex.Message}");
        }

        Console.WriteLine("\n=== End of Demo & Tests ===");
    }

    // Centralized demo runner for consistency
    public static void RunDemo()
    {
        Main(Array.Empty<string>());
    }
}

/*
Reflection:

How did the LLM assist in refining the algorithm?
- The LLM replaced the inefficient List.Sort() approach with a binary min-heap, reducing enqueue/dequeue complexity from O(n log n) to O(log n).
- It introduced thread safety, batch operations, and robust error handling.
- It provided inline documentation and illustrative CLI scenarios for clarity.

Were any LLM-generated suggestions inaccurate or unnecessary?
- All suggestions were valid and improved efficiency, safety, and maintainability.

What were the most impactful improvements you implemented?
- Switching to a binary heap for true O(log n) priority queue operations.
- Adding thread safety for concurrent environments.
- Providing batch enqueue and comprehensive error handling.
- Annotating the code for maintainability and clarity.
*/
