using System;
using System.Collections.Generic;

// Represents a task with a priority
public class TaskItem
{
    public int Priority { get; set; }
    public string Description { get; set; }
}

// AVL Tree Node
public class AVLTreeNode
{
    public TaskItem Task;
    public AVLTreeNode Left;
    public AVLTreeNode Right;
    public int Height;

    public AVLTreeNode(TaskItem task)
    {
        Task = task;
        Height = 1;
    }
}

// AVL Tree for task prioritization
public class TaskPriorityAVLTree
{
    private AVLTreeNode root;

    public void Insert(TaskItem task)
    {
        root = Insert(root, task);
    }

    public TaskItem PopHighestPriorityTask()
    {
        if (root == null) return null;
        AVLTreeNode minNode;
        root = RemoveMin(root, out minNode);
        return minNode?.Task;
    }

    public TaskItem Search(int priority)
    {
        var node = root;
        while (node != null)
        {
            if (priority == node.Task.Priority)
                return node.Task;
            else if (priority < node.Task.Priority)
                node = node.Left;
            else
                node = node.Right;
        }
        return null;
    }

    private AVLTreeNode Insert(AVLTreeNode node, TaskItem task)
    {
        if (node == null)
            return new AVLTreeNode(task);

        if (task.Priority < node.Task.Priority)
            node.Left = Insert(node.Left, task);
        else
            node.Right = Insert(node.Right, task);

        UpdateHeight(node);
        return Balance(node);
    }

    private AVLTreeNode RemoveMin(AVLTreeNode node, out AVLTreeNode minNode)
    {
        if (node.Left == null)
        {
            minNode = node;
            return node.Right;
        }
        node.Left = RemoveMin(node.Left, out minNode);
        UpdateHeight(node);
        return Balance(node);
    }

    private int Height(AVLTreeNode node) => node?.Height ?? 0;

    private void UpdateHeight(AVLTreeNode node)
    {
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    private int BalanceFactor(AVLTreeNode node)
    {
        return Height(node.Left) - Height(node.Right);
    }

    private AVLTreeNode Balance(AVLTreeNode node)
    {
        int balance = BalanceFactor(node);
        if (balance > 1)
        {
            if (BalanceFactor(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        if (balance < -1)
        {
            if (BalanceFactor(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }

    private AVLTreeNode RotateRight(AVLTreeNode y)
    {
        AVLTreeNode x = y.Left;
        AVLTreeNode T2 = x.Right;
        x.Right = y;
        y.Left = T2;
        UpdateHeight(y);
        UpdateHeight(x);
        return x;
    }

    private AVLTreeNode RotateLeft(AVLTreeNode x)
    {
        AVLTreeNode y = x.Right;
        AVLTreeNode T2 = y.Left;
        y.Left = x;
        x.Right = T2;
        UpdateHeight(x);
        UpdateHeight(y);
        return y;
    }

    // Print all tasks in order
    public void PrintInOrder()
    {
        if (root == null)
        {
            Console.WriteLine("No tasks in the tree.");
            return;
        }
        PrintInOrder(root);
    }

    private void PrintInOrder(AVLTreeNode node)
    {
        if (node == null) return;
        PrintInOrder(node.Left);
        Console.WriteLine($"[{node.Task.Priority}] {node.Task.Description}");
        PrintInOrder(node.Right);
    }

    // Clear the tree
    public void Clear()
    {
        root = null;
    }
}

// Centralized entry point for all demos.

public class Program
{
    public static void Main(string[] args)
    {
        // Centralized, user-friendly CLI for all demos
        while (true)
        {
            Console.WriteLine("\nSelect a demo to run:");
            Console.WriteLine("1. API Request Priority Queue Demo");
            Console.WriteLine("2. Optimized Sorting Demo");
            Console.WriteLine("3. Binary Search Tree (AVL) Demo");
            Console.WriteLine("Q. Quit");
            Console.Write("Enter choice (1, 2, 3, or Q): ");
            var input = Console.ReadLine();
            switch (input?.Trim().ToLowerInvariant())
            {
                case "1":
                    Console.WriteLine("\n--- API Request Priority Queue Demo ---");
                    ApiRequestPriorityQueueDemo.RunDemo();
                    break;
                case "2":
                    Console.WriteLine("\n--- Optimized Sorting Demo ---");
                    Sorting.RunDemo();
                    break;
                case "3":
                    Console.WriteLine("\n--- Binary Search Tree (AVL) Demo ---");
                    BinaryTreeDemo.RunDemo();
                    break;
                case "q":
                    Console.WriteLine("Exiting. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
