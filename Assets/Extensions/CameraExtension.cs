using UnityEngine;

namespace Extensions
{
    public static class CameraExtension
    {
        public static Vector3 GetMousePosition(this Camera camera, float raycastPlaneHeight)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, new Vector3(0,raycastPlaneHeight,0));

            plane.Raycast(ray, out var hitInfo);
            return ray.GetPoint(hitInfo);
        }
    }
}