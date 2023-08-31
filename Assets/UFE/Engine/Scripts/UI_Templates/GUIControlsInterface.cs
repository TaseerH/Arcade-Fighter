//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//namespace UFE3D
//{
//    public sealed class GUIControlsInterface : AbstractInputController
//    {
//        PlayerControls controls;

//        Vector2 movement;

//        ButtonPress btn;
//        bool btnInput = false;

//        private int punch1_counter = 1;
//        float comboResetDelay = 1.2f;
//        float lastPunchTime = 0f;
//        Coroutine comboResetCoroutine;

//        IEnumerator ResetCombo()
//        {
//            yield return new WaitForSeconds(comboResetDelay);
//            punch1_counter = 0;
//        }

//        public override void Initialize(IEnumerable<InputReferences> inputs)
//        {
//            base.Initialize(inputs);
//        }

//        private void Awake()
//        {
//            controls = new PlayerControls();

//            controls.Player.Joystick.started += OnMovement;
//            controls.Player.Joystick.performed += OnMovement;
//            controls.Player.Joystick.canceled += OnMovement;

//            controls.Player.Punch1.performed += OnPunch1;
//            controls.Player.Kick1.performed += OnKick1;
//            controls.Player.Special.performed += OnSpecialMove;
//        }

//        private void OnEnable()
//        {
//            controls.Player.Enable();
//        }

//        private void OnDisable()
//        {
//            controls.Player.Disable();
//        }

//        private void Update()
//        {
//            //Debug.Log(punch1_counter);
            
//        }

//        private void OnMovement(InputAction.CallbackContext ctx)
//        {
//            Vector2 input = ctx.ReadValue<Vector2>();

//            movement.x = input.x;
//            if (Mathf.Abs(input.y) > 0.5)
//            {
//                movement.y = input.y;
//            }
//            else
//            {
//                movement.y = 0f;
//            }
//        }

//        private void OnPunch1(InputAction.CallbackContext ctx)
//        {
//            btnInput = ctx.ReadValueAsButton();
//            if (punch1_counter == 0)
//            {
//                btn = ButtonPress.Button1;
//                punch1_counter = 1;
//                Debug.Log(punch1_counter);
//                //lastPunchTime = Time.time;
//                StopAllCoroutines();
//                comboResetCoroutine = StartCoroutine(ResetCombo());
//            }
//            else if (punch1_counter == 1)
//            {
//                btn = ButtonPress.Button2;
//                punch1_counter = 2;
//                //lastPunchTime = Time.time;
//                StopAllCoroutines();
//                comboResetCoroutine = StartCoroutine(ResetCombo());
//            }
//            else if (punch1_counter == 2)
//            {
//                btn = ButtonPress.Button3;
//                punch1_counter = 0;
//                StopAllCoroutines();
//                comboResetCoroutine = StartCoroutine(ResetCombo());
//                //lastPunchTime = Time.time;
//            }
//        }

//        private void OnKick1(InputAction.CallbackContext ctx)
//        {
//            btnInput = ctx.ReadValueAsButton();

//        }
//        private void OnSpecialMove(InputAction.CallbackContext ctx)
//        {
//            btnInput = ctx.ReadValueAsButton();
//            btn = ButtonPress.Button10;
//        }


      

//        public override InputEvents ReadInput(InputReferences inputReference)
//        {
//            if (inputReference != null)
//            {
//                if (inputReference.inputType == InputType.HorizontalAxis)
//                {
//                    return new InputEvents(movement.x);
//                }
//                else if (inputReference.inputType == InputType.VerticalAxis)
//                {
//                    return new InputEvents(movement.y);
//                }

