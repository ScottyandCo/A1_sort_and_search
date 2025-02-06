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

//this entire block might be redundant
int numberOfGuesses = new Random().Next(1, 6);
int minRange = new Random().Next(1, 100);
int maxRange = new Random().Next(1, 100);
while (minRange > maxRange)
    minRange = new Random().Next(1, 100);

int randomNumberArraySize = new Random().Next(minRange, maxRange);
int[] randomNumberArray = new int[randomNumberArraySize];

// fill the randomNumberArray with random numbers
for (int index = 0; index < randomNumberArray.Length; index++)
{
    randomNumberArray[index] = new Random().Next(1, 100);
}

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