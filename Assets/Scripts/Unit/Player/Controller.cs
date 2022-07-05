using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

namespace Unit.Player
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
        public LayerMask groundMask;

        public ControllerState state = ControllerState.ExploreWorld;

        public Rigidbody shell;
        
        private new Rigidbody rigidbody;
        private new Camera camera;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            camera = Camera.main;

            //TODO отписаться от события смерти, когда появится меню, должно быть реализовано иначе
            GlobalEventManager.PlayerDeath.AddListener(() => state = ControllerState.InMenu); 
        }

        private void FixedUpdate()
        {
            switch(state)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    MovePlayer();
                    RotateToCursor();
                    DrawPlayerForwardRay();
                    break;
            }
        }

        private void RotateToCursor()
        {
            var mousePos = GetMousePosition().position;
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

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
 
            return Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask) 
                ? (success: true, position: hitInfo.point) 
                : (success: false, position: Vector3.zero);
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
            var playerTransform = transform;
            Instantiate(shell, playerTransform.position, playerTransform.rotation);
        }

        private void MovePlayer()
        {
            var horizontalDelta = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
            var verticalDelta = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
            
            rigidbody.velocity = new Vector3(verticalDelta, 0, -horizontalDelta);
        }
    }
}