//                if (inputReference.inputType == InputType.Button && inputReference.engineRelatedButton == btn)
//                {
//                    return new InputEvents(btnInput);
//                }
//            }
//            return InputEvents.Default;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UFE3D
{
    public sealed class GUIControlsInterface : AbstractInputController
    {
        PlayerControls controls;

        Vector2 movement;

        ButtonPress btn;
        bool btnInput = false;

        Punch1ComboStage currentPunch1ComboStage = Punch1ComboStage.LightPunch;
        Kick1ComboStage currentKick1ComboStage = Kick1ComboStage.LightKick;
        Punch2ComboStage currentPunch2ComboStage = Punch2ComboStage.LightPunch2;
        Kick2ComboStage currentKick2ComboStage = Kick2ComboStage.LightKick2;
        float comboResetDelay = 1f;

        enum Punch1ComboStage : byte
        {
            LightPunch = ButtonPress.Button1,
            MediumPunch,
            HeavyPunch
        }

        enum Kick1ComboStage : byte
        {
            LightKick = ButtonPress.Button4,
            MediumKick,
            HeavyKick
        }

        enum Punch2ComboStage : byte
        {
            LightPunch2 = ButtonPress.Button7,
            MediumPunch2,
        }

        enum Kick2ComboStage : byte
        {
            LightKick2 = ButtonPress.Button10,
            MediumKick2,
            HeavyKick2
        }

        public override void Initialize(IEnumerable<InputReferences> inputs)
        {
            base.Initialize(inputs);
        }

        private void Awake()
        {
            controls = new PlayerControls();

            controls.Player.Move.started += OnMovement;
            controls.Player.Move.performed += OnMovement;
            controls.Player.Move.canceled += OnMovement;

            controls.Player.Punch1.performed += OnPunch1;
            controls.Player.Kick1.performed += OnKick1;
            controls.Player.Punch2.performed += OnPunch2;
            controls.Player.Kick2.performed += OnKick2;
            controls.Player.SpecialMove.performed += OnSpecialMove;
            controls.Player.Select.performed += PauseGame;


        }

        private void OnEnable()
        {
            controls.Player.Enable();
        }

        private void OnDisable()
        {
            controls.Player.Disable();
        }

        private void OnMovement(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>();

            movement.x = input.x;
            if (Mathf.Abs(input.y) > 0.5)
            {
                movement.y = input.y;
            }
            else
            {
                movement.y = 0f;
            }
        }

        private void OnPunch1(InputAction.CallbackContext ctx)
        {
            btnInput = ctx.ReadValueAsButton();

            if (btnInput)
            {
                btn = (ButtonPress)(byte)currentPunch1ComboStage;
                Debug.Log(btn);
                return;
            }

            if (currentPunch1ComboStage == Punch1ComboStage.LightPunch)
            {
                currentPunch1ComboStage = Punch1ComboStage.MediumPunch;
                StartCoroutine(ResetCombo());
            }
            else if (currentPunch1ComboStage == Punch1ComboStage.MediumPunch)
            {
                StopAllCoroutines();
                currentPunch1ComboStage = Punch1ComboStage.HeavyPunch;
                StartCoroutine(ResetCombo());
            }
            else if (currentPunch1ComboStage == Punch1ComboStage.HeavyPunch)
            {
                StopAllCoroutines();
                currentPunch1ComboStage = Punch1ComboStage.LightPunch;
            }
        }

        private void OnKick1(InputAction.CallbackContext ctx)
        {
            btnInput = ctx.ReadValueAsButton();

            if (btnInput)
            {
                btn = (ButtonPress)(byte)currentKick1ComboStage;
                Debug.Log(btn);
                return;
            }

            if (currentKick1ComboStage == Kick1ComboStage.LightKick)
            {
                currentKick1ComboStage = Kick1ComboStage.MediumKick;
                StartCoroutine(ResetCombo());
            }
            else if (currentKick1ComboStage == Kick1ComboStage.MediumKick)
            {
                StopAllCoroutines();
                currentKick1ComboStage = Kick1ComboStage.HeavyKick;
                StartCoroutine(ResetCombo());
            }
            else if (currentKick1ComboStage == Kick1ComboStage.HeavyKick)
            {
                StopAllCoroutines();
                currentKick1ComboStage = Kick1ComboStage.LightKick;
            }
        }

        private void OnPunch2(InputAction.CallbackContext ctx)
        {
            btnInput = ctx.ReadValueAsButton();

            if (btnInput)
            {
                btn = (ButtonPress)(byte)currentPunch2ComboStage;
                Debug.Log(btn);
                return;
            }

            if (currentPunch2ComboStage == Punch2ComboStage.LightPunch2)
            {
                currentPunch2ComboStage = Punch2ComboStage.MediumPunch2;
                StartCoroutine(ResetCombo());
            }
            else if (currentPunch2ComboStage == Punch2ComboStage.MediumPunch2)
            {
                StopAllCoroutines();
                currentPunch2ComboStage = Punch2ComboStage.LightPunch2;
                
            }
        }

        private void OnKick2(InputAction.CallbackContext ctx)
        {
            btnInput = ctx.ReadValueAsButton();

            if (btnInput)
            {
                btn = (ButtonPress)(byte)currentKick2ComboStage;
                Debug.Log(btn);
                return;
            }

            if (currentKick2ComboStage == Kick2ComboStage.LightKick2)
            {
                currentKick2ComboStage = Kick2ComboStage.MediumKick2;
                StartCoroutine(ResetCombo());
            }
            else if (currentKick2ComboStage == Kick2ComboStage.MediumKick2)
            {
                StopAllCoroutines();
                currentKick2ComboStage = Kick2ComboStage.HeavyKick2;
                StartCoroutine(ResetCombo());
            }
            else if (currentKick2ComboStage == Kick2ComboStage.HeavyKick2)
            {
                StopAllCoroutines();
                currentKick2ComboStage = Kick2ComboStage.LightKick2;
            }
        }


        private void OnSpecialMove(InputAction.CallbackContext ctx)
        {
            btnInput = ctx.ReadValueAsButton();
            btn = ButtonPress.Button9;
        }

        private void PauseGame(InputAction.CallbackContext ctx)
        {
            btnInput = ctx.ReadValueAsButton();
            btn = ButtonPress.Start;
        }


        IEnumerator ResetCombo()
        {
            yield return new WaitForSeconds(comboResetDelay);
            currentPunch1ComboStage = Punch1ComboStage.LightPunch;
            currentKick1ComboStage = Kick1ComboStage.LightKick;
            currentPunch2ComboStage = Punch2ComboStage.LightPunch2;
            currentKick2ComboStage = Kick2ComboStage.LightKick2;
        }

        public override InputEvents ReadInput(InputReferences inputReference)
        {
            if (inputReference != null)
            {
                if (inputReference.inputType == InputType.HorizontalAxis)
                {
                    return new InputEvents(movement.x);
                }
                else if (inputReference.inputType == InputType.VerticalAxis)
                {
                    return new InputEvents(movement.y);
                }

                if (inputReference.inputType == InputType.Button && inputReference.engineRelatedButton == btn)
                {
                    return new InputEvents(btnInput);
                    
                }
            }
            return InputEvents.Default;
        }
    }
}