using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal
{
	class Briefcase
	{
		public int Number;
		public bool Available;
		public bool IsMainBriefcase = false;
		public int Value;
	}
	class DealOrNoDeal
	{
		static List<int> Values = new List<int> { 1, 5, 10, 15, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000, 750000, 1000000 };
		static List<Briefcase> Briefcases = new List<Briefcase>();
		static int DrawsLeftTot = 7;
		static int DrawsLeft = DrawsLeftTot;
		public static void Start()
		{
			InitializeBriefcases();
			GameLoop();
		}

		private static void GameLoop()
		{
			foreach (var item in Briefcases)
			{
				Console.WriteLine(item.Number + " " + item.Value);
			}
			int winValue = 0;
			while (winValue == 0)
			{
				DrawField();
				GetUserInput();
				winValue = PhonePerson();
			}
			Console.WriteLine("You won: " + winValue);
			Console.ReadKey();
		}

		private static void SelectFirstBriefcase()
		{
			if (Briefcases.Select(x=>x.IsMainBriefcase).Max() == false)
			{
				Console.Write("Choose case: ");
				Briefcases[GetNumber()].IsMainBriefcase = true;
				DrawField();
			}
		}

		private static int PhonePerson()
		{
			DrawField();
			Console.ForegroundColor = ConsoleColor.White;
			int offer = 0;
			if (Briefcases.Count(x => x.Available) != 2)
			{
				if (DrawsLeft == 0)
				{
					if (DrawsLeftTot == 6)
						DrawsLeftTot -= 2;
					else if (DrawsLeftTot > 1)
						DrawsLeftTot--;
					DrawsLeft = DrawsLeftTot;
					offer = Briefcases.Where(x => x.Available).Sum(x => x.Value) / Briefcases.Count(x => x.Available);
					Console.Write("Offer: " + offer + "\nAccept? [y]es/[n]o: ");
					if (Console.ReadKey().KeyChar.ToString().ToLower() != "y")
						return 0;
				}
			}
			else
			{
				Console.Write("Select one of the remaining cases: ");
				offer = Briefcases[GetNumber(true)].Value;
			}
			return offer;
		}

		private static void GetUserInput()
		{
			Console.ForegroundColor = ConsoleColor.White;
			SelectFirstBriefcase();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nDraws left: " + DrawsLeft);
			Console.Write("Enter case number: ");
			int number = GetNumber();
			Briefcases[number].Available = false;
			DrawsLeft--;
		}

		private static int GetNumber(bool allowMain = false)
		{
			int value = 0;
			try
			{
				int a = int.Parse(Console.ReadLine());
				if (allowMain)
				{
					if (Briefcases[a - 1].Available)
					{
						value = a - 1;
					}
					else
					{
						value = GetNumber(allowMain);
					}
				}
				else
				{
					if (Briefcases[a-1].Available && Briefcases[a-1].IsMainBriefcase == false)
					{
						value = a-1;
					}
					else
					{
						value = GetNumber(allowMain);
					}

				}
			}
			catch
			{
				value = GetNumber(allowMain);
			}
			return value;
		}

		private static void DrawField()
		{
			Console.Clear();
			//values
			Briefcases.Sort((x, y) => x.Value.CompareTo(y.Value));
			for (int i = 0; i < Briefcases.Count(); i++)
			{
				Console.SetCursorPosition(Briefcases.Select(x => x.Value).Max().ToString().Length - Briefcases[i].Value.ToString().Length, i);
				Console.ForegroundColor = Briefcases[i].Available == true || Briefcases[i].IsMainBriefcase == true ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
				Console.WriteLine(Briefcases[i].Value + "");
				Console.ForegroundColor = Briefcases[i].Available == false ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
			}
			//numbers / IDS
			Briefcases.Sort((x, y) => x.Number.CompareTo(y.Number));
			for (int i = 0; i < Briefcases.Count(); i++)
			{
				Console.SetCursorPosition(Briefcases.Select(x => x.Value).Max().ToString().Length+5 + Briefcases.Select(x => x.Number).Max().ToString().Length- Briefcases[i].Number.ToString().Length, i);
				Console.ForegroundColor = Briefcases[i].Available == true && Briefcases[i].IsMainBriefcase == false ? ConsoleColor.Red : Briefcases[i].IsMainBriefcase == true ? ConsoleColor.Green : ConsoleColor.DarkGray;
				Console.WriteLine(Briefcases[i].Number + "");
				Console.ForegroundColor = Briefcases[i].Available == false ? ConsoleColor.Red : ConsoleColor.DarkGray;
			}
		}

		private static void InitializeBriefcases()
		{
			int count = Values.Count();
			int[] randomized = GenerateRandomizedList(count);
			for (int i = 0; i < count; i++)
				Briefcases.Add(new Briefcase() { Number = (i+1), Available = true, Value = randomized[i] });
		}

		private static int[] GenerateRandomizedList(int count)
		{
			Random random = new Random();
			int[] rngvalues = new int[count];
			for (int i = 0; i < count; i++)
			{
				int rng = random.Next() % Values.Count();
				rngvalues[i] = Values[rng];
				Values.RemoveAt(rng);
			}
			return rngvalues;
		}
	}
}
