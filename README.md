# algorithms-c-sharp

A C# project demonstrating efficient data structures and algorithms, with interactive CLI demos for learning and benchmarking.  
The codebase showcases practical, production-ready implementations with clear inline documentation and LLM-generated optimizations.

---

## Features

- **API Request Priority Queue**  
  Efficient, thread-safe priority queue for API requests using a binary min-heap.  
  - O(log n) enqueue/dequeue  
  - Batch operations and robust error handling  
  - See [`ApiRequestPriorityQueueDemo`](ProjectA/ApiRequestPriorityQueueDemo.cs)

- **Optimized Sorting**  
  Fast, in-place QuickSort and parallel QuickSort for large datasets.  
  - CLI lets you sort test, custom, or large random datasets  
  - Benchmarks and sample output  
  - See [`Sorting`](ProjectA/OptimizedSortingDemo.cs)

- **AVL Binary Search Tree**  
  Self-balancing BST for fast insert/search (O(log n)).  
  - Interactive demo for insertion, search, and in-order traversal  
  - See [`BinaryTreeDemo`](ProjectA/BinaryTreeDemo.cs)

- **Task Priority AVL Tree**  
  Specialized AVL tree for managing prioritized tasks.  
  - Insert, pop highest-priority, search, and print tasks  
  - See [`TaskPriorityAVLTree`](ProjectA/TaskPriorityAVLTree.cs)

---

## Usage

1. **Build and Run Demos**  
   ```sh
   dotnet run --project ProjectA
   ```
   Follow the interactive menu to explore each algorithm demo.

2. **Project Structure**
   - [`ProjectA`](ProjectA/): Main console app and demos
   - `.devcontainer/`: Codespaces/VS Code dev container setup
   - `.github/`, `.vscode/`: CI and editor configs

---

## Purpose

This project is designed for:
- Learning and comparing efficient algorithm implementations in C#
- Demonstrating LLM-assisted code improvements and best practices
- Providing ready-to-use, well-documented code for real-world scenarios

---

## Reflections

The `TaskExecutor` class demonstrates robust task management with retry logic and structured logging. Key improvements include:

- **Enhanced exception handling**: The retry mechanism ensures tasks are retried up to a configurable limit, with detailed logs for each attempt.
- **Structured logging**: The `LogError` and `LogInfo` methods provide timestamped messages, improving debugging and monitoring.
- **Input validation**: Null checks and error handling in `AddTask` and `ExecuteTask` prevent invalid input from causing runtime errors.
- **Code readability**: Inline comments and clear method structures make the code maintainable and easy to understand.
- **Scalability**: The queue-based design supports efficient task processing, suitable for real-world applications.
- **Transparency**: Detailed feedback during retries ensures users understand task failures and retry attempts.
- **Maintainability**: The modular design allows for easy extension, such as adding new task types or modifying retry logic.

Overall, the `TaskExecutor` class is a practical example of applying best practices for reliability and maintainability in task management systems. Similar principles are applied across `ProjectA`, including efficient algorithms (e.g., QuickSort in `OptimizedSortingDemo`) and self-balancing data structures (e.g., AVLTree in `BinaryTreeDemo`). These implementations showcase production-ready solutions with clear documentation, making the project a valuable resource for learning and benchmarking.
