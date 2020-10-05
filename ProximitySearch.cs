/*
    Author: Jeffrey Zhou
    Date: 9/24/2020
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;

public static class ProximitySearch{

    public static void Main(string[] args){
        string keyword1 = "";
        string keyword2 = "";
        long range = 0;
        List<string> wordList = new List<string>();

        string error = validateArguments(args, ref keyword1, ref keyword2, ref wordList, ref range);
        if(!string.IsNullOrEmpty(error)){
            Console.WriteLine(error);
            return;
        }

        //Outputting the final count
        Console.WriteLine("Result = " + countKeywordsMatchesWithinRange(keyword1, keyword2, wordList, range));
    }

    public static String validateArguments(string[] args, ref string keyword1, ref string keyword2, ref List<string> wordList, ref long range){
        /*
            Check to if the correct amount of arguments(4) are passed in
            Index 0 - First Keyword
            Index 1 - Second Keyword
            Index 2 - Range
            Index 3 - Text Filename
        */
        if(args.Length < 4 || args.Length > 4){
            return "ERROR: Please make sure you have entered exactly 4 arguments and try again.";
        }

        keyword1 = args[0];
        keyword2 = args[1];

        //Check if the keywords have special characters or numbers
        if(hasSpecialCharactersOrNumbers(keyword1) || hasSpecialCharactersOrNumbers(keyword2)){
            return "ERROR: One of the keywords given has a special character or a number. Please provide keywords with no special characters and numbers.";
        }
        
        bool isParsable = long.TryParse(args[2], out range);
        string textFile = args[3];

        //Check if the number passed as the range isn't parsable or too large
        if(!isParsable){
            return "ERROR: The range value given was not parsable or too large. Please enter a correct integer value for the range.";
        }

        //Check if the range is smaller than 1
        if(range < 2){
            return "ERROR: The range value given was less than 2. Please enter an integer value of at least 2 for the range.";
        }

        //Check if the file name give doesn't exist
        FileInfo fileInfo = new FileInfo(textFile);
        if(!fileInfo.Exists){
            return "ERROR: The file \"" + textFile + "\" cannot be found. Please make sure the file name isn't typed incorrectly and the file is in the project folder.";
        }
        
        //Check if the file isn't a .txt file
        if(!fileInfo.Extension.Equals(".txt")){
            return "ERROR: The file \"" + textFile + "\" isn't a .txt file. Please sure the file is a .txt file.";
        }

        //Check if the file is empty
        if(fileInfo.Length < 2){
            return "ERROR: The file \"" + textFile + "\" is empty. Please sure the file is populated with words.";
        }

        //Parse the file and put all the words into an ArrayList
        StreamReader file = new StreamReader(textFile);

        wordList = new List<string>(); 
        string line;  
        while((line = file.ReadLine()) != null){
            wordList.AddRange(line.Split());
        }

        //Check if there are any special characters or numbers in the text file
        if(wordList.Any(s => hasSpecialCharactersOrNumbers(s))){
            return "ERROR: One of the words in the file \"" + textFile + "\" has a special character or a number. Please provide keywords with no special characters and numbers.";
        }

        //Check if the word count is less than 2
        if(wordList.Count < 2){
            return "ERROR: The word count of the file \"" + textFile + "\" is less than 2. Please sure the file is populated with at least 2 words.";
        }

        //Check if the given range is greater than the word count
        if(range > wordList.Count){
            return "ERROR: The range value given was larger than the word count in the file \"" + textFile + "\". Please enter a range value smaller or equal to the word count in the text file.";
        }
        return "";
    }

    public static long countKeywordsMatchesWithinRange(string keyword1, string keyword2, List<string> wordList, long range){
        //Check for invalid argument values
        if(
            string.IsNullOrEmpty(keyword1)
            ||
            string.IsNullOrEmpty(keyword2)
            ||
            wordList == null
            ||
            wordList.Count == 0
            ||
            range < 2
            ||
            range > wordList.Count
        ){
            return 0;
        }

        //Convert all keywords to lower case
        keyword1 = keyword1.ToLower();
        keyword2 = keyword2.ToLower();

        //Convert all the words to lower case
        wordList = wordList.ConvertAll(m => m.ToLower());
        
        int count = 0;
        bool bothKeywordsAreTheSame = keyword1.Equals(keyword2);

        //Loop through all the words in the ArrayList
        for(int i = 0; i < wordList.Count; i++){
            string currentWord = wordList[i].ToString();
            //Checking for match(case-insensitive)
            if((currentWord.Equals(keyword1) || currentWord.Equals(keyword2))){
                //Grab the current range that inner loop to parse through. 
                //Example 1: If i = 0 and the range given is 6. The inner loop should only parse to index 5
                //Example 2: If i = 10 and the range given is 8. The inner loop should only parse to index 17
                long currEndPosition = i + range - 1;

                //If the current range is greater than the word count, set currEndPosition to the last index of the list
                if(currEndPosition >= wordList.Count){
                    currEndPosition = wordList.Count - 1;
                }

                //Parse through the word list in the given range to find the keywords
                for(int j = i + 1; j <= currEndPosition; j++){
                    /*
                        Conditions:
                        1. If both keywords are not the same and the current word is the first keyword and the found an instance of second keyword, add to count.
                        2. If both keywords are not the same and the current word is the second keyword and the found an instance of first keyword, add to count.
                        3. If both keywords are the same and there is a match of the same word found, add to count
                    */
                    string wordToMatch = wordList[j].ToString();
                    bool differentKeywordsFound = !bothKeywordsAreTheSame && ((currentWord.Equals(keyword1) && wordToMatch.Equals(keyword2)) || (currentWord.Equals(keyword2) && wordToMatch.Equals(keyword1)));
                    bool sameKeywordsFound = bothKeywordsAreTheSame && currentWord.Equals(wordToMatch);
                    if(differentKeywordsFound || sameKeywordsFound){
                        count += 1;
                    }
                }
            }
        }
        return count;
    }

    public static bool hasSpecialCharactersOrNumbers(string word){
        return word.Any(ch => !Char.IsLetter(ch) && !ch.Equals('\'')); //Exclude apostrophe
    }
}