using System;
using System.Collections.Generic;

namespace ProjectA
{
    public class TaskExecutor
    {
        // Queue to store tasks for processing
        private readonly Queue<string> taskQueue = new Queue<string>();

        // Maximum number of retries for failed tasks
        private readonly int maxRetries;

        // Constructor initializes the maxRetries value
        public TaskExecutor(int maxRetries = 3)
        {
            this.maxRetries = maxRetries;
        }

        // Adds a task to the executor with enhanced null checks and logging
        public void AddTask(string task)
        {
            // Check if the task is null or empty
            if (string.IsNullOrWhiteSpace(task))
            {
                // Log an error message for invalid input
                LogError("Invalid task. Task cannot be null or empty.");
                return;
            }

            // Add the task to the queue
            taskQueue.Enqueue(task);

            // Log an informational message indicating the task was added
            LogInfo($"Task '{task}' added to the queue.");
        }

        // Processes and clears all tasks with improved exception handling
        public void ProcessTasks()
        {
            int processedCount = 0; // Counter for successfully processed tasks

            // Check if the queue is empty
            if (taskQueue.Count == 0)
            {
                // Log an informational message indicating no tasks to process
                LogInfo("No tasks to process.");
                return;
            }

            // Process each task in the queue
            while (taskQueue.Count > 0)
            {
                string task = taskQueue.Dequeue(); // Dequeue the next task
                ProcessTaskWithRetries(task); // Process the task with retry logic
                processedCount++; // Increment the processed task counter
            }

            // Log an informational message indicating the total number of tasks processed
            LogInfo($"Processing complete. Total tasks processed: {processedCount}");
        }

        // Returns the list of current tasks in the queue
        public IEnumerable<string> GetTasks()
        {
            // Convert the queue to an array and return it
            return taskQueue.ToArray();
        }

        // Handles retry logic for failed tasks with enhanced logging
        private void ProcessTaskWithRetries(string task)
        {
            int retryCount = 0; // Counter for retry attempts

            // Retry processing the task until the maximum retry limit is reached
            while (retryCount < maxRetries)
            {
                try
                {
                    // Log an informational message indicating the task is being processed
                    LogInfo($"Processing task: {task}");

                    // Attempt to execute the task
                    ExecuteTask(task);

                    // Log an informational message indicating the task was processed successfully
                    LogInfo($"Task '{task}' processed successfully.");
                    return; // Exit the method on successful execution
                }
                catch (Exception ex)
                {
                    retryCount++; // Increment the retry counter

                    // Log an error message indicating the failure and retry attempt
                    LogError($"Error processing task '{task}': {ex.Message}. Retry {retryCount}/{maxRetries}");

                    // If the maximum retry limit is reached, log a final error message
                    if (retryCount == maxRetries)
                    {
                        LogError($"Task '{task}' failed after {maxRetries} retries. Exception: {ex}");
                    }
                }
            }
        }

        // Logs errors with structured messages
        private void LogError(string message)
        {
            // Log the error message with a timestamp
            Console.WriteLine($"[ERROR] {DateTime.Now}: {message}");
        }

        // Logs informational messages
        private void LogInfo(string message)
        {
            // Log the informational message with a timestamp
            Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
        }

        // Executes a single task with enhanced exception handling
        private void ExecuteTask(string task)
        {
            // Check if the task is null or empty
            if (string.IsNullOrWhiteSpace(task))
            {
                // Throw an exception for invalid input
                throw new ArgumentException("Task execution failed due to empty or null content.");
            }

            // Check if the task contains the word "Fail"
            if (task.Contains("Fail"))
            {
                // Throw an exception for invalid task content
                throw new InvalidOperationException("Task execution failed due to invalid content.");
            }

            // Simulate task execution logic here
        }
    }
}
