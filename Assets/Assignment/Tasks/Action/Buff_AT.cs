using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;


namespace NodeCanvas.Tasks.Actions {

	public class Buff_AT : ActionTask {

        public GameObject buffSig;
        public BBParameter<float> buffDelay;
        public BBParameter<float> buffRadius;
        public BBParameter<NavMeshAgent> navmesh;
        public BBParameter<GameObject> ally;
        public BBParameter<GameObject> target;
        public BBParameter<float> buffReload;
        public BBParameter<float> reloadTimer;
        public BBParameter<float> fleeRange;
        private NavMeshAgent nma;
        private float bDelay;
        private float bRadius;
        private float buffDelayTimer;
        private GameObject a;
        private GameObject t;
        private PsychicExtraFunctionality pef;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            bDelay = buffDelay.value;
            bRadius = buffRadius.value;
            nma = navmesh.value;
            a = ally.value;
            t = target.value;
            buffSig.SetActive(false);
            pef = agent.GetComponent<PsychicExtraFunctionality>();
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            buffDelayTimer = 0;
            nma.SetDestination(agent.transform.position);
            //agent.transform.LookAt(t.transform.position);
            buffSig.SetActive(true);
            pef.buffing = true;
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

            if (buffDelayTimer >= bDelay)
            {
                buffSig.SetActive(false);
                pef.buffing = false;
                reloadTimer.SetValue(buffReload.value); //set reload to max for buffing

                if (Vector3.Distance(a.transform.position, agent.transform.position) <= bRadius) //only buff if within range
                {
                    a.GetComponent<ChargerBuffHandler>().buff();
                }
                
                EndAction(true);
            }
            else
            {
                buffDelayTimer += Time.deltaTime;
                float size = (buffDelayTimer / bDelay) * bRadius;
                buffSig.transform.localScale = new Vector3(size, 0.1f, size);
                //agent.transform.LookAt(t.transform.position);

                if (Vector3.Distance(t.transform.position, agent.transform.position) <= fleeRange.value)
                {
                    buffSig.SetActive(false);
                    pef.buffing = false;
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