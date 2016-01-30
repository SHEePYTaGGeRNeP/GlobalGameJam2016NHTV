namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Assets.Scripts.Helpers;

    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    internal class GameCamera : MonoBehaviour
    {
        enum CameraMovement { None, Left, Right, Up, Down, ZoomIn, ZoomOut }
        private CameraMovement _prevCameraMovement;

        [SerializeField]
        private Transform[] _targets;
        [SerializeField]
        private Transform _centerTransform;
        

        public int MovementMargin = 20;

        private Camera _camera;

        private Vector3 _targetPosition;

        private Vector3 _cameraStartPosition;
        private Vector3 middlePoint;
        private float distanceFromMiddlePoint;
        private float distanceBetweenPlayers;


        private void Awake()
        {
            this._camera = this.GetComponent<Camera>();
            this._cameraStartPosition = this.transform.position;
        }

        private void Update()
        {
            CameraMovement newCameraMovement = this._prevCameraMovement;
            Vector3 middlePoint = Vector3.zero;
            foreach (Transform t in this._targets)
            {
                Vector3 v = t.position;
                middlePoint += v;
            }
            middlePoint = middlePoint / this._targets.Length;
            this._targetPosition = new Vector3(middlePoint.x, this.transform.position.y, this.transform.position.z);
            this._centerTransform.position = middlePoint;


            // Zoom doesn't work so it's commented out.

            //int height = this._camera.pixelHeight;
            //int width = this._camera.pixelWidth;
            //Rect zoomOutRect = new Rect(0, 0, width, height);
            //Rect zoomInRect = new Rect(40 + this.MovementMargin, 40 + this.MovementMargin, width - 80 - this.MovementMargin, height - 80 - this.MovementMargin);
            //List<Transform> transformsOutOfBounds = this.CheckObjectsOutOfCameraBounds(zoomOutRect);
            //if (transformsOutOfBounds.Count > 0)
            //{
            //    this.ZoomOut(this._targetPosition);
            //    newCameraMovement = CameraMovement.ZoomOut;
            //}
            //else if (this.CheckObjectsInOfCameraBounds(zoomInRect).Count == this._targets.Length)
            //{
            //    this.ZoomIn(this._targetPosition);
            //    newCameraMovement = CameraMovement.ZoomIn;
            //}
            //else
            //    Debug.Log("Not zooming in");



            if (this.transform.position != this._targetPosition)
                this.transform.position = this._targetPosition;

            if (newCameraMovement == this._prevCameraMovement)
                this._prevCameraMovement = CameraMovement.None;
            else
                this._prevCameraMovement = newCameraMovement;
        }


        private void ZoomIn(Vector3 targetPos)
        {
            LogHelper.Log(typeof(GameCamera), "Zooming back in..");
            this._targetPosition = new Vector3(targetPos.x, targetPos.y - 1, targetPos.z + 1);
        }

        private void ZoomOut(Vector3 targetPos)
        {
            LogHelper.Log(typeof(GameCamera), "Zooming out..");
            this._targetPosition = new Vector3(targetPos.x, targetPos.y + 1, targetPos.z - 1);
        }

        private List<Transform> CheckObjectsOutOfCameraBounds(Rect boundingBox)
        {
            List<Transform> transformsOutOfBounds = new List<Transform>();
            Vector3[] positions = new Vector3[this._targets.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = this._camera.WorldToScreenPoint(this._targets[i].position);
            }

            for (int i = 0; i < positions.Length; i++)
            {
                if (!boundingBox.Contains(positions[i]))
                    transformsOutOfBounds.Add(this._targets[i]);
            }
            return transformsOutOfBounds;
        }
        private List<Transform> CheckObjectsInOfCameraBounds(Rect boundingBox)
        {
            List<Transform> transformsOutOfBounds = new List<Transform>();
            Vector3[] positions = new Vector3[this._targets.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = this._camera.WorldToScreenPoint(this._targets[i].position);
            }

            for (int i = 0; i < positions.Length; i++)
            {
                if (boundingBox.Contains(positions[i]))
                    transformsOutOfBounds.Add(this._targets[i]);
            }
            return transformsOutOfBounds;
        }
    }
}
