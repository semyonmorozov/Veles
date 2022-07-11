using System;
using Units.Weapon;
using UnityEngine;

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
        public int DefaultMoveSpeed = 200;
        public int MoveSpeedAgilityMultiplier = 100;
        public int DefaultRotationSpeed = 20;
        public int RotationSpeedAgilityMultiplier = 10;

        public int FallPositionY = 0;

        public ControllerState State = ControllerState.ExploreWorld;
        public ControllerType ControllerType = ControllerType.Mouse;

        public Weapon.SpellBase Weapon;

        private new Rigidbody rigidbody;
        private new Camera camera;
        private PlayerStats playerStats;
        private Animator animator;
        
        private static readonly int MovingTriggerName = Animator.StringToHash("MovingState");
        public float LocVelocityZ;
        public float LocVelocityX;
        private float Speed => DefaultMoveSpeed + playerStats.Agility * MoveSpeedAgilityMultiplier;
        private float RotationSpeed => DefaultRotationSpeed + playerStats.Agility * RotationSpeedAgilityMultiplier;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            camera = Camera.main;

            playerStats = GetComponent<PlayerStats>();

            GlobalEventManager.PlayerDeath.AddListener(() => State = ControllerState.InMenu);

            Weapon = gameObject.AddComponent<SowBall>();
            SetControllerType();
            animator = GetComponent<Animator>();
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
                    AnimateMoving();
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

            rigidbody.velocity = new Vector3(verticalDelta, rigidbody.velocity.y, -horizontalDelta);
        }

        private void AnimateMoving()
        {
            var ninetyDegreesQuaternion = Quaternion.AngleAxis(90, Vector3.up);
            var locVelocity = ninetyDegreesQuaternion * transform.InverseTransformDirection(rigidbody.velocity);
            LocVelocityZ = locVelocity.z;
            var horizontalDelta = LocVelocityZ;
            LocVelocityX = locVelocity.x;
            var verticalDelta = LocVelocityX;
            var newMovingState = horizontalDelta switch
            {
                > 5 => verticalDelta switch
                {
                    > 5 => MovingState.RunRight,
                    < -5 => MovingState.RunBackwardRight,
                    _ => MovingState.StrafeRight
                },
                < -5 => verticalDelta switch
                {
                    > 5 => MovingState.RunLeft,
                    < -5 => MovingState.RunBackwardLeft,
                    _ => MovingState.StrafeLeft
                },
                _ => verticalDelta switch
                {
                    > 1 => MovingState.RunForward,
                    < -1 => MovingState.RunBackward,
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