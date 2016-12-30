using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day10
    {
        private readonly ITestOutputHelper _output;

        private static readonly Regex BotSpecificationRegex = 
            new Regex("^bot (\\d+) gives low to (bot|output) (\\d+) and high to (bot|output) (\\d+)$", RegexOptions.Multiline);
        private static readonly Regex InputSpecificationRegex =
            new Regex("^value (\\d+) goes to bot (\\d+)$", RegexOptions.Multiline);

        public Day10(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Sample()
        {
            string input = "value 5 goes to bot 2\r\n" +
                "bot 2 gives low to bot 1 and high to bot 0\r\n" +
                "value 3 goes to bot 1\r\n" +
                "bot 1 gives low to output 1 and high to bot 0\r\n" +
                "bot 0 gives low to output 2 and high to output 0\r\n" +
                "value 2 goes to bot 2";

            var specification = ParseSpecification(input);
            var botsList = ConfigureBots(specification.Item1, specification.Item2);

            var outputBins = GetOutputBinTransfers(botsList);

            Assert.True(outputBins.ContainsKey(0));
            Assert.Equal(5, outputBins[0]);
            Assert.True(outputBins.ContainsKey(1));
            Assert.Equal(2, outputBins[1]);
            Assert.True(outputBins.ContainsKey(2));
            Assert.Equal(3, outputBins[2]);

            var bot2 = botsList.Single(b => b.Number == 2);
            Assert.True(bot2.TransferDetails.Any(td => td.MicrochipValue == 5) && bot2.TransferDetails.Any(td => td.MicrochipValue == 2));
        }

        [Fact]
        public void Parts1And2()
        {
            var input = GetPuzzleInput.DayText(10);
            var specification = ParseSpecification(input);
            var bots = ConfigureBots(specification.Item1, specification.Item2);

            var chip61And17Comparer = bots.Single(b => b.TransferDetails.Any(td => td.MicrochipValue == 61) && b.TransferDetails.Any(td => td.MicrochipValue == 17));
            _output.WriteLine("Part 1: {0}", chip61And17Comparer.Number);

            var outputBins = GetOutputBinTransfers(bots);
            _output.WriteLine("Part 2: {0}", outputBins[0] * outputBins[1] * outputBins[2]);
        }

        private IReadOnlyDictionary<int, int> GetOutputBinTransfers(IReadOnlyList<Bot> bots)
        {
            return bots.SelectMany(b => b.TransferDetails)
                .Where(td => td.TargetType == TransferTarget.OutputBin)
                .ToDictionary(td => td.TargetNumber, td => td.MicrochipValue);
        }

        private void AddBotConfiguration(int botNumber, int microchipValue, Dictionary<int, int> partialConfigurations, List<Bot> configuredBots, IReadOnlyList<BotSpecification> botSpecifications)
        {
            int firstChipValue;
            if (!partialConfigurations.TryGetValue(botNumber, out firstChipValue))
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

        private Tuple<List<BotSpecification>, List<InputSpecification>> ParseSpecification(string specification)
        {
            specification = specification.Replace("\r", string.Empty);
            var bots = ParseBotSpecification(specification);
            var inputs = ParseInputSpecification(specification);
            return new Tuple<List<BotSpecification>, List<InputSpecification>>(bots, inputs);
        }

        private List<BotSpecification> ParseBotSpecification(string specification)
        {
            var bots = new List<BotSpecification>();
            var matches = BotSpecificationRegex.Matches(specification);
            foreach (Match match in matches)
            {
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

        private TransferTarget ParseTargetType(string value)
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

        private List<InputSpecification> ParseInputSpecification(string specification)
        {
            var inputs = new List<InputSpecification>();
            var matches = InputSpecificationRegex.Matches(specification);
            foreach (Match match in matches)
            {
                var microchipValue = int.Parse(match.Groups[1].Value);
                var botNumber = int.Parse(match.Groups[2].Value);

                var input = new InputSpecification(microchipValue, botNumber);
                inputs.Add(input);
            }

            return inputs;
        }

        private IReadOnlyList<Bot> ConfigureBots(IReadOnlyList<BotSpecification> botSpecifications, IReadOnlyList<InputSpecification> inputSpecifications)
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
        private class BotSpecification
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

        private class BotTransferSpecification
        {
            public TransferTarget Target { get; }
            public int Number { get; }

            public BotTransferSpecification(TransferTarget lowValueTargetType, int lowValueTargetNumber)
            {
                Target = lowValueTargetType;
                Number = lowValueTargetNumber;
            }
        }

        private enum TransferTarget
        {
            Bot,
            OutputBin
        }

        [DebuggerDisplay("{MicrochipValue} -> {BotNumber}")]
        private class InputSpecification
        {
            public int MicrochipValue { get; }
            public int BotNumber { get; }

            public InputSpecification(int microchipValue, int botNumber)
            {
                MicrochipValue = microchipValue;
                BotNumber = botNumber;
            }
        }

        private class Bot
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

        private class TransferDetail
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
}
