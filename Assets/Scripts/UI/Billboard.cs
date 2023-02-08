using System;
using UnityEngine;

namespace UI
{
    public class Billboard : MonoBehaviour
    {
        private Quaternion _originalRot;

        private void Start()
        {
            _originalRot = transform.rotation;
        }

        private void Update()
        {
            transform.rotation = Camera.main.transform.rotation * _originalRot;
        }
    }
}
