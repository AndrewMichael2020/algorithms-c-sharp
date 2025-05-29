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
  - See [`TaskPriorityAVLTree`](ProjectA/Program.cs)

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

## References

- [ProjectA/ApiRequestPriorityQueueDemo.cs](ProjectA/ApiRequestPriorityQueueDemo.cs)
- [ProjectA/OptimizedSortingDemo.cs](ProjectA/OptimizedSortingDemo.cs)
- [ProjectA/BinaryTreeDemo.cs](ProjectA/BinaryTreeDemo.cs)
- [ProjectA/Program.cs](ProjectA/Program.cs)

---
