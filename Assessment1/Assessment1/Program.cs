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

MainGame guessingGame = new MainGame();
int numberOfGuesses = guessingGame.DefineGameParameters();
int[] randomNumberArray = guessingGame.GenerateRandomNumbers();

//TODO: Remove the below testing code
Console.WriteLine();
Console.WriteLine("FOR TESTING PURPOSES");
Console.WriteLine("Random Numbers {0}", string.Join(", ", randomNumberArray));
Console.WriteLine();
// remove above

int[] userGuessArray = guessingGame.TakeUserGuess();
(int score, List<int> correctGuesses) = guessingGame.ScoreUserGuesses(userGuessArray, numberOfGuesses, randomNumberArray);
guessingGame.ProvideUserFeedback(score, numberOfGuesses, correctGuesses);

internal class MainGame
{
    // Developer hard floor and ceiling
    private const int DeveloperMinRange = 0;
    private const int DeveloperMaxRange = 1000;
    // User provides the below, within the range above
    private static int _numberOfGuesses;
    private static int _minRange = DeveloperMinRange;
    private static int _maxRange = DeveloperMaxRange;
    
    public int DefineGameParameters()
    {
        while (_numberOfGuesses <= 0 || _numberOfGuesses > DeveloperMaxRange) { 
            Console.Write("How many guesses would you like to attempt (greater than 0)? "); 
            while (!int.TryParse(Console.ReadLine(), out _numberOfGuesses)) 
            { 
                Console.Write("How many guesses would you like to attempt (greater than 0)? "); 
            }
        }

        while (_minRange <= DeveloperMinRange)
        {
            Console.Write($"What would you like the lowest number to be (greater than {DeveloperMinRange})? ");
            while (!int.TryParse(Console.ReadLine(), out _minRange))
            {
                Console.Write($"What would you like the lowest number to be (greater than {DeveloperMinRange})? ");
            }
        }

        // Determine the maximum number available in the random number array
        // Continue to ask the user for another number if it is above the developers maximum
        while (_maxRange >= DeveloperMaxRange)
        {
            Console.Write($"What would you like the highest number to be (less than {DeveloperMaxRange})? ");
            while (!int.TryParse(Console.ReadLine(), out _maxRange))
            {
                Console.Write($"What would you like the highest number to be (less than {DeveloperMaxRange})? ");
            }
        }

        if (_maxRange - _minRange < _numberOfGuesses)
        {
            Console.WriteLine();
            Console.WriteLine("The number of guesses exceeds the range of available numbers, enter a broader range");
            Console.WriteLine();
            _numberOfGuesses = 0;
            _minRange = DeveloperMinRange;
            _maxRange = DeveloperMaxRange;
            DefineGameParameters();
        } return _numberOfGuesses;
    }

    public int[] GenerateRandomNumbers()
    {
        int[] randomNumberArray = new int[_numberOfGuesses];
        // fill the randomNumberArray with random numbers
        for (int index = 0; index < randomNumberArray.Length; index++)
        {
            int number = new Random().Next(_minRange, _maxRange + 1);
            while (randomNumberArray.Contains(number))
                number = new Random().Next(_minRange, _maxRange + 1);
            randomNumberArray[index] = number;
        }
        return randomNumberArray;
    }

    public int[] TakeUserGuess()
    {
        int[] userGuessArray = new int[_numberOfGuesses];
        for (int index = 0; index < _numberOfGuesses; index++)
        {
            int userGuess = 0;
            while (userGuess < _minRange || userGuess > _maxRange)
            {
                Console.Write($"Guess #{index + 1} - Enter a number (between {_minRange} and {_maxRange}): ");
                while (!int.TryParse(Console.ReadLine(), out userGuess)) 
                { 
                    Console.Write($"Guess #{index + 1} - Enter a number (between {_minRange} and {_maxRange}): "); 
                }
                userGuessArray[index] = userGuess;
            }
        }
        Console.WriteLine();
        return userGuessArray;
    } 

    public (int, List<int>) ScoreUserGuesses(int[] userGuessArray, int numberOfGuesses, int[] randomNumberArray)
    {
        int score = 0;
        List<int> correctGuesses = [];
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
        return (score, correctGuesses);
    }

    public void ProvideUserFeedback(int score, int numberOfGuesses, List<int> correctGuesses)
    {
        Console.WriteLine($"You correctly guessed {score} numbers out of {numberOfGuesses} attempts.");
        if (score == 0)
            Console.WriteLine("You didn't guess any numbers correctly...");
        else if (score == 1)
            Console.WriteLine("Your correct guess was number {0}", string.Join(" & ", correctGuesses));
        else
            Console.WriteLine("You correctly guessed numbers {0}", string.Join(" & ", correctGuesses));
    }
}

