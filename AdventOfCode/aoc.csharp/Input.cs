using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace aoc.csharp;

public static class Input
{
    internal static string[] GetLines(TextReader input)
    {
        var lines = new List<string>();
        string? line;
        while ((line = input.ReadLine()) != null)
        {
            lines.Add(line);
        }

        return lines.ToArray();
    }

    internal static List<T> ToList<T>(TextReader reader) where T : IConvertible
    {
        var list = new List<T>();
        var sb = new StringBuilder();

        int read;
        while ((read = reader.Read()) >= 0)
        {
            char c = (char)read;
            if ((c >= '0' && c <= '9') || c == '-')
            {
                sb.Append(c);
            }
            else if (c == ',' || c == ' ' || c == '\n' || c == '\r')
            {
                if (sb.Length > 0)
                {
                    list.Add((T)Convert.ChangeType(sb.ToString(), typeof(T)));
                    sb.Clear();
                }
            }
            else
            {
                throw new Exception("Unexpected character");
            }
        }

        if (sb.Length > 0)
        {
            list.Add((T)Convert.ChangeType(sb.ToString(), typeof(T)));
        }

        return list;
    }

    public static async Task<TextReader> GetAsync(int year, int day)
    {
        var baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "zivkan", "CodingForFun", "AdventOfCode");
        if (!Directory.Exists(baseDirectory))
        {
            Directory.CreateDirectory(baseDirectory);
        }

        var fileName = $"{year:D4}-{day:D2}.txt";
        var fullPath = Path.Combine(baseDirectory, fileName);

        if (!File.Exists(fullPath))
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>(optional: false)
                .Build();

            var value = config["session"];

            var httpClient = new HttpClient(new HttpClientHandler { UseCookies = false });
            httpClient.DefaultRequestHeaders.Add("User-Agent", "zivkan.aoc");
            httpClient.DefaultRequestHeaders.Add("Cookie", $"session={value}");
            var url = $"https://adventofcode.com/{year}/day/{day}/input";
            var data = await httpClient.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(fullPath, data);
        }

        var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return new StreamReader(fileStream, Encoding.UTF8, leaveOpen: false);
    }
}
