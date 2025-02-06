/*
 * The application must:
 *  1. Take user input to store values they select into an array structure.
 *  2. Write a custom Linear and Binary Search and use the Binary Search to find the matching values in the array.
 *  3. Only allow values within a predefined range.
 *  4. Handle any input validation gracefully, while informing user if incorrect.
 *  5. Where necessary display outputs to user for prompt and feedback.
 *  6. Allow for customisation of the application by ensuring the following values are variables:
 *      a. The total number of values that the user must enter / are generated randomly.
 *      b. The range of numbers that are accepted for input / to be selected from randomly.
 */

//TODO: change from user input to random int
Console.Write("How many guesses would you like? ");
int numberOfGuesses = int.Parse(Console.ReadLine());

int[] userGuessArray = new int[numberOfGuesses];
for (int index = 0; index < numberOfGuesses; index++)
{
    Console.Write($"Enter a number (between 1 and 99) for Guess #{index + 1}: ");
    userGuessArray[index] = int.Parse(Console.ReadLine());
}

for (int index = 0; index < numberOfGuesses; index++)
{
    Console.WriteLine($"Guess #{index + 1} - {userGuessArray.GetValue(index)}");
}