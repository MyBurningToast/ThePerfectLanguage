using System.Text;

namespace ThePerfectLanguage
{
	internal class Program
	{
		private static int ropeSize;
		private static int pointerIdx;

		private static char[] rope = null!;

		private const string ROPE_KEYWORD = "ROPE";
		private const string PULL_KEYWORD = "PULL";
		private const string PUSH_KEYWORD = "PUSH";
		private const string GIVEUP_KEYWORD = "GIVEUP";


		private static void Main(string[] args)
		{
			var instructions = ParseCode();
			Execute(instructions);
		}

		private static void Execute(List<(string operation, string? argument, int srcLineNumber)> instructions)
		{
			foreach (var (op, arg, srcLineNumber) in instructions)
			{
				switch (op)
				{
					case ROPE_KEYWORD:
						if (!int.TryParse(arg!, out ropeSize) || ropeSize < 0)
							Error($"Invalid rope size: {arg}", srcLineNumber);

						rope = new char[ropeSize];
						Array.Fill(rope, ' ');
						pointerIdx = ropeSize / 2;
						break;

					case PULL_KEYWORD:
						PullPointer(GetSide(arg, srcLineNumber));
						break;

					case PUSH_KEYWORD:
						PushChar(arg![0]);
						break;

					case GIVEUP_KEYWORD:
						GiveUp(GetSide(arg, srcLineNumber));
						break;
				}
			}

			static Side GetSide(string? arg, int srcLineNumber)
			{
				if (arg != null)
				{
					if (arg == "L")
						return Side.Left;
					else if (arg == "R")
						return Side.Right;
					else
						Error($"Invalid arg: {arg}", srcLineNumber);
				}
				else
				{
					Error("Arg is null", srcLineNumber);
				}

				return Side.Null;
			}
		}

		private static void Error(string msg, int line)
		{
			Console.Error.WriteLine($"Error on line {line} [{msg}]");
			Environment.Exit(1);
		}

		private static List<(string operation, string? argument, int srcLineNumber)> ParseCode()
		{
			string filePath = Path.Combine(AppContext.BaseDirectory, "code.txt");
			string[] lines = File.ReadAllLines(filePath);

			var instructions = new List<(string operation, string? argument, int srcLineNumber)>();

			int lineNum = 1;
			foreach(string rawLine in lines)
			{
				string line = rawLine.Trim();
				if (line.Length == 0)
				{
					lineNum++;
					continue;
				}

				string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				string op = parts[0];
				string? arg = parts.Length > 1 ? parts[1] : null;

				instructions.Add((op, arg, lineNum));
				lineNum++;
			}

			return instructions;
		}

		private static void PushChar(char c) => rope[pointerIdx] = c;

		private static void PullPointer(Side side, int amount = 1)
		{
			switch (side)
			{
				case Side.Left:
					pointerIdx += amount;
					break;
				case Side.Right:
					pointerIdx -= amount;
					break;
			}

			if (pointerIdx < 0 || pointerIdx >= rope.Length)
			{
				if (pointerIdx < 0)
					EndGame($"Right side wins! (pulled to far, pointer at index {pointerIdx})");
				else
					EndGame($"Left side wins! (pulled to far, pointer at index {pointerIdx})");
			}
		}

		private static void EndGame(string output)
		{
			if (rope.Length == 0)
			{
				Console.WriteLine("You forgot to bring the rope lol, i guess its a tie");
			}
			else
			{
				Console.WriteLine($"{output}");
			}

			Environment.Exit(0);
		}

		private static void GiveUp(Side side)
		{
			StringBuilder sb = new StringBuilder(rope.Length);

			switch (side)
			{
				case Side.Left:

					for (int i = pointerIdx; i < rope.Length; i++)
					{
						sb.Append(rope[i]);
					}

					break;

				case Side.Right:

					for (int i = pointerIdx; i >= 0; i--)
					{
						sb.Append(rope[i]);
					}

					break;
			}

			EndGame(sb.ToString());
		}

		//NOTE: for debugging use
		private static void PrintRope()
		{
			int i = 0;
			foreach (var c in rope)
			{
				if (pointerIdx == i)
				{
					Console.WriteLine($"{i}: {c}    <---- POINTER HERE");
				}
				else
				{
					Console.WriteLine($"{i}: {c}");
				}
				i++;
			}
			Console.WriteLine();
		}

		private enum Side
		{
			Null,
			Left,
			Right
		}
	}
}
