using UnityEngine;

namespace Game
{
    public class Player: MonoBehaviour
    {
        public Rigidbody2D rigidbody2D;
        public GameObject graphicsParent;

        public void HandleDeath()
        {
            Destroy(graphicsParent);
        }
    }
}