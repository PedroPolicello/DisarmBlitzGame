using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathOperationGenerator : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private int minOperandValue;
    [SerializeField] private int maxOperandValue;
    [SerializeField] private Button checkButton;
    [SerializeField] private GameObject numberManager;

    [Header("UI")]
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject dashButton;
    [SerializeField] private GameObject numberUI;

    public GameObject[] numberSlots; 

    private int operand1;
    private int operand2;
    private int result;

    private int playerTrys = 3;
    private bool isNumberDisarmed = false;


    void Start()
    {
        GenerateMathOperation();
        checkButton.onClick.AddListener(CheckResult);
    }

    void Update()
    {
        if(playerTrys <= 0)
        {
            numberManager.gameObject.SetActive(false);
            numberUI.gameObject.SetActive(false);
            joystick.gameObject.SetActive(true);
            useButton.gameObject.SetActive(true);
            interactButton.gameObject.SetActive(true);
            dashButton.gameObject.SetActive(true);
            numberSlots[3].GetComponentInChildren<TextMeshProUGUI>().text = "";
            numberSlots[4].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    void GenerateMathOperation()
    {
        operand1 = Random.Range(minOperandValue, maxOperandValue + 1);
        operand2 = Random.Range(minOperandValue, maxOperandValue + 1);

        bool isAddition = Random.value > 0.5f;

        if (isAddition)
        {
            result = operand1 + operand2;
            numberSlots[1].GetComponentInChildren<TextMeshProUGUI>().text = "+";
        }
        else
        {
            // Garantir que o resultado não seja negativo
            if (operand1 < operand2)
            {
                int temp = operand1;
                operand1 = operand2;
                operand2 = temp;
            }

            result = operand1 - operand2;
            numberSlots[1].GetComponentInChildren<TextMeshProUGUI>().text = "-";
        }

        // Exibir os números nos slots de texto
        numberSlots[0].GetComponentInChildren<TextMeshProUGUI>().text = operand1.ToString();
        numberSlots[2].GetComponentInChildren<TextMeshProUGUI>().text = operand2.ToString();
        numberSlots[3].GetComponentInChildren<TextMeshProUGUI>().text = "";
        numberSlots[4].GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    public void CheckResult()
    {
        string playerResult = numberSlots[3].GetComponentInChildren<TextMeshProUGUI>().text + numberSlots[4].GetComponentInChildren<TextMeshProUGUI>().text;
        int finalResult = int.Parse(playerResult);

        if (finalResult == result)
        {
            Debug.Log("Resposta correta!");
            PlayerMovement.instance.SetNumberDisarm(true);
            numberManager.gameObject.SetActive(false);
            numberUI.gameObject.SetActive(false);
            joystick.gameObject.SetActive(true);
            useButton.gameObject.SetActive(true);
            interactButton.gameObject.SetActive(true);
            dashButton.gameObject.SetActive(true);
            numberSlots[3].GetComponentInChildren<TextMeshProUGUI>().text = "";
            numberSlots[4].GetComponentInChildren<TextMeshProUGUI>().text = "";
            GenerateMathOperation();
        }
        else
        {
            playerTrys--;
            Debug.Log("Resposta incorreta! Você tem " + playerTrys + " chances!");
            GenerateMathOperation();
        }
    }
}