using System;
using Units.Player.Weapon.SnowBall;
using UnityEngine;
using UnityEngine.AI;

namespace Units.Player
{
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

    public class Controller : MonoBehaviour
    {
        private const int MovingAnimationSensitivity = 25;
        public int FallPositionY = 0;

        public ControllerState State = ControllerState.ExploreWorld;
        public ControllerType ControllerType = ControllerType.Mouse;

        public SpellBase Weapon;

        private new Rigidbody rigidbody;
        private new Camera camera;
        private PlayerStats playerStats;
        private Animator animator;

        private static readonly int MovingTriggerName = Animator.StringToHash("MovingState");

        private UnitMovingSound playerSounds;

        private float Speed => 100 + playerStats.Agility * 50;
        private float RotationSpeed => 20 + playerStats.Agility * 10;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            camera = Camera.main;

            playerStats = GetComponent<PlayerStats>();

            GlobalEventManager.PlayerDeath.AddListener(() => State = ControllerState.InMenu);

            Weapon = gameObject.AddComponent<SowBall>();
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
                    SendEventIfPlayerFell();
                    MovePlayer();
                    Rotate();
                    DrawPlayerForwardRay();
                    break;
            }
        }

        public void FinishCastingAnimation() //вызывается из анимации окончания атаки
        {
            Weapon.FinishCasting();
        }

        private void SendEventIfPlayerFell()
        {
            if (transform.position.y < FallPositionY)
            {
                GlobalEventManager.UnitFellEvent.Invoke(gameObject);
            }
        }

        private void Rotate()
        {
            switch (ControllerType)
            {
                case ControllerType.Mouse:
                    var mousePos = GetMousePosition();
                    var lookRotation = Quaternion.LookRotation(mousePos - transform.position);
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

        private void DrawPlayerForwardRay()
        {
            var playerTransform = transform;
            var forward = playerTransform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(playerTransform.position, forward, Color.green);
        }

        private Vector3 GetMousePosition()
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, transform.position);

            plane.Raycast(ray, out var hitInfo);
            return ray.GetPoint(hitInfo);
        }

        private void Update()
        {
            switch (State)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    if (Input.GetMouseButton(0))
                    {
                        Weapon.StartAttack();
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        Weapon.CancelAttack();
                    }

                    break;
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