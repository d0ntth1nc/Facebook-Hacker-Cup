using System;
using System.IO;

class MainClass
{
    static bool isPossible = false;

	struct Macronutrient : IEquatable<Macronutrient>
	{
		public int GP { get; set; }
		public int GC { get; set; }
		public int GF { get; set; }

		public Macronutrient(string macronutrient)
			: this()
		{
			var data = macronutrient.Split(' ');
			this.GP = int.Parse(data[0]);
			this.GC = int.Parse(data[1]);
			this.GF = int.Parse(data[2]);
		}

		public static Macronutrient operator +(Macronutrient left, Macronutrient right)
		{
			var m = new Macronutrient ();
			m.GP = left.GP + right.GP;
			m.GC = left.GC + right.GC;
			m.GF = left.GF + right.GF;
			return m;
		}

		public bool Equals (Macronutrient m)
		{
			return this.GP == m.GP && this.GC == m.GC && this.GF == m.GF;
		}
	}

	public static void Main (string[] args)
	{
		Console.Write ("Enter input path: ");
		string inputFilePath = "/home/alex/Downloads/";
		inputFilePath += Console.ReadLine();
		
		using (var file = new StreamReader (inputFilePath)) {
			using (var outputFile = new StreamWriter("/home/alex/output.txt")) {
				int casesCount = int.Parse(file.ReadLine ());
				for (int i = 1; i <= casesCount; i++) {
					var expected = new Macronutrient (file.ReadLine ());
					int eatTimes = int.Parse(file.ReadLine ());
					var macronutrients = new Macronutrient[eatTimes];
		
					for (int eatIndex = 0; eatIndex < eatTimes; eatIndex++) {
						macronutrients[eatIndex] = new Macronutrient(file.ReadLine ());
					}
		            
					isPossible = false;
					var combinations = new Macronutrient[eatTimes];
					for (int len = 1; len <= eatTimes; len++)
					{
					    GetCombinations(macronutrients, combinations, 0, 1, len, expected);
					}

					outputFile.WriteLine (String.Format("Case #{0}: {1}", i,
						isPossible ? "yes" : "no"
					));
				}
			}
		}
	}

    private static void GetCombinations(Macronutrient[] arr, Macronutrient[] combinationsArray, int currentIndex, int nextIndex, int len, Macronutrient expected)
    {
        if (currentIndex != len)
        {
            for (int i = nextIndex; i <= arr.Length; i++)
            {
                combinationsArray[currentIndex] = arr[i - 1];
                GetCombinations(arr, combinationsArray, currentIndex + 1, i + 1, len, expected);
            }
        }
        else
        {
            IsPossible(combinationsArray, len, expected);
        }
    }

    static void IsPossible(Macronutrient[] combinationsArray, int len, Macronutrient expected)
    {
        var mc = new Macronutrient();
        for (int i = 0; i < len; i++)
        {
            mc = mc + combinationsArray[i];
        }
        if (mc.Equals(expected)) {
            isPossible = true;
        }
    }
}