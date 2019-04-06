using System;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class Program
    {

        static void printMatrix(string[] matrix, int n)
        {
            for (int i = 0; i < n; i++)
                Console.WriteLine(matrix[i]);
        }

        static string[] checkHorizontal(int x, int y, string[] matrix, string currentWord)
        {
            int n = currentWord.Length;

            for (int i = 0; i < n; i++)
            {
                if (matrix[x][y + i] == '-' || matrix[x][y + i] == currentWord[i])
                {
                    var hChar = matrix[x].ToCharArray();
                    hChar[y + i] = currentWord[i];
                    matrix[x] = string.Join(string.Empty, hChar);
                }
                else
                {

                    var hChar = matrix[0].ToCharArray();
                    hChar[0] = '@';
                    matrix[0] = string.Join(string.Empty, hChar);
                    return matrix;
                }
            }

            return matrix;
        }

        static string[] checkVertical(int x, int y, string[] matrix, string currentWord)
        {
            int n = currentWord.Length;

            for (int i = 0; i < n; i++)
            {
                if (matrix[x + i][y] == '-' || matrix[x + i][y] == currentWord[i])
                {
                    var vChar = matrix[x + i].ToCharArray();
                    vChar[y] = currentWord[i];
                    matrix[x + i] = string.Join(string.Empty, vChar);
                }
                else
                {
                    var vChar = matrix[0].ToCharArray();
                    vChar[0] = '@';
                    matrix[0] = string.Join(string.Empty, vChar);
                    return matrix;
                }
            }

            return matrix;
        }

        static void solvePuzzle(string[] words, string[] matrix, int index, int n)
        {
            if (index < words.Length)
            {
                string currentWord = words[index];
                int maxLen = n - currentWord.Length;

                for (int i = 0; i < n; i++)
                {
                    int count = 0;
                    for (int c = 0; c < matrix.Length; c++)
                    {
                        if (matrix[c][i] == '-')
                            count++;

                        if (count > 0 && c + 1 < matrix.Length - 1 && matrix[c + 1][i] == '+')
                        {
                            if (count - 1 == c)
                                count++;
                            else
                                count--;
                        }
                    }

                    if (count == currentWord.Length)
                    {
                        string[] temp = null;
                        for (int j = 0; j <= maxLen; j++)
                        {
                            temp = checkVertical(j, i, matrix, currentWord);
                        }

                        solvePuzzle(words, temp, index + 1, n);
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    int count = 0;
                    for (int c = 0; c < matrix.Length; c++)
                    {
                        if (matrix[i][c] == '-')
                            count++;
                        else if (count > 0 && c + 1 < matrix.Length - 1 && matrix[i][c + 1] == '-')
                            count++;

                        if (count > 0 && c + 1 < matrix.Length - 1 && matrix[i][c + 1] == '+')
                            count--;
                    }

                    if (count == currentWord.Length)
                    {
                        string[] temp = null;

                        for (int j = 0; j <= maxLen; j++)
                        {
                            temp = checkHorizontal(i, j, matrix, currentWord);
                        }

                        solvePuzzle(words, temp, index + 1, n);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int n1 = 10;
            string[] matrix = new string[n1];

            matrix[0] = "+-++++++-+";
            matrix[1] = "+-++++++-+";
            matrix[2] = "+-++++-+-+";
            matrix[3] = "+-++++-+-+";
            matrix[4] = "+-++++-+-+";
            matrix[5] = "+-++++-+-+";
            matrix[6] = "+-++++-+++";
            matrix[7] = "+-+------+";
            matrix[8] = "+-++++++++";
            matrix[9] = "+++-------";

            string[] words = new string[5];

            words[0] = "PUNJAB";
            words[1] = "JHARKHAND";
            words[2] = "MIZORAM";
            words[3] = "MUMBAI";
            words[4] = "ANKARA";


            solvePuzzle(words, matrix, 0, n1);
            printMatrix(matrix, n1);

            Console.ReadLine();
        }
    }
}
