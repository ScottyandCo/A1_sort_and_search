/*
 * The application must:
 *  1. Take user input to store values they select into an array structure. - COMPLETE
 *  2. Write a custom Linear and Binary Search and use the Binary Search to find the matching values in the array.
 *  3. Only allow values within a predefined range.
 *  4. Handle any input validation gracefully, while informing user if incorrect.
 *  5. Where necessary display outputs to user for prompt and feedback. - COMPLETE
 *  6. Allow for customisation of the application by ensuring the following values are variables:
 *      a. The total number of values that the user must enter / are generated randomly.
 *      b. The range of numbers that are accepted for input / to be selected from randomly.
 */
//TODO: confirm 6a and 6b are supposed to be randomly generated, or they must be able to be changed by the user?
//TODO: add input validation, error handling and define strict value handling

using System.ComponentModel.Design;

int numberOfGuesses = 0;
int developerMinRange = 0;
int minRange = developerMinRange;
int developerMaxRange = 1000;
int maxRange = developerMaxRange;

while (numberOfGuesses <= 0) { 
    Console.Write("How many guesses would you like to attempt (greater than 0)? "); 
    while (!int.TryParse(Console.ReadLine(), out numberOfGuesses)) 
    { 
        Console.Write("How many numbers would you like to guess (greater than 0)? "); 
    } 
}

while (minRange <= developerMinRange)
{
    Console.Write($"What would you like the lowest number to be (greater than {developerMinRange})? ");
    while (!int.TryParse(Console.ReadLine(), out minRange))
    {
        Console.Write($"What would you like the lowest number to be (greater than {developerMinRange})? ");
    }
}

// Determine the maximum number available in the random number array
// Continue to ask the user for another number if it is above the hard maximum
while (maxRange >= developerMaxRange)
{
    Console.Write($"What would you like the highest number to be (less than {developerMaxRange})? ");
    while (!int.TryParse(Console.ReadLine(), out maxRange))
    {
        Console.Write($"What would you like the highest number to be (less than {developerMaxRange})? ");
    }
}

//if (maxRange - minRange > numberOfGuesses)
    //re-run start program function

int[] randomNumberArray = new int[numberOfGuesses];
int number;
// fill the randomNumberArray with random numbers
for (int index = 0; index < randomNumberArray.Length; index++)
{
    number = new Random().Next(minRange, maxRange + 1);
    while (randomNumberArray.Contains(number))
        number = new Random().Next(minRange, maxRange + 1);
    randomNumberArray[index] = number;
}

Console.WriteLine("Random Numbers {0}", string.Join(", ", randomNumberArray));

int[] userGuessArray = new int[numberOfGuesses];
for (int index = 0; index < numberOfGuesses; index++)
{
    int userGuess = 0;
    while (userGuess < minRange || userGuess > maxRange)
    {
        Console.Write($"Enter a number (between {minRange} and {maxRange}) for Guess #{index + 1}: ");
        userGuess = int.Parse(Console.ReadLine());
        userGuessArray[index] = userGuess;
    }
}
Console.WriteLine();
int score = 0;
List<int> correctGuesses = new List<int>();
for (int index = 0; index < numberOfGuesses; index++)
{
    Console.WriteLine($"Guess #{index + 1} - You guessed '{userGuessArray.GetValue(index)}' and the correct number was '{randomNumberArray.GetValue(index)}'");
    if (Convert.ToInt32(randomNumberArray.GetValue(index)) == Convert.ToInt32(userGuessArray.GetValue(index)))
    {
        Console.WriteLine("Congratulations! You guessed the correct number!");
        Console.WriteLine();
        correctGuesses.Add(userGuessArray[index]);
        score += 1;
    }
    else
    {
        Console.WriteLine("Unfortunately you got that one wrong");
        Console.WriteLine();
    }
}

Console.WriteLine($"You correctly guessed {score} out of {numberOfGuesses}");
Console.WriteLine("Your correct guesses were numbers {0}", string.Join(" & ", correctGuesses));

/*Prog.Begin();

class Prog
{
    private int a = 2;
    private int b = 1;
    private int c = 5;

    public static void Begin()
    {
        Prog Start = new Prog();
        Console.WriteLine($"Program Started {Start.a}");
    }
}*/

