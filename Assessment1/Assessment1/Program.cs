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
 
Console.Write("Enter your first guess (a number from 1 to 99): ");
string userGuess1 = Console.ReadLine();

Console.Write("Enter your second guess (a number from 1 to 99): ");
string userGuess2 = Console.ReadLine();

Console.Write("Enter your third guess (a number from 1 to 99): ");
string userGuess3 = Console.ReadLine();

int[] userGuessArray = new int[3];
int userInt1 = int.Parse(userGuess1);
int userInt2 = int.Parse(userGuess2);
int userInt3 = int.Parse(userGuess3);
userGuessArray[0] = userInt1;
userGuessArray[1] = userInt2;
userGuessArray[2] = userInt3;

int index = 0;
foreach (int value in userGuessArray)
    index = Array.IndexOf(userGuessArray, value);
Console.WriteLine($"Guess #{index + 1} - {userGuessArray.GetValue(index)}");