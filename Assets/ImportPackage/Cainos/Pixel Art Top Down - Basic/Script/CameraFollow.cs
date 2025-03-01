﻿using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic {
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float lerpSpeed = 10.0f;

        private Vector3 offset;

        private Vector3 targetPos;

        private void Start()
        {
            if (target == null) return;

            offset = transform.position - target.position;
        }

        private void FixedUpdate() {
            if (target == null) return;

            targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }

    }
}
