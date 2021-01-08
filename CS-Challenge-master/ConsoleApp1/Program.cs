using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JokeGenerator;
using Newtonsoft.Json;

namespace JokeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Categories categories = Categories.GetCategories();
                string input = string.Empty;

                while (true)
                {
                    string category = string.Empty;
                    Name name = null;
                    int numJokes = default(int);

                    Console.WriteLine("Press 'c' to get categories");
                    Console.WriteLine("Press 'r' to get random jokes");
                    Console.WriteLine("Press 'q' to quit");
                    Console.Write(">");
                    input = ReadLine();
                    if (input == "c")
                    {
                        foreach (KeyValuePair<int, string> c in categories.categories)
                        {
                            Console.WriteLine($"{c.Key}: {c.Value}");
                        }
                    }
                    //Error checking and retrieving random names
                    else if (input == "r")
                    {
                        do
                        {
                            Console.Write("Want to use a random name? (y/n)>");
                            input = ReadLine();
                            //Error check if input is valid
                            if (input == "y")
                            {
                                name = Name.GetName();
                            }
                            else if (input != "n")
                            {
                                Console.WriteLine("Invalid input, please enter y/n");
                            }
                        } while (input != "y" && input != "n");

                        do
                        {
                            Console.Write("Want to specify a category? (y/n)>");
                            input = ReadLine();
                            //Error checking and retrieing the category
                            if (input == "y")
                            {
                                while (string.IsNullOrEmpty(category))
                                {
                                    Console.Write("Enter a category or category number>");
                                    input = ReadLine();

                                    //check if the category that the user inputted is valid
                                    int index;
                                    if (string.IsNullOrEmpty(input))
                                    {                              
                                        Console.WriteLine("No category was given");
                                    }
                                    //If the input is a number and a key in the dictionary we get the value category associated with
                                    else if (int.TryParse(input, out index) && categories.categories.ContainsKey(index))
                                    {
                                        category = categories.categories[index];
                                    }
                                    else if (categories.categories.ContainsValue(input))
                                    {
                                        category = input;
                                    }
                                    else
                                    {                                      
                                        Console.WriteLine("Invalid category given");
                                    }
                                }
                            }
                            else if (input != "n")
                            {
                                Console.WriteLine("Invalid input, please enter y/n");
                            }
                        } while (input != "y" && input != "n" && string.IsNullOrEmpty(category));

                        //Prints the jokes and takes into account a random name or category filter if selected previously
                        bool validNumJoke = false;
                        do
                        {
                            //Check if input is not a number
                            Console.Write("How many jokes do you want? (1-9)>");
                            if (!int.TryParse(ReadLine(), out numJokes))
                            {
                                Console.WriteLine("Invalid input given, please input a number");
                            }
                            //Check if input is outside of the number range
                            else if (numJokes < 1 || numJokes > 9)
                            {
                                Console.WriteLine("Number of jokes given is out of range. Please input a number between 1 - 9");
                            }
                            //Input is valid and we print the joke
                            else
                            {
                                validNumJoke = true;
                                for (int x = 0; x < numJokes; x++)
                                {
                                    Joke joke = Joke.GetRandom(category, name);
                                    Console.WriteLine(joke.joke + Environment.NewLine);
                                }
                            }
                        } while (!validNumJoke);
                    }
                    // quit the application
                    else if (input == "q") 
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured, close the console and restart");
                Thread.Sleep(Timeout.Infinite);
            }
        }

        public static string ReadLine()
        {
            return Console.ReadLine().ToLower();
        }
    }
}
