namespace Assets.Scripts
{
    using System.Collections.Generic;
    using Assets.Scripts.Helpers;
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviour
    {
        public Transform playerOne;
        public Transform playerTwo;
        public float maxZoom = 50f;
        public float minZoom = 20f;
        public float zoomFactor = 0.2f;
        public float zoom = 30f;

        private void FixedUpdate()
        {
            transform.position = (playerOne.position + playerTwo.position) / 2f;
            transform.position += transform.forward * -zoom;

            var camera = GetComponent<Camera>();
            var p1ScreenPos = camera.WorldToScreenPoint(playerOne.position);
            var p2ScreenPos = camera.WorldToScreenPoint(playerTwo.position);
            p1ScreenPos = camera.ScreenToViewportPoint(p1ScreenPos);
            p2ScreenPos = camera.ScreenToViewportPoint(p2ScreenPos);

            var zoomOutRect = new Rect(0.1f, 0.1f, 0.9f, 0.9f);
            var zoomInRect = new Rect(0.2f, 0.2f, 0.8f, 0.8f);

            if (!zoomOutRect.Contains(p1ScreenPos) || !zoomOutRect.Contains(p2ScreenPos))
            {
                if(zoom < maxZoom)
                    zoom += zoomFactor;
            }
            if (zoomInRect.Contains(p1ScreenPos) && zoomInRect.Contains(p2ScreenPos))
            {
                if (zoom > minZoom)
                    zoom -= zoomFactor;
            }
        }
    }
}
