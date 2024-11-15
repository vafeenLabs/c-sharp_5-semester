#!/bin/bash

# Check if the Migrations directory exists and remove it if it does
if [ -d "./Migrations" ]; then
    rm -rf ./Migrations
fi

# Check if the database file exists and remove it if it does
if [ -f "./database.db" ]; then
    rm ./database.db
fi

# Add a new migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update