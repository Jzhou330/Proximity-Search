/*
    Author: Jeffrey Zhou
    Date: 9/24/2020
*/

using Xunit;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public class ProximitySearchTest{

    [Fact]
    public void validateArgumentsFailureTest(){
        string keyword1 = "";
        string keyword2 = "";
        long range = 0;
        string textFile = "UnitTestFile.txt";
        List<string> wordList = new List<string>();
        string[] args = new string[]{}; 

        /*
            Negative Test Case: Check if the correct amount of arguments(4) are passed in.
            Expected Result: "ERROR: Please make sure you have entered exactly 4 arguments and try again."
        */
        args = new string[]{"the", "the", "canal", "6", textFile};
        string error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: Please make sure you have entered exactly 4 arguments and try again." , error);

        /*
            Negative Test Case: Check if the there are any special characters or numbers in the keywords.
            Expected Result: "ERROR: One of the keywords given has a special character or a number. Please provide keywords with no special characters and numbers."
        */
        args = new string[]{"the$", "canal", "6", textFile};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: One of the keywords given has a special character or a number. Please provide keywords with no special characters and numbers.", error);

        /*
            Negative Test Case: Check if the number passed as the range isn't parsable or too large. Passing in a string value as range.
            Expected Result: "ERROR: The range value given was not parsable or too large. Please enter a correct integer value for the range."
        */
        args = new string[]{"the", "canal", "number", textFile};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The range value given was not parsable or too large. Please enter a correct integer value for the range." , error);

        /*
            Negative Test Case: Check if the number passed as the range isn't parsable or too large. Passing in a value that's greate than the largest possible value for a long variable as range.
            Expected Result: "ERROR: The range value given was not parsable or too large. Please enter a correct integer value for the range."
        */
        args = new string[]{"the", "canal", "9,223,372,036,854,775,808", textFile};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The range value given was not parsable or too large. Please enter a correct integer value for the range." , error);

        /*
            Negative Test Case: Check if the range is smaller than 1
            Expected Result: "ERROR: The range value given was less than 2. Please enter an integer value of at least 2 for the range."
        */
        args = new string[]{"the", "canal", "0", textFile};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The range value given was less than 2. Please enter an integer value of at least 2 for the range.", error);

        /*
            Negative Test Case: Check if the file name give doesn't exist
            Expected Result: "ERROR: The file "noneExisting.txt" cannot be found. Please make sure the file name isn't typed incorrectly and the file is in the project folder."
        */
        args = new string[]{"the", "canal", "6", "noneExisting.txt"};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The file \"noneExisting.txt\" cannot be found. Please make sure the file name isn't typed incorrectly and the file is in the project folder.", error);

        /*
            Negative Test Case: Check if the file isn't a .txt file
            Expected Result: "ERROR: The file "test.txt1" isn't a .txt file. Please sure the file is a .txt file."
        */
        args = new string[]{"the", "canal", "6", "WrongExtensionFile.txt1"};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The file \"WrongExtensionFile.txt1\" isn't a .txt file. Please sure the file is a .txt file.", error);
        
        /*
            Negative Test Case: Check if the file is empty
            Expected Result: "ERROR: The file "EmptyTextFile.txt" isn't a .txt file. Please sure the file is a .txt file."
        */
        args = new string[]{"the", "canal", "6", "EmptyTextFile.txt"};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The file \"EmptyTextFile.txt\" is empty. Please sure the file is populated with words.", error);

        /*
            Negative Test Case: Check if there are special characters or numbers in the file
            Expected Result: "ERROR: One of the words in the file "SpecialCharactersFile.txt" has a special character or a number. Please provide keywords with no special characters and numbers."
        */
        args = new string[]{"the", "canal", "6", "SpecialCharactersFile.txt"};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: One of the words in the file \"SpecialCharactersFile.txt\" has a special character or a number. Please provide keywords with no special characters and numbers.", error);

        /*
            Negative Test Case: Check if the word count is less than 2
            Expected Result: "ERROR: The word count of the file "OneWordFile.txt" is less than 2. Please sure the file is populated with at least 2 words."
        */
        args = new string[]{"the", "canal", "6", "OneWordFile.txt"};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The word count of the file \"OneWordFile.txt\" is less than 2. Please sure the file is populated with at least 2 words.", error);

        /*
            Negative Test Case: Check if the given range is greater than the word count
            Expected Result: "ERROR: The range value given was larger than the word count in the file "UnitTestFile.txt". Please enter a range value smaller or equal to the word count in the text file."
        */
        args = new string[]{"the", "canal", "10", textFile};
        error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("ERROR: The range value given was larger than the word count in the file \"" + textFile + "\". Please enter a range value smaller or equal to the word count in the text file.", error);
    }

    [Fact]
    public static void validateArgumentsPositiveTest(){
        /*
            Positive Test Case: Testing with all valid arguments that are defined above and a range of 6 
            Expected Result: "" - Empty string means represents no error
        */
        string keyword1 = "";
        string keyword2 = "";
        List<string> wordList = new List<string>();
        long range = 0;
        string textFile = "UnitTestFile.txt";
        string[] args = new string[]{"the", "canal", "6", textFile};
        string error = ProximitySearch.validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        Assert.Equal("" , error);
    }

    [Fact]
    public void countKeywordsMatchesWithinRangeNegativeTest(){
        string keyword1 = "Hello";
        string keyword2 = "World";
        List<string> wordList = new List<string>{"Happy", "Birthday"};
        int range = 6;

        /*
            Negative Test Case: All arguments are invalid
            Expected Result: 0
        */
        long result = ProximitySearch.countKeywordsMatchesWithinRange(null, null, null, 0);
        Assert.Equal(0, result);

        /*
            Negative Test Case: Null value passed in for First Keyword argument
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(null, keyword2, wordList, range);
        Assert.Equal(0, result);

        /*
            Negative Test Case: Emtry string value passed in for Second Keyword argument
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, "", wordList, range);
        Assert.Equal(0, result);

        /*
            Negative Test Case: Null value passed in for ArrayList argument
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, null, range);
        Assert.Equal(0, result);

        /*
            Negative Test Case: ArrayList passed in is empty
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, new List<string>(), range);
        Assert.Equal(0, result);

        /*
            Negative Test Case: A negative number passed in as the Range
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, wordList, -1);
        Assert.Equal(0, result);

        /*
            Negative Test Case: Range value is greater than the word count(2) in list
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, wordList, 3);
        Assert.Equal(0, result);

        /*
            Negative Test Case: The Keywords passed in isn't in the list
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, wordList, 2);
        Assert.Equal(0, result);

        /*
            Negative Test Case: The Keywords passed in are in the list but aren't in range
            Expected Result: 0
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, new List<string>{"Hello", "Hi", "Happy", "Birthday", "World"}, 2);
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void countKeywordsMatchesWithinRangePositiveTest(){
        string keyword1 = "the";
        string keyword2 = "canal";
        List<string> wordList1 = new List<string>{"the", "man", "the", "plan", "the", "canal", "panama"};
        List<string> wordList2 = new List<string>{"the", "man", "the", "plan", "the", "canal", "panama", "panama", "canal", "the", "plan", "the", "man", "the", "the", "man", "the", "plan", "the", "canal", "panama"};
        /*
            Positive Test Case: Testing with all valid arguments that are defined above and a range of 6 
            Expected Result: 3
        */
        long result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, wordList1, 6);
        Assert.Equal(3, result);

        /*
            Positive Test Case: Testing with all valid arguments that are defined above and a range of 3
            Expected Result: 1
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, wordList1, 3);
        Assert.Equal(1, result);

        /*
            Positive Test Case: Testing with new list of that has only the first keyword and a range of 2
            Expected Result: 1
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword1, new List<string>{"the", "the"}, 2);
        Assert.Equal(1, result);

        /*
            Positive Test Case: Testing with new list of that has only the first keyword and a range of 3
            Expected Result: 3
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword1, new List<string>{"the", "the", "the"}, 3);
        Assert.Equal(3, result);

        /*
            Positive Test Case: Testing with a much bigger list and a range of 6
            Expected Result: 11
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, wordList2, 6);
        Assert.Equal(11, result);

        /*
            Positive Test Case: Testing with the keywords inputted in reverse order
            Expected Result: 11
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword2, keyword1, wordList2, 6);
        Assert.Equal(11, result);

        /*
            Positive Test Case: Testing with the list of words, all in upper case
            Expected Result: 11
        */
        List<string> upperCaseList = wordList2.Cast<string>().ToList();
        upperCaseList = upperCaseList.ConvertAll(m => m.ToUpper());
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1, keyword2, upperCaseList, 6);
        Assert.Equal(11, result);

        /*
            Positive Test Case: Testing with the keywords in upper case
            Expected Result: 11
        */
        result = ProximitySearch.countKeywordsMatchesWithinRange(keyword1.ToUpper(), keyword2.ToUpper(), wordList2, 6);
        Assert.Equal(11, result);
    }

}