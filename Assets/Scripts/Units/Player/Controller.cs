using System.Collections;
using Units.Weapon;
using UnityEngine;

namespace Units.Player
{
    public class Controller : MonoBehaviour
    {
        public enum ControllerState
        {
            InMenu,
            ExploreWorld
        }

        public float speed = 1;
        public float rotationSpeed = 100;
        public  int fallPositionY = 0;

        public ControllerState state = ControllerState.ExploreWorld;

        public Weapon.Weapon weapon;
        
        private new Rigidbody rigidbody;
        private new Camera camera;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            camera = Camera.main;

            //TODO отписаться от события смерти, когда появится меню, должно быть реализовано иначе
            GlobalEventManager.PlayerDeath.AddListener(() => state = ControllerState.InMenu);

            weapon = gameObject.AddComponent<SowBall>();
        }

        private void FixedUpdate()
        {
            switch(state)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    SendEventIfPlayerFell();
                    MovePlayer();
                    RotateToCursor();
                    DrawPlayerForwardRay();
                    break;
            }
        }

        private void SendEventIfPlayerFell()
        {
            if (transform.position.y < fallPositionY) 
            {
                GlobalEventManager.UnitFellEvent.Invoke(gameObject);
            }
        }

        private void RotateToCursor()
        {
            var mousePos = GetMousePosition();
            var lookRotation = Quaternion.LookRotation(mousePos - transform.position);
            lookRotation.z = 0;
            lookRotation.x = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
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
            switch(state)
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
            weapon.Attack();
        }

        private void MovePlayer()
        {
            var horizontalDelta = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
            var verticalDelta = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
            
            rigidbody.velocity = new Vector3(verticalDelta, rigidbody.velocity.y, -horizontalDelta);
        }
    }
}