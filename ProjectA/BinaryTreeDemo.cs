using System;
using System.Threading.Tasks;

// Node class now includes a Height property for AVL balancing
public class Node
{
    public int Value;
    public Node? Left, Right;
    public int Height;

    public Node(int value)
    {
        Value = value;
        Left = Right = null;
        Height = 1; // Height is initialized to 1 for AVL logic
    }
}

// AVLTree implements a self-balancing binary search tree
public class AVLTree
{
    private Node? root;

    // Insert value and keep tree balanced (O(log n) time)
    public void Insert(int value)
    {
        root = Insert(root, value);
    }

    // Efficient search by value (O(log n) time in balanced tree)
    public bool Search(int value)
    {
        var node = root;
        while (node != null)
        {
            if (value == node.Value)
                return true;
            node = value < node.Value ? node.Left : node.Right;
        }
        return false;
    }

    // In-order traversal prints values in sorted order
    public void PrintInOrder()
    {
        PrintInOrder(root);
        Console.WriteLine();
    }

    private void PrintInOrder(Node? node)
    {
        if (node == null) return;
        PrintInOrder(node.Left);
        Console.Write(node.Value + " ");
        PrintInOrder(node.Right);
    }

    // AVL insert with balancing
    private Node Insert(Node? node, int value)
    {
        if (node == null)
            return new Node(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else if (value > node.Value)
            node.Right = Insert(node.Right, value);
        else
            return node; // Duplicate values not inserted

        UpdateHeight(node);
        return Balance(node);
    }

    // Height helpers
    private int Height(Node? node) => node?.Height ?? 0;

    private void UpdateHeight(Node node)
    {
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    private int BalanceFactor(Node node)
    {
        return Height(node.Left) - Height(node.Right);
    }

    // AVL balancing logic: ensures tree remains balanced after insertions
    private Node Balance(Node node)
    {
        int balance = BalanceFactor(node);
        if (balance > 1)
        {
            // Left-Right case
            if (BalanceFactor(node.Left!) < 0)
                node.Left = RotateLeft(node.Left!);
            // Left-Left case
            return RotateRight(node);
        }
        if (balance < -1)
        {
            // Right-Left case
            if (BalanceFactor(node.Right!) > 0)
                node.Right = RotateRight(node.Right!);
            // Right-Right case
            return RotateLeft(node);
        }
        return node;
    }

    // Right rotation for AVL balancing
    private Node RotateRight(Node y)
    {
        Node x = y.Left!;
        Node T2 = x.Right;
        x.Right = y;
        y.Left = T2;
        UpdateHeight(y);
        UpdateHeight(x);
        return x;
    }

    // Left rotation for AVL balancing
    private Node RotateLeft(Node x)
    {
        Node y = x.Right!;
        Node T2 = y.Left;
        y.Left = x;
        x.Right = T2;
        UpdateHeight(x);
        UpdateHeight(y);
        return y;
    }
}

// Demo class for CLI interaction
public class BinaryTreeDemo
{
    // Add a method to run the demo interactively from the main selector
    public static void RunDemo()
    {
        RunDemoInteractive();
    }

    // Rename the original interactive logic to avoid confusion
    private static void RunDemoInteractive()
    {
        var tree = new AVLTree();
        Console.WriteLine("AVL Tree Demo (Self-Balancing Binary Search Tree)");
        Console.WriteLine("Enter numbers to insert (comma separated):");
        var input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            var values = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var val in values)
            {
                if (int.TryParse(val.Trim(), out int num))
                    tree.Insert(num);
                else
                    Console.WriteLine($"Invalid number: {val}");
            }
        }
        Console.WriteLine("In-order traversal:");
        tree.PrintInOrder();

        Console.WriteLine("Enter a value to search:");
        var searchInput = Console.ReadLine();
        if (searchInput == null)
        {
            Console.WriteLine("Search input is null.");
            return;
        }
        if (int.TryParse(searchInput, out int searchVal))
        {
            bool found = tree.Search(searchVal);
            Console.WriteLine(found
                ? $"Value {searchVal} found in the tree."
                : $"Value {searchVal} not found in the tree.");
        }
        else
        {
            Console.WriteLine("Invalid input for search.");
        }
    }

    // Entry point for the project
    public static void Main(string[] args) // Remove 'async' keyword
    {
        RunDemo();
    }
}

/*
Reflection:

How did you assist in refining the code?
- I replaced the unbalanced binary tree with an AVL tree, ensuring O(log n) insert/search.
- I added an efficient iterative search method.
- I documented each change inline for clarity.

Were any LLM-generated suggestions inaccurate or unnecessary?
- No, all suggestions were relevant and addressed the performance and maintainability issues.

What were the most impactful improvements you implemented?
- Introducing AVL balancing to prevent tree degeneration.
- Adding an efficient search method.
- Improving code documentation and structure for maintainability.
*/
