using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static string task = "Задача: Даны 5 векторов длины 4 над Z_2. Какой из этих векторов надо удалить, чтобы оставшиеся вектора образовывали векторное пространство?";
        static void Main(string[] args)
        {
            int n = 0;
            Console.WriteLine("Программа разработанна для генерирования типовых задач");
            Console.WriteLine(task);
            Console.WriteLine("Какое количество заданий сгенерировать?");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; ++i)
            {
                algo();
            }
            Console.WriteLine("Генерация заданий завершена!");
            Console.ReadLine();
        }

        static void algo()
        {
            Random rand = new Random();
            string[] vectors = new string[5];
            vectors[0] = "0000"; // Без этого, векторное подпространство никогда не сможет быть образовано
            vectors[1] = checkVector(vectors);
            vectors[2] = checkVector(vectors);
            vectors[3] = checkVector(vectors);
            if (vectors.Contains<String>(sumOfVectors(vectors[1], vectors[2])))
            {
                if (vectors.Contains<String>(sumOfVectors(vectors[2], vectors[3])))
                {
                    vectors[4] = checkVector(vectors);
                }
                else
                {
                    vectors[4] = sumOfVectors(vectors[2], vectors[3]);
                }
            }
            else
            {
                vectors[4] = sumOfVectors(vectors[1], vectors[2]);
            }


            foreach (var item in vectors)
            {
                rightAnswer(vectors, item);
            }

            foreach (var item in vectors)
            {
                Console.WriteLine(item.ToString());
            }
        }

        static string sumOfVectors(string v1, string v2)
        {
            string res = "";
            for (int i = 0; i < v1.Length; ++i)
            {
                res += sumOfEls(int.Parse(v1[i].ToString()), int.Parse(v2[i].ToString()));
            }
            return res;
        }

        static int sumOfEls(int e1, int e2)
        {
            int res = 0;
            if (e1 + e2 == 0 || e1 + e2 == 2) res = 0;
            if (e1 + e2 == 1) res = 1;
            return res;
        }

        static string generateVector(Random rand)
        {
            string v = "";
            for (int i = 0; i < 4; ++i)
            {
                if (rand.Next(0, 2) == 0)
                    v += '0';
                else
                    v += '1';

            }
            return v;
        }

        static string checkVector(string[] vectors)
        {
            Random rand = new Random();
            string vector;
            do
            {
                 vector = generateVector(rand);
            }
            while (vectors.Contains<String>(vector));
            return vector;
        }

        static void rightAnswer(string[] vectors, string vector)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine(vector);
            Console.WriteLine("-----------------");
            bool[] results = new bool[5];
            int k = 0;
            for (int i = 0; i < 5; ++i)
            {
                results[i] = vectors.Contains<String>(sumOfVectors(vector, vectors[i]));
                if (!results[i]) k++;
                Console.WriteLine((i).ToString() + ") " + results[i]);
            }
            // Если найден вектор, который можно удалить
            if (k >= 3)
            {
                string fileName = "out.txt";
                FileStream aFile = new FileStream(fileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(aFile);
                aFile.Seek(0, SeekOrigin.End);
                sw.WriteLine(task);
                // sw.WriteLine(String.Join(", ", vectors));
                string[] copy = new string[5];
                vectors.CopyTo(copy, 0);
                //copy[0] = "Никакой";
                Random rnd = new Random();
                string[] MyRandomArray = copy.OrderBy(x => rnd.Next()).ToArray();
                sw.WriteLine(String.Join(", ", MyRandomArray));


                for (int i = 0; i < MyRandomArray.Length; ++i)
                {
                    if (MyRandomArray[i] != vector)
                        sw.WriteLine((i + 1).ToString() + ") " + MyRandomArray[i]);
                    else
                        sw.WriteLine((i + 1).ToString() + ") " + MyRandomArray[i] + " *");
                }
                sw.WriteLine();
                sw.Close();
            }
            
        } 
    }
}
