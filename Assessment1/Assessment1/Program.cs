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
Array.Sort(randomNumberArray);
int score = 0;
List<bool> resultList = [];
List<int> correctGuesses = [];

//TODO: Remove the below testing code
Console.WriteLine();
Console.WriteLine("FOR TESTING PURPOSES");
Console.WriteLine("The randomly generated numbers are {0}", string.Join(", ", randomNumberArray));
Console.WriteLine();
// remove above

int[] userGuessArray = guessingGame.TakeUserGuess();
foreach (int guess in userGuessArray)
{
    bool result = guessingGame.BinarySearch(randomNumberArray, guess);
    resultList.Add(result);
}

(score, correctGuesses) = guessingGame.ScoreUserGuesses(score, resultList, numberOfGuesses, userGuessArray, correctGuesses);
guessingGame.ProvideUserFeedback(score, numberOfGuesses, correctGuesses, userGuessArray, resultList);

internal class MainGame
{
    // Developer hard floor and ceiling
    private const int DeveloperMinRange = 0;
    private const int DeveloperMaxRange = 1000;
    // User provides the below parameters , within the range above
    private static int _numberOfGuesses;
    private static int _minRange = DeveloperMinRange;
    private static int _maxRange = DeveloperMaxRange;

    /// <summary>
    /// This method takes user inputs to define the number of guesses and the range (minimum number and maximum number)
    /// TryParse will return false, and continue to loop through the user input request, until the user input string can
    /// be parsed to an integer
    /// </summary>
    /// <returns>A single integer</returns>
    public int DefineGameParameters()
    {
        while (_numberOfGuesses <= 0 || _numberOfGuesses > DeveloperMaxRange)
        {
            Console.Write("How many guesses would you like to attempt (enter a number greater than 0)? ");
            while (!int.TryParse(Console.ReadLine(), out _numberOfGuesses))
            {
                Console.Write("How many guesses would you like to attempt (enter a nummber greater than 0)? ");
            }
        }

        while (_minRange <= DeveloperMinRange)
        {
            Console.Write($"What would you like the lowest number to be (enter a number greater than {DeveloperMinRange})? ");
            while (!int.TryParse(Console.ReadLine(), out _minRange))
            {
                Console.Write($"What would you like the lowest number to be (enter a number greater than {DeveloperMinRange})? ");
            }
        }
        
        while (_maxRange >= DeveloperMaxRange)
        {
            Console.Write($"What would you like the highest number to be (enter a number greater than {_minRange} and less than {DeveloperMaxRange})? ");
            while (!int.TryParse(Console.ReadLine(), out _maxRange))
            {
                Console.Write($"What would you like the highest number to be (enter a number greater than {_minRange} and less than {DeveloperMaxRange})? ");
            }
        }
        // this if statement ensures that there are more numbers (range) than guess attempts
        // if guesses is greater than numbers it resets all numbers to original and calls the
        // define game parameters method, essentially restarting the game
        if (_maxRange - _minRange < _numberOfGuesses)
        {
            Console.WriteLine();
            Console.WriteLine("The number of guesses exceeds the range of available numbers, enter a broader range");
            Console.WriteLine();
            _numberOfGuesses = 0;
            _minRange = DeveloperMinRange;
            _maxRange = DeveloperMaxRange;
            DefineGameParameters();
        }

        return _numberOfGuesses;
    }
    
    /// <summary>
    /// create an array of integers the length of user guess attempts
    /// then fills the array with random numbers within the defined range (users minimum and maximum numbers)
    /// the while loop ensures a number cannot be in the array multiple times
    /// </summary>
    /// <returns>An array of randomly generated integers</returns>
    public int[] GenerateRandomNumbers()
    {
        int[] randomNumberArray = new int[_numberOfGuesses];
        for (int index = 0; index < randomNumberArray.Length; index++)
        {
            int number = new Random().Next(_minRange, _maxRange + 1);
            while (randomNumberArray.Contains(number))
                number = new Random().Next(_minRange, _maxRange + 1);
            randomNumberArray[index] = number;
        }

        return randomNumberArray;
    }
    
