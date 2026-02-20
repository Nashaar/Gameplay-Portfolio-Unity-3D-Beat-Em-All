using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    #region Declarations
    [Header("Actions")] 
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction sprintAction;
    public InputAction crouchAction;
    public InputAction lockAction;
    public InputAction attackAction;
    public InputAction interactAction;
    public InputAction nextAction;
    public InputAction previousAction;
    public InputAction weaponAction;
    #endregion

    #region Unity Functions
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move"); 
        jumpAction = InputSystem.actions.FindAction("Jump"); 
        sprintAction = InputSystem.actions.FindAction("Sprint"); 
        crouchAction = InputSystem.actions.FindAction("Crouch"); 
        lockAction = InputSystem.actions.FindAction("Lock"); 
        attackAction = InputSystem.actions.FindAction("Attack"); 
        interactAction = InputSystem.actions.FindAction("Interact"); 
        nextAction = InputSystem.actions.FindAction("Next"); 
        previousAction = InputSystem.actions.FindAction("Previous"); 
        weaponAction = InputSystem.actions.FindAction("Weapon"); 
    }

    private void OnEnable() 
    { 
        moveAction.Enable(); 
        jumpAction.Enable(); 
        sprintAction.Enable(); 
        crouchAction.Enable();
        lockAction.Enable();
        crouchAction.Enable();
        attackAction.Enable();
        interactAction.Enable();
        nextAction.Enable();
        previousAction.Enable();
        weaponAction.Enable();
    } 
    
    private void OnDisable() 
    { 
        moveAction.Disable(); 
        jumpAction.Disable(); 
        sprintAction.Disable(); 
        crouchAction.Disable();
        lockAction.Disable();
        crouchAction.Disable();
        attackAction.Disable();
        interactAction.Disable();
        nextAction.Disable();
        previousAction.Disable();
        weaponAction.Disable();
    } 
    #endregion

    #region Input Reading
    public bool InputPressed(InputAction actionPerformed)
    {
        bool inputPerformed = actionPerformed.WasPressedThisFrame();
        return inputPerformed;
    }

    public bool InputHeld(InputAction actionPerformed)
    {
        bool inputPerformedHold = actionPerformed.IsPressed();
        return inputPerformedHold;
    }

    public Vector2 MoveInput()
    {
        return moveAction.ReadValue<Vector2>(); 
    }
    #endregion
}
