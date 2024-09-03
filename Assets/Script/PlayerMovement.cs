using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2.0f;
    [SerializeField]
    private float SprintSpeed = 6.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    public float rotationSpeed = .8f;
    private float currentSpeed;
    public float animatorSmoother = 0.1f;

    public Transform CameraTransform;
    private PlayerInput playerInput;
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private InputAction movement;
    private InputAction jump;
    private InputAction Sprint;

    Vector2 currentAnimationBlend;
    Vector2 animationVelocity;

    public GameObject FootStepSound;
    public AudioSource footStepaudioSource;

    private Transform nearestZombie;
    private bool isCoroutineRunning = false;
    private bool isBitten = false;

    private void Start()
    {
        footStepaudioSource = FootStepSound.GetComponent<AudioSource>();
        FootStepSound.SetActive(false);
        CameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        movement = playerInput.actions["Movement"];
        jump = playerInput.actions["Jump"];
        Sprint = playerInput.actions["Sprint"];
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isCoroutineRunning) return;
        if (EventManager.instance.GetWakeDone() == false) return;
        if (Dialog.instance.GetDialogDone() == false) return;
        if (DoorInteractionFunction.instance.GetPlayerIsTeleported() == true) return;
        if (ChangeVictoryScene.instance.GetIsVictory() == true) return;
        if (isBitten) return;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Sprint.ReadValue<float>() > 0)
        {
            animator.SetBool("isSprint", true);
            currentSpeed = SprintSpeed;
            footStepaudioSource.pitch = 0.8f;
        }
        else
        {
            animator.SetBool("isSprint", false);
            currentSpeed = walkSpeed;
        }

        Vector2 input = movement.ReadValue<Vector2>();
        currentAnimationBlend = Vector2.SmoothDamp(currentAnimationBlend, input, ref animationVelocity, animatorSmoother);
        Vector3 move = new Vector3(currentAnimationBlend.x, 0, currentAnimationBlend.y);

        move = move.x * CameraTransform.right + move.z * CameraTransform.forward;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * currentSpeed);

        if (move != Vector3.zero)
        {
            FootStepSound.SetActive(true);
        }
        else
        {
            FootStepSound.SetActive(false);
        }

        if (jump.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.CrossFade("Jump", 0.1f);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Rotasi();

        animator.SetFloat("MoveX", currentAnimationBlend.x);
        animator.SetFloat("MoveZ", currentAnimationBlend.y);
    }

    void Rotasi()
    {
        if (movement.ReadValue<Vector2>() == Vector2.zero)
        {
            return;
        }
        Quaternion rotasiCamera = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotasiCamera, Time.deltaTime * rotationSpeed);
    }

    public void OnBittenByZombie(Transform zombie)
    {   
        isBitten = true;    
        nearestZombie = zombie;
        StartCoroutine(HandleZombieBite());
    }

    private IEnumerator HandleZombieBite()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(1f);
        bitten();
        isCoroutineRunning = false;
    }

    private void bitten()
    {
        transform.LookAt(nearestZombie);
        animator.SetTrigger("Stunned");
    }
}
