using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool IsGrounded { get; set; }
    public Vector2 MovementInput { get; private set; }
    public int InputX { get; private set; }
    public int InputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool[] AttackInput { get; private set; }

    public bool playingFootSteps = false;
    public float footStepsSpeed = 0.5f;

    public void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInput = new bool[count];

    }
    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        // Prevent input if the menu is open
        if (MenuController.IsMenuOpen) return;

        if (context.started)
        {
            AttackInput[(int)CombatInputs.primary] = true;
        }
        if (context.canceled)
        {
            AttackInput[(int)CombatInputs.primary] = false;
        }
    }
    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        // Prevent input if the menu is open
        if (MenuController.IsMenuOpen) return;

        if (context.started)
        {
            AttackInput[(int)CombatInputs.secondary] = true;
        }
        if (context.canceled)
        {
            AttackInput[(int)CombatInputs.secondary] = false;
        }
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // Prevent input if the menu is open
        if (MenuController.IsMenuOpen) return;

        MovementInput = context.ReadValue<Vector2>();

        InputX = (int)(MovementInput * Vector2.right).x;
        InputY = (int)(MovementInput * Vector2.up).y;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // Prevent input if the menu is open
        if (MenuController.IsMenuOpen) return;

        if (context.started)
        {
            JumpInput = true;
        }
    }
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        // Prevent input if the menu is open
        if (MenuController.IsMenuOpen) return;

        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        // Prevent input if the menu is open
        if (MenuController.IsMenuOpen) return;

        if (context.started)
        {
            DashInput = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
   
}

public enum CombatInputs
{
    primary,
    secondary
}