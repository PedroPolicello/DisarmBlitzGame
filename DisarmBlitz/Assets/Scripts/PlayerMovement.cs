using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject dashStun;
    private float normalSpeed;
    private bool canDash = true;

    [Header("Desarms")]
    [SerializeField] private GameObject wordDesarm;
    [SerializeField] private GameObject wordManager;
    [SerializeField] private GameObject numberDesarm;
    [SerializeField] private GameObject numberManager;
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

    void Awake()
    {
        playerTransform = GetComponent<Transform>();
        normalSpeed = speed;
    }
    public void Input(InputAction.CallbackContext value)
    {
        myInput = value.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext value)
    {
        if (value.performed && desarmNum == true)
        {
            numberDesarm.gameObject.SetActive(true);
            numberManager.gameObject.SetActive(true);
            //speed = 0;
            desarmNum = false;
        }

        else if (value.performed && desarmWord == true)
        {
            wordDesarm.gameObject.SetActive(true);
            wordManager.gameObject.SetActive(true);
            //speed = 0;
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
                //bearTrapPrefab.Destroy(bearTrapPrefab);
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
}
