namespace Assets.Scripts
{
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

        public float MinCameraDistance = 15f;
        public float ZoomInMargin = 40f;
        public float ZoomOutMargin = 20f;

        private Camera _camera;
        private Vector3 _targetPosition;
        private float _offsetToCenter;

        [SerializeField]
        private float _currentCameraDistance;

        private void Awake()
        {
            this._camera = this.GetComponent<Camera>();
            this._offsetToCenter = this.CenterTransform.position.z + this.transform.position.z;
        }

        private void Update()
        {
            int height = this._camera.pixelHeight;
            int width = this._camera.pixelWidth;
            if (this.PlayerFight)
                this.PlayerFightUpdate(width, height);
            // TODO: LERP Camera with this._targetPosition
            if (this.transform.position != this._targetPosition)
                this.transform.position = this._targetPosition;
        }

        private void PlayerFightUpdate(int width, int height)
        {
            Rect zoomOutRect = new Rect(this.ZoomOutMargin, this.ZoomOutMargin, width - this.ZoomOutMargin, height - this.ZoomOutMargin);
            Rect zoomInRect = new Rect(this.ZoomInMargin, this.ZoomInMargin, width - this.ZoomInMargin, height - this.ZoomInMargin);
            this._currentCameraDistance = Vector3.Distance(this.transform.position, this.CenterTransform.position);
            List<Transform> transformsOutOfBounds = this.CheckObjectsOutOfCameraBounds(zoomOutRect);
            if (transformsOutOfBounds.Count > 0)
            {
                this.ZoomOut(this._targetPosition);
            }
            else if (this.CheckObjectsInOfCameraBounds(zoomInRect).Count == this.Targets.Length
                && this._currentCameraDistance > this.MinCameraDistance)
            {
                this.ZoomIn(this._targetPosition);
            }
            else
                this.SetMiddle();
        }

        private void SetMiddle()
        {
            if (this.Logging)
                LogHelper.Log(typeof(GameCamera), "Moving camera");
            Vector3 middlePoint = Vector3.zero;
            foreach (Transform t in this.Targets)
            {
                Vector3 v = t.position;
                middlePoint += v;
            }
            middlePoint = middlePoint / this.Targets.Length;
            this._targetPosition = new Vector3(middlePoint.x, this.transform.position.y, this.transform.position.z);// middlePoint.z + this._offsetToCenter);
            this.CenterTransform.position = middlePoint;
        }

        private void ZoomIn(Vector3 targetPos)
        {
            this._targetPosition = new Vector3(targetPos.x, targetPos.y - 1, targetPos.z + 1);
            if (this.Logging)
                LogHelper.Log(typeof(GameCamera), "Zooming in..");
        }
        private void ZoomOut(Vector3 targetPos)
        {
            this._targetPosition = new Vector3(targetPos.x, targetPos.y + 1, targetPos.z - 1);
            if (this.Logging)
                LogHelper.Log(typeof(GameCamera), "Zooming out..");
        }

        private List<Transform> CheckObjectsOutOfCameraBounds(Rect boundingBox)
        {
            List<Transform> transformsOutOfBounds = new List<Transform>();
            Vector3[] positions = new Vector3[this.Targets.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = this._camera.WorldToScreenPoint(this.Targets[i].position);
            }

            for (int i = 0; i < positions.Length; i++)
            {
                if (!boundingBox.Contains(positions[i]))
                    transformsOutOfBounds.Add(this.Targets[i]);
            }
            return transformsOutOfBounds;
        }
        private List<Transform> CheckObjectsInOfCameraBounds(Rect boundingBox)
        {
            List<Transform> transformsOutOfBounds = new List<Transform>();
            Vector3[] positions = new Vector3[this.Targets.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = this._camera.WorldToScreenPoint(this.Targets[i].position);
            }

            for (int i = 0; i < positions.Length; i++)
            {
                if (boundingBox.Contains(positions[i]))
                    transformsOutOfBounds.Add(this.Targets[i]);
            }
            return transformsOutOfBounds;
        }
    }
}
