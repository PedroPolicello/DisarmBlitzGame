using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGenerator : MonoBehaviour
{
    [SerializeField] private string wordToGuess;
    private char[] shuffledLetters;

    [SerializeField] private Transform[] wordSlots;
    [SerializeField] private TextMeshProUGUI[] wordObjects;

    [SerializeField] private CanvasGroup canvasGroup;

    void Start()
    {
        ShuffleWords();
        DisplayShuffledWords();
    }

    void ShuffleWords()
    {
        shuffledLetters = wordToGuess.ToCharArray();
        for (int i = 0; i < shuffledLetters.Length; i++)
        {
            char temp = shuffledLetters[i];
            int randomIndex = Random.Range(i, shuffledLetters.Length);
            shuffledLetters[i] = shuffledLetters[randomIndex];
            shuffledLetters[randomIndex] = temp;
        }
    }

    void DisplayShuffledWords()
    {
        for (int i = 0; i < wordSlots.Length; i++)
        {
            if (i < shuffledLetters.Length)
            {
                wordObjects[i].text = shuffledLetters[i].ToString();
                wordSlots[i].gameObject.SetActive(true);
            }
            else
            {
                wordSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void CheckWord()
    {
        string playerWord = "";
        for (int i = 0; i < wordSlots.Length; i++)
        {
            playerWord += wordObjects[i].text;
        }

        if (playerWord == wordToGuess)
        {
            Debug.Log("Palavra correta!");
            canvasGroup.alpha = 0f; // Torna o UI transparente
            canvasGroup.interactable = false; // Desativa a interação com o UI
            canvasGroup.blocksRaycasts = false; // Impede que o UI intercepte eventos de clique
        }
        else
        {
            Debug.Log("Palavra incorreta!");
        }
    }
}