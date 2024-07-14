using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public InputAction mouseClick;
   // public ItemSlotScript itemslotscript;
    [SerializeField] private UnityEngine.UI.Button exitButton;
    [SerializeField] private ChestInventoryManager CInventoryManager;
    private PlayerCombat playerCombatScript;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput =this.GetComponent<PlayerInput>();
        playerCombatScript=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        mouseClick = playerInput.actions["Click"];
       /* mouseClick.started += ClickStarted;
        mouseClick.performed += ClickPerformed;
        mouseClick.performed += playerCombatScript.StartAttack1;
        mouseClick.canceled += ClickCanceled;*/
    }
    private void OnEnable()
    {
        mouseClick.started += ClickStarted;
        mouseClick.performed += ClickPerformed;
       // mouseClick.performed += playerCombatScript.StartAttack1;
        mouseClick.canceled += ClickCanceled;
    }
    private void OnDisable()
    {
        mouseClick.started -= ClickStarted;
        mouseClick.performed -= ClickPerformed;
       // mouseClick.performed -= playerCombatScript.StartAttack1;
        mouseClick.canceled -= ClickCanceled;
    }
    public void ClickStarted(InputAction.CallbackContext context)
    {

    }
    public void ClickPerformed(InputAction.CallbackContext context)
    {
        if (CInventoryManager.gameObject.activeSelf)
        {
        }
    }
    public void ClickCanceled(InputAction.CallbackContext context)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
