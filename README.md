# C# Web Scraper

## Overview
This C# console application scrapes the front page of [Hacker News](https://news.ycombinator.com/) to extract the top story headlines.
It then saves these headlines into a `headlines.csv` file in a structured format.

## Running Instructions
1. Install .NET SDK if not already installed.
2. Create a new console app: `dotnet new console -n WebScraper`
3. Replace the `Program.cs` content with the provided code.
4. Run `dotnet build` followed by `dotnet run`.
5. After execution, `headlines.csv` will be created in the project directory containing the scraped headlines.

## Video Demonstration
[Video Demonstration Link](https://youtu.be/t4HLygcU-ng)

## What I Learned
- How to perform HTTP GET requests in C# using `HttpClient`.
- Basic HTML parsing techniques.
- How to store extracted data into a CSV file.
- Fundamental C# concepts including classes, loops, and exception handling.

## Future Work
-Learning about the legalities of web scraping
