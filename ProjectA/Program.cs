using System;

namespace ProjectA
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nSelect a demo to run:");
                Console.WriteLine("1. Task Executor Demo");
                Console.WriteLine("2. API Request Priority Queue Demo");
                Console.WriteLine("3. Optimized Sorting Demo");
                Console.WriteLine("4. Binary Search Tree (AVL) Demo");
                Console.WriteLine("Q. Quit");
                Console.Write("Enter choice (1, 2, 3, 4, or Q): ");

                string? choice = Console.ReadLine()?.Trim().ToLower();
                switch (choice)
                {
                    case "1":
                        RunTaskExecutorDemo();
                        break;
                    case "2":
                        ApiRequestPriorityQueueDemo.RunDemo(); // Call the correct demo method
                        break;
                    case "3":
                        Console.WriteLine("Optimized Sorting Demo not implemented yet.");
                        break;
                    case "4":
                        BinaryTreeDemo.RunDemo();
                        break;
                    case "q":
                        running = false;
                        Console.WriteLine("Exiting. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void RunTaskExecutorDemo()
        {
            var executor = new TaskExecutor();
            bool demoRunning = true;

            while (demoRunning)
            {
                Console.WriteLine("\nTask Executor Menu:");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Process Tasks");
                Console.WriteLine("3. View Tasks");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine()?.Trim().ToLower();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter task: ");
                        string? task = Console.ReadLine();
                        executor.AddTask(task ?? string.Empty);
                        break;
                    case "2":
                        executor.ProcessTasks();
                        break;
                    case "3":
                        var tasks = executor.GetTasks();
                        Console.WriteLine("Current Tasks:");
                        foreach (var t in tasks)
                        {
                            Console.WriteLine($"- {t}");
                        }
                        break;
                    case "4":
                        demoRunning = false;
                        Console.WriteLine("Returning to Main Menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
