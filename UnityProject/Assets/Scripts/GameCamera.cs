namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using Assets.Scripts.Helpers;
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviour
    {


        public bool PlayerFight = true;
        public bool Logging = true;
        public Transform[] Targets;
        public Transform CenterTransform;
        public Transform Boss;

        public float MinCameraDistance = 15f;

        public float ZoomInMargin = 40f;
        public float ZoomOutMargin = 20f;
        public float ZoomPerLerp = 5f;

        private Camera _camera;
        private Vector3 _targetPosition;
        private float _offsetToCenter;

        [SerializeField]
        private float _currentCameraDistance;


        #region Lerp
        private Vector3 _startMarker = Vector3.zero;
        private Vector3 _endMarker = Vector3.zero;
        public float CameraSpeed = 5.0F;
        private float _startTime;
        private float _journeyLength;
        #endregion

        private void Awake()
        {
            this._camera = this.GetComponent<Camera>();
            this._offsetToCenter = this.CenterTransform.position.z + this.transform.position.z;
            this.InvokeRepeating("CheckIfCameraShouldMove", 0f, 0.5f);
        }

        private void CheckIfCameraShouldMove()
        {
            int height = this._camera.pixelHeight;
            int width = this._camera.pixelWidth;
            if (this.PlayerFight)
                this.PlayerFightUpdate(width, height);
            else
                this.BossFightUpdate(width, height);
        }

        private void Update()
        {
            if (this._endMarker == Vector3.zero) return;
            this._journeyLength = Vector3.Distance(this._startMarker, this._endMarker);
            float distCovered = (Time.time - this._startTime) * this.CameraSpeed;
            float fracJourney = distCovered / this._journeyLength;
            this.transform.position = Vector3.Lerp(this._startMarker, this._endMarker, fracJourney);
        }

        private void PlayerFightUpdate(int width, int height)
        {
            Rect zoomOutRect = new Rect(this.ZoomOutMargin, this.ZoomOutMargin, width - this.ZoomOutMargin, height - this.ZoomOutMargin);
            Rect zoomInRect = new Rect(this.ZoomInMargin, this.ZoomInMargin, width - this.ZoomInMargin, height - this.ZoomInMargin);
            this._currentCameraDistance = Vector3.Distance(this.transform.position, this.CenterTransform.position);
            this.SetMiddle(this.Targets);
            List<Transform> transformsOutOfBounds = this.CheckObjectsOutOfCameraBounds(zoomOutRect, this.Targets);
            if (transformsOutOfBounds.Count > 0)
            {
                this.ZoomOut(this._targetPosition);
            }
            else if (this.CheckObjectsInOfCameraBounds(zoomInRect, this.Targets).Count == this.Targets.Length
                && this._currentCameraDistance > this.MinCameraDistance)
            {
                this.ZoomIn(this._targetPosition);
            }

        }
        private void BossFightUpdate(int width, int height)
        {
            float maxX = float.MinValue;
            float minX = float.MaxValue;
            float maxZ = float.MinValue;
            float minZ = float.MaxValue;
            Transform[] bossTransforms = this.Boss.transform.GetComponentsInChildren<Transform>();
            Transform[] transforms = new Transform[this.Targets.Length + bossTransforms.Length];
            for (int i = 0; i < this.Targets.Length; i++)
                transforms[i] = this.Targets[i];
            for (int i = 0; i < bossTransforms.Length; i++)
                transforms[this.Targets.Length + i] = bossTransforms[i];
            foreach (Transform t in transforms)
            {
                if (maxX < t.position.x)
                    maxX = t.position.x;
                if (minX > t.position.x)
                    minX = t.position.x;
                if (maxZ < t.position.z)
                    maxZ = t.position.z;
                if (minZ > t.position.z)
                    maxZ = t.position.z;
            }
            Vector3 middlePoint = new Vector3((minX + maxX) / 2, transforms[1].position.y, (minZ + maxZ) / 2);
            this.CenterTransform.position = middlePoint;
            Rect zoomOutRect = new Rect(this.ZoomOutMargin, this.ZoomOutMargin, width - this.ZoomOutMargin, height - this.ZoomOutMargin);
            Rect zoomInRect = new Rect(this.ZoomInMargin, this.ZoomInMargin, width - this.ZoomInMargin, height - this.ZoomInMargin);
            this._currentCameraDistance = Vector3.Distance(this.transform.position, this.CenterTransform.position);
            List<Transform> transformsOutOfBounds = this.CheckObjectsOutOfCameraBounds(zoomOutRect, transforms);
            if (transformsOutOfBounds.Count > 0)
            {
                this.ZoomOut(this._targetPosition);
            }
            else if (this.CheckObjectsInOfCameraBounds(zoomInRect, transforms).Count == this.Targets.Length
                && this._currentCameraDistance > this.MinCameraDistance)
            {
                this.ZoomIn(this._targetPosition);
            }
            else
                this.SetMiddle(transforms);
        }

        private void SetMiddle(IList<Transform> transforms)
        {
            Vector3 middlePoint = Vector3.zero;
            foreach (Transform t in transforms)
            {
                Vector3 v = t.position;
                middlePoint += v;
            }
            middlePoint = middlePoint / transforms.Count;
            this._targetPosition = new Vector3(middlePoint.x, this.transform.position.y, this.transform.position.z);// middlePoint.z + this._offsetToCenter);

            if (this._targetPosition != this.transform.position)
            {
                this._startTime = Time.time;
                this._startMarker = this.transform.position;
                this._endMarker = this._targetPosition;
            }
            this.CenterTransform.position = middlePoint;
        }

        private void ZoomIn(Vector3 targetPos)
        {
            this._targetPosition = new Vector3(targetPos.x, targetPos.y - this.ZoomPerLerp, targetPos.z + this.ZoomPerLerp);
            this._startTime = Time.time;
            this._startMarker = this.transform.position;
            this._endMarker = this._targetPosition;
            if (this.Logging)
                LogHelper.Log(typeof(GameCamera), "Zooming in..");
        }
        private void ZoomOut(Vector3 targetPos)
        {
            this._targetPosition = new Vector3(targetPos.x, targetPos.y + this.ZoomPerLerp, targetPos.z - this.ZoomPerLerp);
            this._startTime = Time.time;
            this._startMarker = this.transform.position;
            this._endMarker = this._targetPosition;
            if (this.Logging)
                LogHelper.Log(typeof(GameCamera), "Zooming out..");
        }

        private List<Transform> CheckObjectsOutOfCameraBounds(Rect boundingBox, IList<Transform> transforms)
        {
            List<Transform> transformsOutOfBounds = new List<Transform>();
            Vector3[] positions = new Vector3[transforms.Count];
            for (int i = 0; i < transforms.Count; i++)
            {
                positions[i] = this._camera.WorldToScreenPoint(transforms[i].position);
            }
            for (int i = 0; i < positions.Length; i++)
            {
                if (!boundingBox.Contains(positions[i]))
                    transformsOutOfBounds.Add(transforms[i]);
            }
            return transformsOutOfBounds;
        }
        private List<Transform> CheckObjectsInOfCameraBounds(Rect boundingBox, IList<Transform> transforms)
        {
            List<Transform> transformsOutOfBounds = new List<Transform>();
            Vector3[] positions = new Vector3[transforms.Count];
            for (int i = 0; i < transforms.Count; i++)
            {
                positions[i] = this._camera.WorldToScreenPoint(transforms[i].position);
            }
            for (int i = 0; i < positions.Length; i++)
            {
                if (boundingBox.Contains(positions[i]))
                    transformsOutOfBounds.Add(transforms[i]);
            }
            return transformsOutOfBounds;
        }
    }
}
