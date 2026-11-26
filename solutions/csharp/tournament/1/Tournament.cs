
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Tournament
{
    private class TeamStats
    {
        public string Name { get; set; }
        public int MP { get; set; }
        public int W { get; set; }
        public int D { get; set; }
        public int L { get; set; }
        public int P { get; set; }

        public TeamStats(string name)
        {
            Name = name;
        }
    }

    public static void Tally(Stream input, Stream output)
    {
        var teams = new Dictionary<string, TeamStats>();

        using (var reader = new StreamReader(
            input, Encoding.UTF8, true, 1024, true))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(';');

                string team1 = parts[0];
                string team2 = parts[1];
                string result = parts[2];

                if (!teams.ContainsKey(team1))
                    teams[team1] = new TeamStats(team1);

                if (!teams.ContainsKey(team2))
                    teams[team2] = new TeamStats(team2);

                TeamStats first = teams[team1];
                TeamStats second = teams[team2];

                first.MP++;
                second.MP++;

                switch (result)
                {
                    case "win":
                        first.W++;
                        first.P += 3;
                        second.L++;
                        break;

                    case "loss":
                        first.L++;
                        second.W++;
                        second.P += 3;
                        break;

                    case "draw":
                        first.D++;
                        second.D++;
                        first.P++;
                        second.P++;
                        break;
                }
            }
        }

        var sortedTeams = teams.Values
            .OrderByDescending(team => team.P)
            .ThenBy(team => team.Name, StringComparer.Ordinal)
            .ToList();

        var resultText = new StringBuilder();

        resultText.Append(
            "Team                           | MP |  W |  D |  L |  P");

        foreach (var team in sortedTeams)
        {
            resultText.Append('\n');
            resultText.AppendFormat(
                "{0,-30} | {1,2} | {2,2} | {3,2} | {4,2} | {5,2}",
                team.Name,
                team.MP,
                team.W,
                team.D,
                team.L,
                team.P);
        }

        using (var writer = new StreamWriter(
            output, new UTF8Encoding(false), 1024, true))
        {
            writer.Write(resultText.ToString());
            writer.Flush();
        }
    }
}
