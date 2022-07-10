using System;
using Units.Weapon;
using UnityEngine;

namespace Units.Player
{
    public class Controller : MonoBehaviour
    {
        public float Speed = 1;
        public float RotationSpeed = 100;
        public  int FallPositionY = 0;

        public ControllerState State = ControllerState.ExploreWorld;
        public ControllerType ControllerType = ControllerType.Mouse;

        public Weapon.Weapon Weapon;
        
        private new Rigidbody rigidbody;
        private new Camera camera;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            camera = Camera.main;

            //TODO отписаться от события смерти, когда появится меню, должно быть реализовано иначе
            GlobalEventManager.PlayerDeath.AddListener(() => State = ControllerState.InMenu);

            Weapon = gameObject.AddComponent<SowBall>();
            SetControllerType();
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
            switch(State)
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
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotationSpeed * Time.fixedDeltaTime);
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
            switch(State)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack();
                    }
                    break;
            }
        }

        private void Attack()
        {
            Weapon.Attack();
        }

        private void MovePlayer()
        {
            var horizontalDelta = Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime;
            var verticalDelta = Input.GetAxis("Vertical") * Speed * Time.fixedDeltaTime;
            
            rigidbody.velocity = new Vector3(verticalDelta, rigidbody.velocity.y, -horizontalDelta);
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