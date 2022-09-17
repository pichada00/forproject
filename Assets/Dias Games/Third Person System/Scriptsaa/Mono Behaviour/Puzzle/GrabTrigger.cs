using UnityEngine;

namespace DiasGames.Puzzle
{
    public class GrabTrigger : MonoBehaviour, IDraggable
    {
        [SerializeField] private Transform targetCharacterTransform = null;
        [SerializeField] private Transform rightIK = null;
        [SerializeField] private Transform leftIK = null;
        [Space]
        [SerializeField] private Collider characterRefCollider;
        [SerializeField] private Interactor interactor;

        public Transform Target { get { return targetCharacterTransform; } }

        private DraggableObject _block = null;

        private void Awake()
        {
            _block = GetComponentInParent<DraggableObject>();
            characterRefCollider.enabled = false;
            interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
        }

        public void StartDrag()
        {
            _block.EnablePhysics();
            //Interactor.Instance.handLeft = true;
            //Interactor.Instance.handRight = true;
            //characterRefCollider.enabled = true;
        }

        public void StopDrag()
        {
            _block.DisablePhysics();
            //Interactor.Instance.handLeft = false;
            //Interactor.Instance.handRight = false;
            characterRefCollider.enabled = false;
        }

        public bool Move(Vector3 velocity)
        {
            return _block.Move(velocity);
        }

        public void SetPushState(bool pushing)
        {
            if (pushing)
                _block.EnablePhysics();
            else
                _block.DisablePhysics();
        }

        #region IHandIk & ICharacterTargetPos

        public Transform GetLeftHandTarget()
        {
            return leftIK;
        }

        public Transform GetRightHandTarget()
        {
            return rightIK;
        }

        public Transform GetTarget()
        {
            return targetCharacterTransform;
        }

        #endregion
    }
}