    /// <summary>
    /// Requests and takes user input for each "user guess"
    /// Ensures that the string is a number and that the number is unique.
    /// </summary>
    /// <returns>An array of integers, all the user guesses</returns>
    public int[] TakeUserGuess() 
    {
        int[] userGuessArray = new int[_numberOfGuesses];
        List<int> previousGuessList = [];
        for (int index = 0; index < _numberOfGuesses; index++)
        {
            int userGuess = 0;
            while (userGuess < _minRange || userGuess > _maxRange)
            {
                Console.Write($"Guess #{index + 1} - Enter a number (between {_minRange} and {_maxRange}): ");
                while (!int.TryParse(Console.ReadLine(), out userGuess) || userGuessArray.Contains(userGuess))
                {
                    Console.WriteLine($"You must enter a unique number (between {_minRange} and {_maxRange}): ");
                    Console.WriteLine($"You have already guessed: {string.Join(" & ", previousGuessList)}.");
                    Console.Write($"Guess #{index + 1}: ");
                }

                userGuessArray[index] = userGuess;
                previousGuessList.Add(userGuess);
            }
        }
        return userGuessArray;
    }

    /// <summary>
    /// Provide user feedback (using WriteLine) about all user guesses
    /// </summary>
    /// <param name = "score">The user score (integer of correct guesses)</param>
    /// <param name = "numberOfGuesses">The number of guesses, as defined by the user (integer)</param>
    /// <param name = "correctGuesses">The correct guesses the user has made (List of integers)</param>
    /// <param name = "userGuessArray">All user guesses (Array of integers)</param>
    /// <param name = "resultList">List of user guesses as booleans (correct or not) (List of booleans)</param>  
    /// <returns>void</returns>
    public void ProvideUserFeedback(int score, int numberOfGuesses, List<int> correctGuesses, int[] userGuessArray, List<bool> resultList)
    {
        int index = 0;
        foreach (bool guess in resultList)
        {
            Console.WriteLine();
            Console.WriteLine($"Guess #{index + 1} - You guessed '{userGuessArray.GetValue(index)}'");
            if (guess)
            {
                Console.WriteLine("Congratulations! You guessed the correct number!");
            }
            else
            {
                Console.WriteLine("Unfortunately you got that one wrong");
            }

            index++;
        }
        Console.WriteLine();
        Console.WriteLine($"You correctly guessed {score} out of {numberOfGuesses} attempts.");
        if (score == 0)
            Console.WriteLine("You didn't guess any numbers correctly...");
        else if (score == 1)
            Console.WriteLine("Your correct guess was number {0}", string.Join(" & ", correctGuesses));
        else
            Console.WriteLine("You correctly guessed numbers {0}", string.Join(" & ", correctGuesses));
    }

    /// <summary>
    /// Compare each user guess (using the foreach function in Main) to each number within the random number array
    /// using a binary search, to look for a match
    /// The binary search works by finding the middle index, checking that index value against the users guess,
    /// altering the high/low index based on whether the user guess was higher or lower than the value at the middle index
    /// this loops until the user guess is found, or the low index is higher than the high index (meaning the user guess
    /// value was not in the random array)
    /// </summary>
    /// <param name = "randomNumberArray">The randomly generated array of integers</param>
    /// <param name = "userGuess">the user guess (integer)</param>
    /// <returns>True if found, False if not found (boolean)</returns>
    public bool BinarySearch(int[] randomNumberArray, int userGuess)
    {
        int highestIndex = randomNumberArray.Length - 1;
        int lowestIndex = 0;
        bool result = false;
        while (lowestIndex <= highestIndex)
        {
            int middleIndex = (highestIndex + lowestIndex) / 2;
            
            if (randomNumberArray[middleIndex] == userGuess)
            {
                result = true;
                return result;
            }

            if (randomNumberArray[middleIndex] < userGuess)
            {
                lowestIndex = middleIndex + 1;
            }

            else if (randomNumberArray[middleIndex] > userGuess)
            {
                highestIndex = middleIndex - 1;
            }
        }
        return result;
    }
    
    /// <summary>
    /// Provide user feedback (using WriteLine) based on the booleans within the result list
    /// adds each correct guess (integer) to a list for use in provide user feedback method
    /// increases score for each correct guess
    /// </summary>
    /// <param name = "score">The user score / number of correct guesses (integer)</param>
    /// <param name = "resultList">booleans based on correct or incorrect user guesses(List of booleans)</param>
    /// <param name = "numberOfGuesses">The number of guesses, as defined by the user (integer)</param>
    /// <param name = "userGuessArray">All guesses the user has made (Array of integers)</param>
    /// <param name = "correctGuesses">The correct guesses the user has made (List of integers)</param> 
    /// <returns>user score (integer), users correct guesses (List of integers)</returns>
    public (int, List<int>) ScoreUserGuesses(int score, List<bool> resultList, int numberOfGuesses, int[] userGuessArray, List<int> correctGuesses)
    {
        for (int index = 0; index < numberOfGuesses; index++)
        {
            if (resultList[index])
            {
                correctGuesses.Add(Convert.ToInt32(userGuessArray.GetValue(index)));
                score++;
            }
        }

        return (score, correctGuesses);
    }
}

