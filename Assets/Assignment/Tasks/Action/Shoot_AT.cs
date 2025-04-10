using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Shoot_AT : ActionTask {

        public GameObject aim;
        public BBParameter<float> shootDelay;
        public BBParameter<GameObject> bullet;
        public BBParameter<NavMeshAgent> navmesh;
        public BBParameter<GameObject> target;
        public BBParameter<float> shootReload;
        public BBParameter<float> reloadTimer;
        public BBParameter<float> fleeRange;
        private NavMeshAgent nma;
        private float sDelay;
        private float shootDelayTimer;
        private GameObject b;
        private InstantiateBullet ib;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            sDelay = shootDelay.value;
            nma = navmesh.value;
            b = bullet.value;
            ib = agent.GetComponent<InstantiateBullet>(); //get the script to instantiate because NodeCanvas is super weird about instantiation
            aim.SetActive(false);
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            shootDelayTimer = 0;
            nma.SetDestination(agent.transform.position);
            agent.transform.LookAt(target.value.transform.position);
            aim.SetActive(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

            if (shootDelayTimer >= sDelay)
            {
                aim.SetActive(false);
                ib.spawn(b); //spawn bullet, returns the object in case I need it
                reloadTimer.SetValue(shootReload.value); //set reload to max for shooting
                EndAction(true);
            }
            else
            {
                shootDelayTimer += Time.deltaTime;
                agent.transform.LookAt(target.value.transform.position);

                if (Vector3.Distance(target.value.transform.position, agent.transform.position) <= fleeRange.value)
                {
                    aim.SetActive(false);
                    EndAction(true); //end action if player gets within flee range - aka, interrupt
                }

            }

        }

        //Called when the task is disabled.
        protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}