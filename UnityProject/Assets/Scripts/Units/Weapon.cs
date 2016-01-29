namespace Assets.Scripts.Units
{
    using UnityEngine;
    class Weapon : MonoBehaviour
    {

        public float speed = 1.0F;
        public float Forward = 1f;
        private Vector3 _startPos;

        private Vector3 startMarker;
        private Vector3 endMarker;
        private float startTime;
        private float journeyLength;
        private bool _returning = false;
        private bool _done = false;
        void Awake()
        {
            this._startPos = this.transform.position;
        }

        public void Attack()
        {
            this.startTime = Time.time;
            this.startMarker = this._startPos;
            this.endMarker = new Vector3(this._startPos.x, this._startPos.y, this._startPos.z + this.Forward);
            this.journeyLength = Vector3.Distance(this.startMarker, this.endMarker);
            this._returning = false;
            this._done = false;
        }
        private void ReturnToOriginal()
        {
            this._returning = true;
            this.startTime = Time.time;
            this.startMarker = this.transform.position;
            this.endMarker = this._startPos;
            this.journeyLength = Vector3.Distance(this.startMarker, this.endMarker);
        }
        void Update()
        {
            if (this.startMarker == Vector3.zero || this._done) return;
            float distCovered = (Time.time - this.startTime) * this.speed;
            float fracJourney = distCovered / this.journeyLength;
            this.transform.position = Vector3.Lerp(this.startMarker, this.endMarker, fracJourney);

            if (this.transform.position == this.endMarker && !this._returning)
                this.ReturnToOriginal();
            else if (this.transform.position == this.endMarker)
                this._done = true;

               
        }

    }
}
