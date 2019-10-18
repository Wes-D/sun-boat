using System;
using System.IO;

namespace SortMeSorter
{
    class Program
    {
        protected static void Main(string[] args)
        {
            // Output the application title
            Console.WriteLine("Welcome to the Sort Me.txt Sorter!");
            Console.WriteLine("Created By: Charles Dow");
            Console.WriteLine("Created On: 09 October 2019");
            Console.WriteLine("----------------------------------");
            Console.WriteLine(" ");

            //Ask for file path
            Console.WriteLine("Please input the full file path for 'Sort Me.txt'");
            string fileLoc = @Convert.ToString(Console.ReadLine());

            //try to pass the file location to the word sorter class.
            try
            {
                WordSorter.toArray(fileLoc);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e);
            }

            //wait for user input to close app.
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }

    class WordSorter
    {
        //This method reads the txt file and puts each word into an array, removing white spaces in the process
        public static void toArray(string fileLoc)
        {
            string[] unsortedWords = new string[lineCount(fileLoc)]; //calls lineCount to get accurate size for array.
            int index = 0;

            foreach (string name in File.ReadLines(fileLoc))
            {
                if (name.Length > 0) //Checks to ensure something is written on that line
                {
                    unsortedWords[index++] = blankTrim(name);
                }
            }

            lengthSort(unsortedWords); //sends the new array to be sorted by length
        }
        
        //Method to count number of lines with text in the Sort Me file.
        protected static int lineCount(string fileLoc)
        {
            int size = 0;

            foreach (string name in File.ReadLines(fileLoc))
            {
                if (name.Length > 0)
                {
                    size++;
                }
            }
            return size;
        }

        //method to remove whitespace from the edges of a string
        protected static string blankTrim(string name)
        {
                return name.Trim();
        }

        //selection sort method using the length of each word
        protected static void lengthSort(string[] unsortedWords)
        {
            int arrayLength = 50;
            int min;
            string tmp;

            for (int i = 0; i < arrayLength - 1; i++)
            {
                min = i;

                for (int j = i + 1; j < arrayLength; j++)
                {
                    if (unsortedWords[j].Length < unsortedWords[min].Length)
                    {
                        min = j;
                    }
                }
                tmp = unsortedWords[min];
                unsortedWords[min] = unsortedWords[i];
                unsortedWords[i] = tmp;
            }
            sameLengthFinder(unsortedWords); //sends the sorted array to be broken into similar length segments and further sorted.
        }

        //Method that will scan the array and create new, same-length arrays that it will send to sorted alphabetically.
        protected static void sameLengthFinder(string[] lengthSortedWords)
        {
            int nextIndex = 1;
            int posRef = 0;

            for (int index = 0; index < lengthSortedWords.Length-1; index++)
            {
                if (lengthSortedWords[index].Length != lengthSortedWords[nextIndex].Length)
                {
                    string[] sameLengthWords = new string[nextIndex - posRef];

                    for (int j = 0; j < sameLengthWords.Length; j++) //populates the new array
                    {
                        sameLengthWords[j] = lengthSortedWords[posRef];
                        posRef++;
                    }

                    Console.WriteLine("----------------------------------");
                    alphabeticalSort(sameLengthWords);
                    //posRef = nextIndex;
                }
                nextIndex++;
            }

            //passes the final array to be sorted and printed.
            string[] lastArray = new string[lengthSortedWords.Length - posRef];

            for (int j = 0; j < lastArray.Length; j++) //populates the new array
            {
                lastArray[j] = lengthSortedWords[posRef];
                posRef++;
            }

            Console.WriteLine("----------------------------------");
            alphabeticalSort(lastArray);
        }

        //sorts the given array alphabetically then sends it to get printed.
        protected static void alphabeticalSort(string[] sameLengthWords)
        {
            Array.Sort(sameLengthWords);
            Printer.printToConsole(sameLengthWords);
        }
    }

    class Printer
    {
        //prints each index of given array to the console on its own line.
        public static void printToConsole(string[] array)
        {
            for (int index = 0; index < array.Length; index++)
            {
                Console.WriteLine(array[index]);
            }
        }        
    }
}
