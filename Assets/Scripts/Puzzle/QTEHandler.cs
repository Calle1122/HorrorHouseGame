using QTESystem;
using UnityEngine;

namespace Puzzle
{
    public class QTEHandler : MonoBehaviour
    {
        [SerializeField] private QTE qteComponent;

        public QTE QTEComponent { get; private set; }

        private void Awake()
        {
            QTEComponent = qteComponent;
        }
    }
}