using System;
using System.Collections.Generic;
using Events;
using Puzzle;
using UnityEngine;

namespace Puzzle
{
    public class ProgressionTracker : MonoBehaviour
    {
        [SerializeField] private DefaultEvent gameStartEvent;

        private void Start()
        {
            gameStartEvent.RaiseEvent();
        }
    }
}
