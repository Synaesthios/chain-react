using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);


        private void LateUpdate()
        {
            transform.position = GetUpdatedPosition();
        }

        public Vector3 GetUpdatedPosition() {
            return target.position + offset;
        }
    }
}
