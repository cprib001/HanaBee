using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter
{
    public string romaji;
    public bool isHolding;

    public Letter(string romaji,
                  bool isHolding = false)
    {
        this.romaji = romaji;
        this.isHolding = isHolding;
    }
}

public class Word
{
    public string nameRomaji;
    public string nameEnglish;
    public List<Letter> nameHiragana;
    public string description;
    public int wordSize;
    public bool markedForDeletion;

    public Word(string nameRomaji,
                string nameEnglish,
                List<Letter> nameHiragana,
                string description,
                int wordSize,
                bool markedForDeletion = false)
    {
        this.nameRomaji = nameRomaji;
        this.nameEnglish = nameEnglish;
        this.nameHiragana = nameHiragana;
        this.description = description;
        this.wordSize = wordSize;
        this.markedForDeletion = markedForDeletion;
    }
}

public class LogicManager : MonoBehaviour
{

    public List<Word> theWordList = new List<Word>();
    public List<Letter> theDeck1 = new List<Letter>();
    public List<Letter> theDeck2 = new List<Letter>();

    public int minWordSize = int.MaxValue;
    public int maxWordSize = int.MinValue;

    public int wordCount = 0;
    public int deckSize = 0;

    public List<Letter> shuffledDeck1 = new List<Letter>();
    public List<Letter> shuffledDeck2 = new List<Letter>();

    private static LogicManager instance;
    public static LogicManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        InitializeLogic();
        shuffledDeck1 = theDeck1;
        shuffledDeck2 = theDeck2;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeLogic()
    {
        var databaseAsString = (Resources.Load("database") as TextAsset).text;
        var firstSplit = databaseAsString.Split('\n');

        foreach (var word in firstSplit)
        {
            var secondSplit = word.Split('~');
            var nameRomaji = secondSplit[0];
            var nameEnglish = secondSplit[1];
            var thirdSplit = secondSplit[2].Split('.');
            List<Letter> nameHiragana = new List<Letter>();
            for (int i = 0; i < thirdSplit.Length; i++)
            {
                Letter theLetter = new Letter(thirdSplit[i]);
                nameHiragana.Add(theLetter);
                for (int j = thirdSplit.Length; j > i; j--)
                {
                    theDeck1.Add(theLetter);
                    theDeck2.Add(theLetter);
                }
            }
            if (nameHiragana.Count > maxWordSize)
            {
                maxWordSize = nameHiragana.Count;
            }
            if (nameHiragana.Count < minWordSize)
            {
                minWordSize = nameHiragana.Count;
            }
            var description = secondSplit[3];
            var theWord = new Word(nameRomaji, nameEnglish, nameHiragana, description, nameHiragana.Count);

            theWordList.Add(theWord);
        }
        deckSize = theDeck1.Count;
        wordCount = theWordList.Count;
    }

    public List<Word> RunLogic(List<Letter> shuffledDeck)
    {
        var finalWords = new List<Word>();

        for (int i = 0; i < 5; i++)
        {
            var possibleWords = new List<Word>();

            foreach (var word in theWordList)
            {
                if (shuffledDeck[i].romaji == word.nameHiragana[0].romaji)
                {
                    possibleWords.Add(word);
                }
            }

            foreach (var word in possibleWords)
            {
                if (WordSearch(word, shuffledDeck))
                {
                    finalWords.Add(word);
                }
            }
        }
        //Debug.LogWarning(shuffledDeck[0].romaji + "." + shuffledDeck[1].romaji + "." + shuffledDeck[2].romaji + "." + shuffledDeck[3].romaji + "." + shuffledDeck[4].romaji + "." + shuffledDeck[5].romaji + "." + shuffledDeck[6].romaji + "." + shuffledDeck[7].romaji + "." + shuffledDeck[8].romaji + "." + shuffledDeck[9].romaji + "." + shuffledDeck[10].romaji + "." + shuffledDeck[11].romaji);
        return finalWords;
    }

    public bool WordSearch(Word word, List<Letter> letterList)
    {
        var currentSearchArea = 5;
        ClearHolding(letterList);

        for (int i = 0; i < word.wordSize; i++)
        {
            var matchFound = false;
            var indexFound = -1;
            for (int j = 0; j < currentSearchArea; j++)
            {
                if (!letterList[j].isHolding)
                {
                    if (word.nameHiragana[i].romaji == letterList[j].romaji)
                    {
                        matchFound = true;
                        indexFound = j;
                    }
                }
            }

            if (matchFound)
            {
                letterList[indexFound].isHolding = true;
                if (i + 1 == word.wordSize)
                {
                    return true;
                }
            }
            else
            {
                break;
            }

            if (i + 1 == word.wordSize)
            {
                return false;
            }

            matchFound = false;
            currentSearchArea++;
        }
        return false;
    }

    public void ClearHolding(List<Letter> list)
    {
        foreach (var letter in list)
        {
            letter.isHolding = false;
        }
    }

    public static List<Letter> Shuffle(List<Letter> list)
    {

        System.Random _random = new System.Random();

        Letter myLetter;

        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myLetter = list[r];
            list[r] = list[i];
            list[i] = myLetter;
        }

        return list;
    }
}
