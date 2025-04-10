using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Flee_AT : ActionTask {
        
        public BBParameter<NavMeshAgent> navmesh;
        private NavMeshAgent nma;
        public BBParameter<GameObject> target;
        private GameObject t;
        public float fleeDist; //how much to offset the flee position by

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            nma = navmesh.value;
            t = target.value;
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            float aim = Mathf.Atan2(t.transform.position.x - agent.transform.position.x, t.transform.position.z - agent.transform.position.z); //get angle to target
            //agent.transform.eulerAngles = new Vector3(0, aim * Mathf.Rad2Deg, 0); //debug
            
            Vector3 fleePos =  new Vector3(Mathf.Cos(aim + 90 * Mathf.Deg2Rad), 0, Mathf.Sin(aim - 90 * Mathf.Deg2Rad)); //get aim pos as a unit vector to be added on to agent transform for flee
            nma.SetDestination((agent.transform.position + fleePos * fleeDist));
            EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}