Author: Jeffrey Zhou
Date: 9/24/2020

Coding Exercise:
Design and develop a solution to proximity search. Given a sample document (text file),
two keywords and a numeric range, return the number of times the keywords exist in
the document within the given range, or 0 if your search is not successful. The
keywords themselves are considered to be part of the range. This makes 2 the
minimum valid range for keyword proximity which means there are 2 keywords next to
each other. For the sake of simplicity, assume all words are separated with whitespace.

The solution to this problem was writtin in C# with Visual Studio Code. All the logic is in ProximitySearch.cs.
To build the program, you would have to run this command in the Visual Studio Code Terminal: dotnet build.
To run the program, you would run a command in this format: <dotnet run> <first keyword> <second keyword> <range> <text file>.
Running previous command will execute the Main() method in ProximitySearch.cs.
For example: dotnet run the canal 6 test1.text.
In this project, I have included text files that I have used for functional testing are test1.txt, test2.txt, test3.txt, EmptyFile.txt, OneWordFile.txt, and WrongExtensionFile.txt1
If ran successfully, it will output "Result = {x}" where x is the count of matches.  

Methods used in ProximitySearch.Main():
1. validateArguments()
   This method validates all the arguments passed in by the user.
   If all arguments are valid, it will call countKeywordsMatchesWithinRange().
   If one argument is invalid, it will output an error messgae.
2. countKeywordsMatchesWithinRange()
   This method performs the logic to find the keyword matches.
   If matches are found, a count will be calculated and returned back to be displated.
   If no matches are found, then it will return 0.
3. hasSpecialCharactersOrNumbers()
   This is utility method that takes in a string and parse it to check if any character is a special character or a number.
   This is used in validateArguments() to check keywords and in countKeywordsMatchesWithinRange() to check the words of the text file.

Argument Rules:
1. There must be exactly 4 arguments in the given command
2. The keywords must not contain any special characters or numbers.
3. The range value must be parasable(i.e., a number) and not larger than the max of a long value(i.e., 9,223,372,036,854,775,807).
4. The range value must be at least 2.
5. The text file must exist in the project folder.
6. The text file must end with ".txt" extension
7. The text file must not be empty.
8. The text file must not contain any special characters or numbers.
9. The text file must contain at least 2 words.
10. The range value must be larger than the count of words in the list.

Algorithm:
To find how many times both of the keywords occur in the file we would have to scan through the entire file.
For each word that we are currently on during the scan, we check if the current word matches the first keyword or the second keyword.
If there's the first keyword or the second keyword is found, scan through the list within the given range to find the other keyword.
A match is the event when both first keyword is found and the second keyword, and vice versa.
We keep a count of all matches throught out the algorithm.
If the keywords are different and the current word matches the first keyword then look for the second keyword.
If there is a match within the given range, add 1 to the count.
If the keywords are different and the current word matches the seond keyword then look for the first keywords.
If there is a match within the given range, add 1 to the count.
If the keywords are the same, then we would just need to find an instance of the same word as the current word.
If there is a match within the given range, add 1 to the count.
After the second scan is done, repeat the first entire process for each words in the file until no more words in the file.
Return the final count when the algorithm finishes, if there any matches found.
Else, return 0 if no matches are found.

Complexity:
The worst case time complexity of this algorithm is O(n^2) and the best case time complexity is O(1).

Unit Test:
For testing, I used the xUnit framework.
All the unit tests will be in ProximitySearchTest.cs. To run all the tests, you would have to click on "Run All Test" above the ProximitySearchTest Class declaration.
To run test for each test methods, you would have to click on "Run Test" above a test method declaration.
If ran successfully you will see a Test Execution Summary of which classes passed and which ones failed and total resuls:
Total tests: {x}. Passed: {x}. Failed: {x}. Skipped: {x}
in ProximitySearchTest.cs, I have created negative test cases and positive test cases for each method in ProximitySearch.cs: countKeywordsMatchesWithinRangeNegativeTest(),countKeywordsMatchesWithinRangePositiveTest(), validateArgumentsFailureTest(), countKeywordsWithinRangePositiveTest(). 
I tested with text files that I created as part of the project. The files are located under /bin/Debug/netcoreapp3.1/. The files must be in this directory in order for the test method to recognize that they exist.
The files used for unit testing are:
1. EmptyTextFile.txt - Testing an empty text file
2. OneWordFile.txt - Testing a text file with only one word
3. SpecialCharactersFile.txt - Testing a text file with a special character in it
4. UnitTestFile.txt - The happy path text file
5. WrongExtensionFile.txt1 - Testing a file that isn't a .txt file