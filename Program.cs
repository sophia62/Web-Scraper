using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

// C# Web Scraper
// Author: Sophia Beebe 
//
// This program scrapes the front page of Hacker News (https://news.ycombinator.com/)
// to retrieve the top story titles. It then saves these titles to a CSV file.
//
// Demonstrates:
// - HTTP requests (networking)
// - Basic HTML parsing (string operations)
// - Data storage in CSV
// - Classes, loops, conditionals

class Headline
{
    public string Title { get; set; }

    public Headline(string title)
    {
        Title = title;
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // URL to scrape
        string url = "https://news.ycombinator.com/";
        
        // Fetch HTML
        string html = await FetchHtml(url);
        
        // Parse headlines
        List<Headline> headlines = ParseHeadlines(html);

        // Display and save headlines
        Console.WriteLine("Scraped Headlines:");
        foreach (var h in headlines)
        {
            Console.WriteLine("- " + h.Title);
        }

        SaveToCsv(headlines, "headlines.csv");
        Console.WriteLine("Headlines have been saved to headlines.csv");
    }

    static async Task<string> FetchHtml(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Get HTML from the site
                string responseBody = await client.GetStringAsync(url);
                return responseBody;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching HTML: " + ex.Message);
                return "";
            }
        }
    }

    static List<Headline> ParseHeadlines(string html)
    {
        
        List<Headline> headlines = new List<Headline>();
        
        string[] parts = html.Split(new string[] { "<span class=\"titleline\">" }, StringSplitOptions.None);

        foreach (string part in parts.Skip(1)) // skip the first chunk before the first titleline
        {
            
            int linkStart = part.IndexOf("<a href=\"");
            if (linkStart == -1) continue;

            
            int linkEnd = part.IndexOf("</a>", linkStart);
            if (linkEnd == -1) continue;

            string linkContent = part.Substring(linkStart, linkEnd - linkStart);
           
            int titleStart = linkContent.IndexOf('>');
            if (titleStart == -1) continue;

            string title = linkContent.Substring(titleStart + 1).Trim();
            if (!string.IsNullOrEmpty(title))
            {
                headlines.Add(new Headline(title));
            }
        }

        return headlines;
    }

    static void SaveToCsv(List<Headline> headlines, string filename)
    {
        using (var writer = new StreamWriter(filename))
        {
            writer.WriteLine("Title");
            foreach (var h in headlines)
            {
              
                string safeTitle = h.Title.Replace("\"", "\"\"");
                writer.WriteLine($"\"{safeTitle}\"");
            }
        }
    }
}
