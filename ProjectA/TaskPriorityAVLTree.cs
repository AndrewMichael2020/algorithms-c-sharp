using System;
using System.Collections.Generic;

public class TaskPriorityAVLTree
{
    private class Node
    {
        public int Priority { get; set; }
        public string Task { get; set; }
        public int Height { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int priority, string task)
        {
            Priority = priority;
            Task = task;
            Height = 1;
        }
    }

    private Node root;

    public void Insert(int priority, string task)
    {
        root = Insert(root, priority, task);
    }

    public string PopHighestPriority()
    {
        if (root == null) throw new InvalidOperationException("Tree is empty.");
        var (highest, newRoot) = RemoveHighestPriority(root);
        root = newRoot;
        return highest.Task;
    }

    public void PrintTasks()
    {
        PrintInOrder(root);
    }

    private Node Insert(Node node, int priority, string task)
    {
        if (node == null) return new Node(priority, task);

        if (priority < node.Priority)
            node.Left = Insert(node.Left, priority, task);
        else
            node.Right = Insert(node.Right, priority, task);

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        return Balance(node);
    }

    private (Node, Node) RemoveHighestPriority(Node node)
    {
        if (node.Right == null)
            return (node, node.Left);

        var (highest, newRight) = RemoveHighestPriority(node.Right);
        node.Right = newRight;
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        return (highest, Balance(node));
    }

    private void PrintInOrder(Node node)
    {
        if (node == null) return;
        PrintInOrder(node.Left);
        Console.WriteLine($"Priority: {node.Priority}, Task: {node.Task}");
        PrintInOrder(node.Right);
    }

    private int GetHeight(Node node) => node?.Height ?? 0;

    private int GetBalanceFactor(Node node) => GetHeight(node.Left) - GetHeight(node.Right);

    private Node Balance(Node node)
    {
        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1)
        {
            if (GetBalanceFactor(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (balanceFactor < -1)
        {
            if (GetBalanceFactor(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private Node RotateRight(Node node)
    {
        var newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        newRoot.Height = 1 + Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right));
        return newRoot;
    }

    private Node RotateLeft(Node node)
    {
        var newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        newRoot.Height = 1 + Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right));
        return newRoot;
    }
}

public class TaskPriorityAVLTreeDemo
{
    public static void Main()
    {
        var tree = new TaskPriorityAVLTree();

        // Test data
        tree.Insert(5, "Task A");
        tree.Insert(3, "Task B");
        tree.Insert(8, "Task C");
        tree.Insert(1, "Task D");
        tree.Insert(7, "Task E");

        Console.WriteLine("Tasks in priority order:");
        tree.PrintTasks();

        Console.WriteLine("\nPopping highest-priority task...");
        Console.WriteLine($"Popped Task: {tree.PopHighestPriority()}");

        Console.WriteLine("\nRemaining tasks:");
        tree.PrintTasks();
    }
}
