using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;
        private Transform target;

        private void Update()
        {
            if (target != null && !IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            print("Take that you short, squat peasant!");
        }

        public bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }
    }
}