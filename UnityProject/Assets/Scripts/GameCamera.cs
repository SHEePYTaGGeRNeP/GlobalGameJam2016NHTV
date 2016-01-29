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
                Vector3 v = t.position;// this._camera.WorldToScreenPoint(t.position);
                middlePoint += v;
            }
            middlePoint = middlePoint / this._targets.Length;
            this._targetPosition = new Vector3(middlePoint.x, this.transform.position.y, this.transform.position.z);
            this._centerTransform.position = middlePoint;
            //this.transform.position = middlePoint;

            int height = this._camera.pixelHeight;
            int width = this._camera.pixelWidth;
            List<Transform> transformsOutOfBounds = this.CheckObjectsOutOfCameraBounds(height, width);
            if (transformsOutOfBounds.Count > 0)
            {
                this.ZoomOut(this._targetPosition);
                newCameraMovement = CameraMovement.ZoomOut;
            }
        
            else if (this.transform.position.y != this._cameraStartPosition.y && this.transform.position.z != this._cameraStartPosition.z
                && this._prevCameraMovement != CameraMovement.ZoomOut)
            {
                if (this.CheckObjectsOutOfCameraBounds(width - 2, height - 2).IsNullOrEmpty())
                {
                    this.ZoomIn(this._targetPosition);
                    newCameraMovement = CameraMovement.ZoomIn;
                }
                else
                    Debug.Log("Didnt zoom in !");
            }


            if (this.transform.position != this._targetPosition)
                this.transform.position = this._targetPosition;

            if (newCameraMovement == this._prevCameraMovement)
                this._prevCameraMovement = CameraMovement.None;
            else
                this._prevCameraMovement = newCameraMovement;



            //Vector3 newCameraPos = Camera.main.transform.position;
            //newCameraPos.x = middlePoint.x;
            //Camera.main.transform.position = newCameraPos;

            //// Find the middle point between players.
            //Vector3 vectorBetweenPlayers = player2.position - player1.position;
            //middlePoint = player1.position + 0.5f * vectorBetweenPlayers;

            //// Calculate the new distance.
            //distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
            //cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

            //// Set camera to new position.
            //Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
            //Camera.main.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);

            /*
            CameraMovement newCameraMovement = this._prevCameraMovement;
            this._targetPosition = this.transform.position;

                */

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

        private List<Transform> CheckObjectsOutOfCameraBounds(int height, int width)
        {
            List<Transform> transformsOutOfBounds = new List<Transform>();
            Vector3[] positions = new Vector3[this._targets.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = this._camera.WorldToScreenPoint( this._targets[i].position);
            }

            for (int i = 0; i < positions.Length; i++)
            {
                Rect cameraRectangle = new Rect(0 + this.MovementMargin, 0 + this.MovementMargin, width - this.MovementMargin, height - this.MovementMargin);
                if (!cameraRectangle.Contains(positions[i]))
                    transformsOutOfBounds.Add(this._targets[i]);
            }
            return transformsOutOfBounds;
        }
    }
}
