using System;
using System.Collections;
using Main.Extensions;
using Main.Units.Player.Weapon;
using Main.Units.Player.Weapon.SnowBall;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Units.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const int MovingAnimationSensitivity = 25;

        public ControllerState State = ControllerState.ExploreWorld;
        public ControllerType ControllerType = ControllerType.Mouse;

        public SpellBase Weapon;

        private new Rigidbody rigidbody;
        private new Camera camera;
        private PlayerMainStats playerMainStats;
        private Animator animator;

        private static readonly int MovingTriggerName = Animator.StringToHash("MovingState");

        private UnitMovingSound playerSounds;
        private IEnumerator switchWeapon;
        private EventSystem eventSystem;

        private float Speed => 100 + playerMainStats.Agility * 50;
        private float RotationSpeed => 20 + playerMainStats.Agility * 10;

        private void Awake()
        {
            eventSystem = EventSystem.current;
            rigidbody = GetComponent<Rigidbody>();
            camera = Camera.main;

            playerMainStats = GetComponent<PlayerMainStats>();

            switchWeapon = SwitchWeapon();
            switchWeapon.MoveNext();
            SetControllerType();
            animator = GetComponent<Animator>();

            playerSounds = GetComponent<UnitMovingSound>();
        }

        private void SetControllerType()
        {
            foreach (var x in Input.GetJoystickNames())
            {
                if (x.Contains("Xbox"))
                {
                    ControllerType = ControllerType.XboxGamePad;
                }
            }
        }

        private void FixedUpdate()
        {
            switch (State)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    MovePlayer();
                    Rotate();
                    break;
            }
        }

        private void Rotate()
        {
            switch (ControllerType)
            {
                case ControllerType.Mouse:
                    var transformPosition = transform.position;
                    var mousePos = camera.GetMousePosition(transformPosition.y);
                    var lookRotation = Quaternion.LookRotation(mousePos - transformPosition);
                    lookRotation.z = 0;
                    lookRotation.x = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
                        RotationSpeed * Time.fixedDeltaTime);
                    break;
                case ControllerType.XboxGamePad:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            switch (State)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    
                    if (Input.GetMouseButton(0) && !eventSystem.IsPointerOverGameObject(-1))
                    {
                        Weapon.StartAttack();
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        Weapon.CancelAttack();
                    }

                    if (Input.GetMouseButtonDown(1) && !eventSystem.IsPointerOverGameObject())
                    {
                        switchWeapon.MoveNext();
                    }

                    break;
            }
        }

        private IEnumerator SwitchWeapon()
        {
            while (true)
            {
                Destroy(gameObject.GetComponent<SpellBase>());
                Weapon = gameObject.AddComponent<SnowBall>();
                yield return null;
                Destroy(gameObject.GetComponent<SpellBase>());
                Weapon = gameObject.AddComponent<FireBall>();
                yield return null;
            }
        } 

        private void MovePlayer()
        {
            var horizontalDelta = Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime;
            var verticalDelta = Input.GetAxis("Vertical") * Speed * Time.fixedDeltaTime;

            if (Math.Abs(horizontalDelta) > 0.1f || Math.Abs(verticalDelta) > 0.1f)
            {
                playerSounds.PlayMovingSounds();
                rigidbody.velocity = new Vector3(verticalDelta, rigidbody.velocity.y, -horizontalDelta);
            }
            else
            {
                rigidbody.velocity -= rigidbody.velocity * 0.5f;
                playerSounds.StopMovingSounds();
            }
            AnimateMoving(horizontalDelta, verticalDelta);
        }

        private void AnimateMoving(float horizontalDelta, float verticalDelta)
        {
            var direction = new Vector3(-horizontalDelta,0, -verticalDelta);
            
            var locVelocity = transform.InverseTransformDirection(direction).normalized;

            var normalizedHorizontalDelta = (int)(locVelocity.z * 100);

            var normalizedVerticalDelta = (int)(locVelocity.x * 100);

            var newMovingState = normalizedHorizontalDelta switch
            {
                > MovingAnimationSensitivity => normalizedVerticalDelta switch
                {
                    > MovingAnimationSensitivity => MovingState.RunRight,
                    < -MovingAnimationSensitivity => MovingState.RunBackwardRight,
                    _ => MovingState.StrafeRight
                },
                < -MovingAnimationSensitivity => normalizedVerticalDelta switch
                {
                    > MovingAnimationSensitivity => MovingState.RunLeft,
                    < -MovingAnimationSensitivity => MovingState.RunBackwardLeft,
                    _ => MovingState.StrafeLeft
                },
                _ => normalizedVerticalDelta switch
                {
                    > 10 => MovingState.RunForward,
                    < -10 => MovingState.RunBackward,
                    _ => MovingState.Idle
                }
            };

            animator.SetInteger(MovingTriggerName, (int)newMovingState);
        }
    }

    public enum MovingState
    {
        Idle = 0,
        RunForward = 1,
        RunRight = 2,
        RunLeft = 3,
        RunBackward = 4,
        RunBackwardLeft = 5,
        RunBackwardRight = 6,
        StrafeRight = 7,
        StrafeLeft = 8
    }

    public enum ControllerType
    {
        Mouse,
        XboxGamePad
    }

    public enum ControllerState
    {
        InMenu,
        ExploreWorld
    }
}