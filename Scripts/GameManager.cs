using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject theField1;
    public GameObject theHand1;
    public GameObject theField2;
    public GameObject theHand2;
    public Canvas theCanvas;
    public Text theWordText1;
    public Text theDescriptionText1;
    public Text theWordText2;
    public Text theDescriptionText2;

    List<Word> finalWords1 = new List<Word>();
    List<Word> finalWords2 = new List<Word>();

    List<Letter> theFieldList1 = new List<Letter>();
    List<Letter> theFieldList2 = new List<Letter>();

    List<int> handX = new List<int> { -86, -43, 0, 43, 86 };
    List<int> fieldX = new List<int> { -64, -48, -32, -16, 0, 16, 32, 48, 64 };

    public int selector1 = 0;
    public int selector2 = 0;
    public bool isSelecting1 = false;
    public bool isSelecting2 = false;
    public bool isLocked1 = false;
    public bool isLocked2 = false;

    bool AnalogMovingR1;
    bool AnalogMovingL1;
    bool DpadMovingR1;
    bool DpadMovingL1;
    bool AnalogMovingR2;
    bool AnalogMovingL2;
    bool DpadMovingR2;
    bool DpadMovingL2;

    public bool Success1;
    public bool Success2;

    // Use this for initialization
    void Start () {
        SetupDecks();
        Highlight(1);
        Highlight(2);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Selector();
        SuccessCheck(1);
        SuccessCheck(2);
    }
    
    public void SuccessCheck(int player)
    {
        if(player == 1)
        {
            Success1 = SuccessfulWord(1);
            if (Success1)
            {
                Success1 = false;
                theWordText1.text = "Romaji: " + finalWords1[0].nameRomaji + "\nEnglish: " + finalWords1[0].nameEnglish;
                theDescriptionText1.text = finalWords1[0].description;
                NewAttackManager.Instance.spawnHitOn(2);
                StartCoroutine(Refresh(1));
            }
        }
        if (player == 2)
        {
            Success2 = SuccessfulWord(2);
            if (Success2)
            {
                Success2 = false;
                theWordText2.text = "Romaji: " + finalWords2[0].nameRomaji + "\nEnglish: " + finalWords2[0].nameEnglish;
                theDescriptionText2.text = finalWords2[0].description;
                NewAttackManager.Instance.spawnHitOn(1);
                StartCoroutine(Refresh(2));
            }
        }
    }

    public IEnumerator Refresh(int player)
    {
        if(player == 1)
        {
            isLocked1 = true;
            yield return new WaitForSeconds(0.5f);
            theFieldList1.Clear();
            var allChildren = theField1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            foreach (var child in allChildren)
            {
                child.transform.SetParent(theCanvas.transform);
                child.SetActive(false);
            }

            finalWords1 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck1);

            finalWords1 = finalWords1.Distinct().ToList();

            UpdateHand(1);
            Highlight(1);
            isLocked1 = false;
        }
        if (player == 2)
        {
            isLocked2 = true;
            yield return new WaitForSeconds(0.5f);
            theFieldList2.Clear();
            var allChildren = theField2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            foreach (var child in allChildren)
            {
                child.transform.SetParent(theCanvas.transform);
                child.SetActive(false);
            }

            finalWords2 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck2);

            finalWords2 = finalWords2.Distinct().ToList();

            UpdateHand(2);
            Highlight(2);
            isLocked2 = false;
        }
    }

    public bool SuccessfulWord(int player)
    {
        if(player == 1)
        {
            if(theFieldList1.Count != 0)
            {
                if (finalWords1.Count == 1)
                {
                    if (theFieldList1.Count == finalWords1[0].nameHiragana.Count &&
                        theFieldList1[theFieldList1.Count - 1].romaji == finalWords1[0].nameHiragana[finalWords1[0].nameHiragana.Count - 1].romaji &&
                        theFieldList1[0].romaji == finalWords1[0].nameHiragana[0].romaji)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        if (player == 2)
        {
            if (theFieldList2.Count != 0)
            {
                if (finalWords2.Count == 1)
                {
                    if (theFieldList2.Count == finalWords2[0].nameHiragana.Count &&
                        theFieldList2[theFieldList2.Count - 1].romaji == finalWords2[0].nameHiragana[finalWords2[0].nameHiragana.Count - 1].romaji &&
                        theFieldList2[0].romaji == finalWords2[0].nameHiragana[0].romaji)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        return false;
    }

    public void TrimExcess(int player)
    {
        if(player == 1)
        {
            for (int i = 0; i < theFieldList1.Count; i++)
            {
                for (int j = finalWords1.Count - 1; j >= 0; j--)
                {
                    if (theFieldList1.Count > finalWords1[j].nameHiragana.Count || theFieldList1[i].romaji != finalWords1[j].nameHiragana[i].romaji)
                    {
                        if (!SuccessfulWord(1))
                        {
                            finalWords1.Remove(finalWords1[j]);
                        }
                    }
                }
            }
        }
        if (player == 2)
        {
            for (int i = 0; i < theFieldList2.Count; i++)
            {
                for (int j = finalWords2.Count - 1; j >= 0; j--)
                {
                    if (theFieldList2.Count > finalWords2[j].nameHiragana.Count || theFieldList2[i].romaji != finalWords2[j].nameHiragana[i].romaji)
                    {
                        if (!SuccessfulWord(2))
                        {
                            finalWords2.Remove(finalWords2[j]);
                        }
                    }
                }
            }
        }
    }

    public void Highlight(int player)
    {
        if(player == 1)
        {
            var allChildren1 = theHand1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            for (int i = finalWords1.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!(theFieldList1.Count > finalWords1[i].nameHiragana.Count - 1))
                    {
                        if (finalWords1[i].nameHiragana[theFieldList1.Count].romaji == LogicManager.Instance.shuffledDeck1[j].romaji)
                        {
                            allChildren1[j].GetComponent<Image>().color = new Color(1f, 0.84f, 0f);
                        }
                    }
                }
            }
        }
        if (player == 2)
        {
            var allChildren2 = theHand2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            for (int i = finalWords2.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!(theFieldList2.Count > finalWords2[i].nameHiragana.Count - 1))
                    {
                        if (finalWords2[i].nameHiragana[theFieldList2.Count].romaji == LogicManager.Instance.shuffledDeck2[j].romaji)
                        {
                            allChildren2[j].GetComponent<Image>().color = new Color(1f, 0.84f, 0f);
                        }
                    }
                }
            }
        }
    }

    public void UpdateHand(int player)
    {
        if(player == 1)
        {
            var allChildren = theHand1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            for (int i = 0; i < 5; i++)
            {
                allChildren[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
        if (player == 2)
        {
            var allChildren = theHand2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            for (int i = 0; i < 5; i++)
            {
                allChildren[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    } 

    public void Selector()
    {
        if(theFieldList1.Count >= 9)
        {
            isLocked1 = true;
        }
        else
        {
            isLocked1 = false;
        }
        if (theFieldList2.Count >= 9)
        {
            isLocked2 = true;
        }
        else
        {
            isLocked2 = false;
        }

        AnalogMovingR1 = Input.GetAxis("1 PS4 Right Analog Horizontal") > 0.03 && !(Input.GetAxis("1 PS4 Right Analog Vertical") > 0.03) && !(Input.GetAxis("1 PS4 Right Analog Vertical") < -0.03);
        AnalogMovingL1 = Input.GetAxis("1 PS4 Right Analog Horizontal") < -0.03 && !(Input.GetAxis("1 PS4 Right Analog Vertical") > 0.03) && !(Input.GetAxis("1 PS4 Right Analog Vertical") < -0.03);

        AnalogMovingR2 = Input.GetAxis("2 PS4 Right Analog Horizontal") > 0.03 && !(Input.GetAxis("2 PS4 Right Analog Vertical") > 0.03) && !(Input.GetAxis("2 PS4 Right Analog Vertical") < -0.03);
        AnalogMovingL2 = Input.GetAxis("2 PS4 Right Analog Horizontal") < -0.03 && !(Input.GetAxis("2 PS4 Right Analog Vertical") > 0.03) && !(Input.GetAxis("2 PS4 Right Analog Vertical") < -0.03);

        if (AnalogMovingR1 && !isSelecting1)
        {
            StartCoroutine(Select(1, 1));
        }
        if (AnalogMovingL1 && !isSelecting1)
        {
            StartCoroutine(Select(1, -1));
        }
        if (AnalogMovingR2 && !isSelecting2)
        {
            StartCoroutine(Select(2, 1));
        }
        if (AnalogMovingL2 && !isSelecting2)
        {
            StartCoroutine(Select(2, -1));
        }

        if (Input.GetButtonDown("1 PS4 X") && !isLocked1 || Input.GetButtonDown("1 PS4 R1") && !isLocked1)
        {
            PlayCard(1, selector1);
            UpdateHand(1);
            TrimExcess(1);
            Highlight(1);
        }
        if (Input.GetButtonDown("1 PS4 S") && !isLocked1)
        {
            DiscardHand(1);
            UpdateHand(1);
            TrimExcess(1);
            Highlight(1);
        }

        if (Input.GetButtonDown("2 PS4 X") && !isLocked2 || Input.GetButtonDown("2 PS4 R1") && !isLocked2)
        {
            PlayCard(2, selector2);
            UpdateHand(2);
            TrimExcess(2);
            Highlight(2);
        }
        if (Input.GetButtonDown("2 PS4 S") && !isLocked2)
        {
            DiscardHand(2);
            UpdateHand(2);
            TrimExcess(2);
            Highlight(2);
        }

        var allChildren1 = theHand1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        var obj1 = allChildren1[selector1];
        foreach (var child in allChildren1)
        {
            child.transform.localScale = new Vector3(1, 1, 1);
        }
        obj1.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        var allChildren2 = theHand2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        var obj2 = allChildren2[selector2];
        foreach (var child in allChildren2)
        {
            child.transform.localScale = new Vector3(1, 1, 1);
        }
        obj2.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public IEnumerator Select(int player, int increment)
    {
        if(player == 1)
        {
            isSelecting1 = true;
            selector1 += increment;
            if(selector1 > 4)
            {
                selector1 = 0;
            }
            else if(selector1 < 0)
            {
                selector1 = 4;
            }
            yield return new WaitForSeconds(0.3f);
            isSelecting1 = false;
        }
        if (player == 2)
        {
            isSelecting2 = true;
            selector2 += increment;
            if (selector2 > 4)
            {
                selector2 = 0;
            }
            else if (selector2 < 0)
            {
                selector2 = 4;
            }
            yield return new WaitForSeconds(0.3f);
            isSelecting2 = false;
        }
    }

    public void Swap(List<Letter> list, int indexA, int indexB)
    {
        Letter tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    public void PlayCard(int player, int index)
    {
        if(player == 1)
        {
            var newObj = EnhancedObjectPooler.Instance.GetPooledObject(LogicManager.Instance.shuffledDeck1[5].romaji);
            newObj.transform.SetParent(theHand1.transform);
            newObj.transform.SetSiblingIndex(index);
            newObj.transform.localPosition = new Vector3(handX[index], 0, 0);
            newObj.SetActive(true);

            var allChildren = theHand1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            var oldObj = allChildren[index+1];
            oldObj.SetActive(false);
            oldObj.transform.SetParent(theField1.transform);
            var allChildrenField = theField1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            var currentIndex = allChildrenField.Length - 1;
            oldObj.transform.localPosition = new Vector3(fieldX[currentIndex], 0, 0);
            oldObj.SetActive(true);

            theFieldList1.Add(LogicManager.Instance.shuffledDeck1[index]);
            LogicManager.Instance.shuffledDeck1.Add(LogicManager.Instance.shuffledDeck1[index]);
            Swap(LogicManager.Instance.shuffledDeck1, index, 5);
            LogicManager.Instance.shuffledDeck1.RemoveAt(5);

            //Debug.LogWarning(LogicManager.Instance.shuffledDeck1[0].romaji + "." + LogicManager.Instance.shuffledDeck1[1].romaji + "." + LogicManager.Instance.shuffledDeck1[2].romaji + "." + LogicManager.Instance.shuffledDeck1[3].romaji + "." + LogicManager.Instance.shuffledDeck1[4].romaji + "." + LogicManager.Instance.shuffledDeck1[5].romaji + "." + LogicManager.Instance.shuffledDeck1[6].romaji + "." + LogicManager.Instance.shuffledDeck1[7].romaji + "." + LogicManager.Instance.shuffledDeck1[8].romaji + "." + LogicManager.Instance.shuffledDeck1[9].romaji + "." + LogicManager.Instance.shuffledDeck1[10].romaji + "." + LogicManager.Instance.shuffledDeck1[11].romaji);

            //foreach (var letter in theFieldList1)
            //{
            //    Debug.Log(letter.romaji);
            //}
        }
        else if(player == 2)
        {
            var newObj = EnhancedObjectPooler.Instance.GetPooledObject(LogicManager.Instance.shuffledDeck2[5].romaji);
            newObj.transform.SetParent(theHand2.transform);
            newObj.transform.SetSiblingIndex(index);
            newObj.transform.localPosition = new Vector3(handX[index], 0, 0);
            newObj.SetActive(true);

            var allChildren = theHand2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            var oldObj = allChildren[index + 1];
            oldObj.SetActive(false);
            oldObj.transform.SetParent(theField2.transform);
            var allChildrenField = theField2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            var currentIndex = allChildrenField.Length - 1;
            oldObj.transform.localPosition = new Vector3(fieldX[currentIndex], 0, 0);
            oldObj.SetActive(true);

            theFieldList2.Add(LogicManager.Instance.shuffledDeck2[index]);
            LogicManager.Instance.shuffledDeck2.Add(LogicManager.Instance.shuffledDeck2[index]);
            Swap(LogicManager.Instance.shuffledDeck2, index, 5);
            LogicManager.Instance.shuffledDeck2.RemoveAt(5);
        }
    }

    public void DiscardHand(int player)
    {
        if (player == 1)
        {
            UpdateHand(1);

            var allChildren = theHand1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            for (int i = 0; i < 5; i++)
            {
                LogicManager.Instance.shuffledDeck1.Add(LogicManager.Instance.shuffledDeck1[i]);
                Swap(LogicManager.Instance.shuffledDeck1, i, 5);
                LogicManager.Instance.shuffledDeck1.RemoveAt(5);
                GameObject obj = allChildren[i];
                obj.transform.SetParent(theCanvas.transform);
                obj.SetActive(false);
            }

            theFieldList1.Clear();
            var allChildrenField = theField1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            foreach(var child in allChildrenField)
            {
                child.transform.SetParent(theCanvas.transform);
                child.SetActive(false);
            }

            LogicManager.Instance.shuffledDeck1 = LogicManager.Shuffle(LogicManager.Instance.theDeck);
            finalWords1 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck1);
            while(finalWords1.Count == 0)
            {
                LogicManager.Instance.shuffledDeck1 = LogicManager.Shuffle(LogicManager.Instance.theDeck);
                finalWords1 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck1);
            }

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = EnhancedObjectPooler.Instance.GetPooledObject(LogicManager.Instance.shuffledDeck1[i].romaji);
                obj.transform.SetParent(theHand1.transform);
                obj.transform.localPosition = new Vector3(handX[i], 0, 0);
                obj.SetActive(true);
            }

            finalWords1 = finalWords1.Distinct().ToList();

            UpdateHand(1);
            TrimExcess(1);
            Highlight(1);

            //foreach (var word in finalWords1)
            //{
            //    Debug.Log(word.nameRomaji);
            //}
        }
        else if (player == 2)
        {
            UpdateHand(2);

            var allChildren = theHand2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            for (int i = 0; i < 5; i++)
            {
                LogicManager.Instance.shuffledDeck2.Add(LogicManager.Instance.shuffledDeck2[i]);
                Swap(LogicManager.Instance.shuffledDeck2, i, 5);
                LogicManager.Instance.shuffledDeck2.RemoveAt(5);
                GameObject obj = allChildren[i];
                obj.transform.SetParent(theCanvas.transform);
                obj.SetActive(false);
            }

            theFieldList2.Clear();
            var allChildrenField = theField2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            foreach (var child in allChildrenField)
            {
                child.transform.SetParent(theCanvas.transform);
                child.SetActive(false);
            }

            LogicManager.Instance.shuffledDeck2 = LogicManager.Shuffle(LogicManager.Instance.theDeck);
            finalWords2 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck2);
            while (finalWords2.Count == 0)
            {
                LogicManager.Instance.shuffledDeck2 = LogicManager.Shuffle(LogicManager.Instance.theDeck);
                finalWords2 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck2);
            }

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = EnhancedObjectPooler.Instance.GetPooledObject(LogicManager.Instance.shuffledDeck2[i].romaji);
                obj.transform.SetParent(theHand2.transform);
                obj.transform.localPosition = new Vector3(handX[i], 0, 0);
                obj.SetActive(true);
            }

            finalWords2 = finalWords2.Distinct().ToList();

            UpdateHand(2);
            TrimExcess(2);
            Highlight(2);
        }
    }

    public void SetupDecks()
    {
        LogicManager.Instance.shuffledDeck1 = LogicManager.Shuffle(LogicManager.Instance.theDeck);
        LogicManager.Instance.shuffledDeck2 = LogicManager.Instance.shuffledDeck1;

        finalWords1 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck1);
        finalWords2 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck2);

        while(finalWords1.Count == 0 && finalWords2.Count == 0)
        {
            LogicManager.Instance.shuffledDeck1 = LogicManager.Shuffle(LogicManager.Instance.theDeck);
            LogicManager.Instance.shuffledDeck2 = LogicManager.Instance.shuffledDeck1;

            finalWords1 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck1);
            finalWords2 = LogicManager.Instance.RunLogic(LogicManager.Instance.shuffledDeck2);
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject obj = EnhancedObjectPooler.Instance.GetPooledObject(LogicManager.Instance.shuffledDeck1[i].romaji);
            obj.transform.SetParent(theHand1.transform);
            obj.transform.localPosition = new Vector3(handX[i], 0, 0);
            obj.SetActive(true);
            GameObject obj2 = EnhancedObjectPooler.Instance.GetPooledObject(LogicManager.Instance.shuffledDeck2[i].romaji);
            obj2.transform.SetParent(theHand2.transform);
            obj2.transform.localPosition = new Vector3(handX[i], 0, 0);
            obj2.SetActive(true);
        }

        finalWords1 = finalWords1.Distinct().ToList();
        finalWords2 = finalWords2.Distinct().ToList();

        //foreach(var word in finalWords1)
        //{
        //    Debug.Log(word.nameRomaji);
        //}
    }
}
