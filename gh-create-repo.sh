#!/bin/sh
# CLI script to create a new GitHub repository using the GitHub CLI

REPO_NAME="Exploring algorithm implementations"

# Create the repository (public by default)
gh repo create "$REPO_NAME" --public --confirm

echo "Repository '$REPO_NAME' created successfully."

# To run this script from the CLI:
#   sh gh-create-repo.sh
# Make sure you are authenticated with GitHub CLI (`gh auth login`) before running.
