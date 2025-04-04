using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class Charge_AT : ActionTask {

		public GameObject aimPivot;
		public BBParameter<float> chargeDelay;
		public BBParameter<float> chargeDistance;
		public BBParameter<float> chargeSpeed;
        public BBParameter<NavMeshAgent> navmesh;
		public BBParameter<GameObject> target;
		public BBParameter<float> chargeCooldown;
		public BBParameter<float> chargeCooldownTimer;
        private NavMeshAgent nma;
        private float cDelay;
		private float cDistance;
		private float cSpeed;
		private float chargeDelayTimer;
		private float chargeTime; //how long to charge for.
		private VariableContainer vc;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			cDelay = chargeDelay.value;
			cDistance = chargeDistance.value;
			cSpeed = chargeSpeed.value;
            nma = navmesh.value;
			vc = agent.GetComponent<VariableContainer>();
            aimPivot.SetActive(false);
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			chargeDelayTimer = 0;
			chargeTime = cDistance / cSpeed;
			nma.SetDestination(agent.transform.position);
			agent.transform.LookAt(target.value.transform.position);
			aimPivot.SetActive(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			if (chargeDelayTimer >= cDelay )
			{
                aimPivot.SetActive(false);

                vc.velocity = agent.transform.forward * cSpeed;
				chargeTime -= Time.deltaTime;
                nma.Move(vc.velocity * Time.deltaTime);
                nma.SetDestination(agent.transform.position);
                if (chargeTime <= 0 )
				{
                    vc.velocity = new Vector3();
					chargeCooldownTimer.SetValue(chargeCooldown.value);
                    EndAction(true);
				}
			} else
			{
                chargeDelayTimer += Time.deltaTime;
                aimPivot.transform.localScale = new Vector3(1, 1, (chargeDelayTimer / cDelay) * cDistance * 0.33f);
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