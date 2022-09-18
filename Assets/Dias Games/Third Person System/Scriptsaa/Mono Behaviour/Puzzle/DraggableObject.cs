using UnityEngine;

namespace DiasGames.Puzzle
{
    public class DraggableObject : MonoBehaviour
    {
        private Rigidbody _rigidbody = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public virtual bool Move(Vector3 velocity)
        {
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;

            return true;
        }

        public void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = Vector3.zero;
        }
        public void EnablePhysicsofAI()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
        }

        public virtual void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
        }
        public virtual void DisablePhysicsAI()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = true;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
