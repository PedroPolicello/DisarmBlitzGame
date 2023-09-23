using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;


    [Header("Player Movement")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject dashStun;
    private float normalSpeed;
    private bool canDash = true;

    [Header("Player UI")]
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject dashButton;
    [SerializeField] private GameObject disarmScreen;

    [Header("Desarms")]
    [SerializeField] private GameObject wordDesarm;
    [SerializeField] private GameObject numberDesarm;
    [SerializeField] private GameObject blind;
    [SerializeField] private int QTE;
    private bool desarmNum;
    private bool desarmWord;
    private bool bearTrap;

    private bool escapingBearTrap;
    private int escapeButtonPresses = 0;
    private int requiredEscapePresses = 5;
    private GameObject bearTrapPrefab;

    private Vector2 myInput;
    private Transform playerTransform;

    private bool isWordDisarmed = false;
    private bool isNumberDisarmed = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerTransform = GetComponent<Transform>();
        normalSpeed = speed;
    }
    public void Input(InputAction.CallbackContext value)
    {
        myInput = value.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext value)
    {
        if (value.performed && desarmNum == true && isNumberDisarmed==false)
        {
            numberDesarm.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            dashButton.gameObject.SetActive(false);
            desarmNum = false;
        }

        else if (value.performed && desarmWord == true && isWordDisarmed==false)
        {
            wordDesarm.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            dashButton.gameObject.SetActive(false);
            desarmWord = false;
        }
    }

    public void Use(InputAction.CallbackContext value)
    {
        if (value.performed && bearTrap && !escapingBearTrap)
        {
            escapingBearTrap = true;
        }

        if (value.canceled && escapingBearTrap)
        {
            escapeButtonPresses++;
            print(escapeButtonPresses);

            if (escapeButtonPresses >= requiredEscapePresses)
            {
                bearTrap = false;
                escapingBearTrap = false;
                speed = normalSpeed;
            }
        }
    }

    public void CallDash()
    {
        if (canDash == true)
        {
            StartCoroutine(Dash());
        }
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        playerTransform.Translate(new Vector3(myInput.x, 0, myInput.y) * speed * Time.deltaTime);
    }

    //DESARMS
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NumberDesarm")
        {
            desarmNum = true;
        }

        else if (collision.gameObject.tag == "WordDesarm")
        {
            desarmWord = true;
        }

        else if (collision.gameObject.tag == "Blind")
        {
            StartCoroutine(Blind());
        }

        else if (collision.gameObject.tag == "BearTrap")
        {
            bearTrap = true;
            speed = 0;
        }

        else if (collision.gameObject.tag == "HoneySlow")
        {
            speed = normalSpeed / 2;
        }

        else if(collision.gameObject.CompareTag("FinalDisarm") && isWordDisarmed && isNumberDisarmed)
        {
            print("Bomb Disarmed");
            disarmScreen.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "NumberDesarm")
        {
            desarmNum = false;
        }

        else if (collision.gameObject.tag == "WordDesarm")
        {
            desarmWord = false;
        }

        if (collision.gameObject.tag == "HoneySlow")
        {
            speed = normalSpeed;
        }
    }

    IEnumerator Dash()
    {
        dashStun.SetActive(true);
        speed *= 1.5f;
        yield return new WaitForSeconds(.4f);
        speed = normalSpeed;
        canDash = false;
        dashStun.SetActive(false);
        yield return new WaitForSeconds(5f);
        canDash = true;
    }

    //SABOTAGENS
    IEnumerator DashStun()
    {
        speed = 0;
        yield return new WaitForSeconds(2f);
        speed = normalSpeed;
    }

    IEnumerator Blind()
    {
        blind.SetActive(true);
        yield return new WaitForSeconds(2f);
        blind.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DashStun")
        {
            StartCoroutine(DashStun());
        }
    }


    public void SetNumberDisarm(bool isDisarmed)
    {
        isNumberDisarmed = isDisarmed;
    }

    public void SetWordDisarm(bool isDisarmed)
    {
        isWordDisarmed = isDisarmed;
    }

}
