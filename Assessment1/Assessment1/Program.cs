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

Console.Write("How many numbers would you like to guess? ");
int numberOfGuesses = int.Parse(Console.ReadLine());

Console.Write("What would you like the lowest number to be? ");
int minRange = int.Parse(Console.ReadLine());

Console.Write("What would you like the highest number to be? ");
int maxRange = int.Parse(Console.ReadLine());

//this entire block might be redundant
/*int numberOfGuesses = new Random().Next(1, 6);
int minRange = new Random().Next(1, 3);
int maxRange = new Random().Next(1, 5);
while (minRange > maxRange)
    minRange = new Random().Next(1, 3);*/

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

int userGuess;
int[] userCorrectGuessArray = new int[numberOfGuesses];
int[] userIncorrectGuessArray = new int[numberOfGuesses];
for (int index = 0; index < numberOfGuesses; index++)
{
    Console.Write($"Enter a number (between 1 and 99) for Guess #{index + 1}: ");
    userGuess = int.Parse(Console.ReadLine());
    if (randomNumberArray.Contains(userGuess))
        userCorrectGuessArray[index] = userGuess;
    else
        userIncorrectGuessArray[index] = userGuess;
}

for (int index = 0; index < numberOfGuesses; index++)
{
    Console.WriteLine($"Guess #{index + 1} - {userCorrectGuessArray.GetValue(index)}");
}

int[] correctGuesses = new int[userCorrectGuessArray.Length];
for (int index = 0; index < userCorrectGuessArray.Length; index++)
{
    if (userCorrectGuessArray[index] != 0)
        correctGuesses[index] = userCorrectGuessArray[index];
}
Console.WriteLine("You correctly guessed {0}", string.Join(", ", correctGuesses));