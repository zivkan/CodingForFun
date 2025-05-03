using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2016;

public class Day10 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var specification = ParseSpecification(input.ReadToEnd());
        var bots = ConfigureBots(specification.Item1, specification.Item2);

        var chip61And17Comparer = bots.Single(b => b.TransferDetails.Any(td => td.MicrochipValue == 61) && b.TransferDetails.Any(td => td.MicrochipValue == 17));
        var part1 = chip61And17Comparer.Number;

        var outputBins = GetOutputBinTransfers(bots);
        var part2 = outputBins[0] * outputBins[1] * outputBins[2];

        return (part1.ToString(), part2.ToString());
    }

    private static readonly Regex BotSpecificationRegex =
        new Regex("^bot (\\d+) gives low to (bot|output) (\\d+) and high to (bot|output) (\\d+)$", RegexOptions.Multiline);
    private static readonly Regex InputSpecificationRegex =
        new Regex("^value (\\d+) goes to bot (\\d+)$", RegexOptions.Multiline);

    public static IReadOnlyDictionary<int, int> GetOutputBinTransfers(IReadOnlyList<Bot> bots)
    {
        return bots.SelectMany(b => b.TransferDetails)
            .Where(td => td.TargetType == TransferTarget.OutputBin)
            .ToDictionary(td => td.TargetNumber, td => td.MicrochipValue);
    }

    private static void AddBotConfiguration(
        int botNumber, 
        int microchipValue, 
        Dictionary<int, int> partialConfigurations, 
        List<Bot> configuredBots, 
        IReadOnlyList<BotSpecification> botSpecifications)
    {
        if (!partialConfigurations.TryGetValue(botNumber, out int firstChipValue))
        {
            partialConfigurations.Add(botNumber, microchipValue);
        }
        else
        {
            partialConfigurations.Remove(botNumber);

            int high, low;
            if (microchipValue > firstChipValue)
            {
                high = microchipValue;
                low = firstChipValue;
            }
            else
            {
                high = firstChipValue;
                low = microchipValue;
            }

            var spec = botSpecifications.Single(b => b.BotNumber == botNumber);
            if (spec.High.Target == TransferTarget.Bot)
            {
                AddBotConfiguration(spec.High.Number, high, partialConfigurations, configuredBots, botSpecifications);
            }

            if (spec.Low.Target == TransferTarget.Bot)
            {
                AddBotConfiguration(spec.Low.Number, low, partialConfigurations, configuredBots, botSpecifications);
            }

            configuredBots.Add(new Bot(botNumber, new List<TransferDetail>(2)
            {
                new TransferDetail(low, spec.Low.Target, spec.Low.Number),
                new TransferDetail(high, spec.High.Target, spec.High.Number)
            }));
        }
    }

    public static Tuple<List<BotSpecification>, List<InputSpecification>> ParseSpecification(string specification)
    {
        specification = specification.Replace("\r", string.Empty);
        var bots = ParseBotSpecification(specification);
        var inputs = ParseInputSpecification(specification);
        return new Tuple<List<BotSpecification>, List<InputSpecification>>(bots, inputs);
    }

    private static List<BotSpecification> ParseBotSpecification(string specification)
    {
        var bots = new List<BotSpecification>();
        var matches = BotSpecificationRegex.Matches(specification);
        foreach (Match? match in matches)
        {
            if (match == null) throw new Exception();

            var botNumber = int.Parse(match.Groups[1].Value);
            var lowValueTargetType = ParseTargetType(match.Groups[2].Value);
            var lowValueTargetNumber = int.Parse(match.Groups[3].Value);
            var highValueTargetType = ParseTargetType(match.Groups[4].Value);
            var highValueTargetNumber = int.Parse(match.Groups[5].Value);

            var lowTarget = new BotTransferSpecification(lowValueTargetType, lowValueTargetNumber);
            var highTarget = new BotTransferSpecification(highValueTargetType, highValueTargetNumber);
            var bot = new BotSpecification(botNumber, lowTarget, highTarget);
            bots.Add(bot);
        }

        return bots;
    }

    private static TransferTarget ParseTargetType(string value)
    {
        if (value.Equals("bot", StringComparison.OrdinalIgnoreCase))
        {
            return TransferTarget.Bot;
        }
        else if (value.Equals("output", StringComparison.OrdinalIgnoreCase))
        {
            return TransferTarget.OutputBin;
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private static List<InputSpecification> ParseInputSpecification(string specification)
    {
        var inputs = new List<InputSpecification>();
        var matches = InputSpecificationRegex.Matches(specification);
        foreach (Match? match in matches)
        {
            if (match == null) throw new Exception();

            var microchipValue = int.Parse(match.Groups[1].Value);
            var botNumber = int.Parse(match.Groups[2].Value);

            var input = new InputSpecification(microchipValue, botNumber);
            inputs.Add(input);
        }

        return inputs;
    }

    public static IReadOnlyList<Bot> ConfigureBots(IReadOnlyList<BotSpecification> botSpecifications, IReadOnlyList<InputSpecification> inputSpecifications)
    {
        var configuredBots = new List<Bot>();
        var partialConfigurations = new Dictionary<int, int>();

        foreach (var inputSpec in inputSpecifications)
        {
            AddBotConfiguration(inputSpec.BotNumber, inputSpec.MicrochipValue, partialConfigurations, configuredBots, botSpecifications);
        }

        return configuredBots;
    }

    [DebuggerDisplay("{BotNumber} ({Low.Target} {Low.Number}, {High.Target} {High.Number})")]
    public class BotSpecification
    {
        public int BotNumber { get; }
        public BotTransferSpecification Low { get; }
        public BotTransferSpecification High { get; }

        public BotSpecification(int botNumber, BotTransferSpecification lowTarget, BotTransferSpecification highTarget)
        {
            BotNumber = botNumber;
            Low = lowTarget;
            High = highTarget;
        }
    }

    public class BotTransferSpecification
    {
        public TransferTarget Target { get; }
        public int Number { get; }

        public BotTransferSpecification(TransferTarget lowValueTargetType, int lowValueTargetNumber)
        {
            Target = lowValueTargetType;
            Number = lowValueTargetNumber;
        }
    }

    public enum TransferTarget
    {
        Bot,
        OutputBin
    }

    [DebuggerDisplay("{MicrochipValue} -> {BotNumber}")]
    public class InputSpecification
    {
        public int MicrochipValue { get; }
        public int BotNumber { get; }

        public InputSpecification(int microchipValue, int botNumber)
        {
            MicrochipValue = microchipValue;
            BotNumber = botNumber;
        }
    }

    public class Bot
    {
        public int Number { get; }
        public IReadOnlyList<TransferDetail> TransferDetails { get; }

        public Bot(int number, IReadOnlyList<TransferDetail> transferDetails)
        {
            if (transferDetails.Count != 2)
            {
                throw new ArgumentException();
            }

            Number = number;
            TransferDetails = transferDetails;
        }
    }

    public class TransferDetail
    {
        public int MicrochipValue { get; }
        public TransferTarget TargetType { get; }
        public int TargetNumber { get; }

        public TransferDetail(int microchipValue, TransferTarget targetType, int targetNumber)
        {
            MicrochipValue = microchipValue;
            TargetType = targetType;
            TargetNumber = targetNumber;
        }
    }
}
