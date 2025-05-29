#!/bin/sh
set -e

# Create a new console project named ProjectA
dotnet new console -n ProjectA

# Move the AVL tree code into the new project
mv /workspaces/dotnet-codespaces/TaskPriorityAVLTree.cs /workspaces/dotnet-codespaces/ProjectA/TaskPriorityAVLTree.cs

echo "ProjectA created. To run:"
echo "cd /workspaces/dotnet-codespaces/ProjectA"
echo "dotnet run"
echo ""
echo "Note: You must run 'dotnet run' from inside the ProjectA directory:"
echo "    cd /workspaces/dotnet-codespaces/ProjectA && dotnet run"
