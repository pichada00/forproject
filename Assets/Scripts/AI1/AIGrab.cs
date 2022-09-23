using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DiasGames.Puzzle
{
    public class AIGrab : MonoBehaviour, IDraggable
    {
        [SerializeField] private Transform targetCharacterTransform = null;
        [SerializeField] private Transform leftIK = null;
        [SerializeField] private Transform rightIK = null;
        [SerializeField] private Collider characterRefCollider;

        public Transform Target { get { return targetCharacterTransform; } }

        private DragAI _block = null;
        public bool grabAI = false;
        private void Awake()
        {
            _block = GetComponentInParent<DragAI>();
            characterRefCollider.enabled = false;
            //interactor = GameObject.Find("CS Character Controller").GetComponent<Interactor>();
        }

        private void FixedUpdate()
        {
            if(grabAI == true)
            {

            }
        }
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

        public bool Move(Vector3 velocity)
        {
            return _block.Move(velocity);
        }

        public void StartDrag()
        {
            _block.EnablePhysicsofAI();
            grabAI = true;
            //characterRefCollider.enabled = true;
        }

        public void StopDrag()
        {
            grabAI = false;
            _block.DisablePhysicsAI();
            characterRefCollider.enabled = false;
        }

        public void SetPushState(bool pushing)
        {
            if (pushing)
                _block.EnablePhysics();
            else
                _block.DisablePhysics();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        
    }
}
