using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterController : MonoBehaviour
{
    public GameObject target;
    Animator animator;
    NavMeshAgent agent;
    public float walkingSped;
    public float runningSpeed;
    public ParticleSystem particleSystem;
   // public GameObject enemyRagDoll;
    enum STATE
    {
        IDLE, WONDER, CHASE, ATTCK, DEAD
    };
    STATE state = STATE.IDLE;
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        switch (state)
        {

            case STATE.IDLE:
                if (SeeThePlayer())
                {
                    state = STATE.CHASE;
                }
                else if (Random.Range(0,1000) < 5)
                {
                    state = STATE.WONDER;
                }
              
                   
                break;
            case STATE.WONDER:
                if(!agent.hasPath)
                {
                    float randValueX = transform.position.x + Random.Range(-5f, 5f);
                    float randValueZ = transform.position.z + Random.Range(-5f, 5f);
                    float valueY = Terrain.activeTerrain.SampleHeight(new Vector3(randValueX, 0, randValueZ));
                    Vector3 destination = new Vector3(randValueX, 0, randValueZ);
                    agent.SetDestination(destination);
                    agent.stoppingDistance = 0f;
                    agent.speed = walkingSped;
                    TurnOfAllAnim();
                    animator.SetBool("isWalking", true);
                }
                if(SeeThePlayer())
                {
                    state = STATE.CHASE;
                }
                else if(Random.Range(0,1000) < 7)
                {
                    state = STATE.IDLE;
                    TurnOfAllAnim();
                    agent.ResetPath();
                }

               
                break;
            case STATE.CHASE:
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 3f;
                TurnOfAllAnim();

                animator.SetBool("isRunning", true);
                agent.speed = runningSpeed;
                if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = STATE.ATTCK;
                }
                if(CannotSeePlayer())
                {
                    state = STATE.WONDER;
                    agent.ResetPath();
                }

                break;
            case STATE.ATTCK:
                TurnOfAllAnim();
                animator.SetBool("isAttacking", true);
                transform.LookAt(target.transform.position); //Enemies Look at Player
                if(DistanceToPlayer() > agent.stoppingDistance + 3)
                {
                    state = STATE.CHASE;
                }
                Debug.Log("This is an Attack state");
                break;
            case STATE.DEAD:
                TurnOfAllAnim();
                animator.SetBool("isDead", true);
                Debug.Log("Entered Dead State");
                
               
                this.gameObject.SetActive(false);
               // Instantiate(enemyRagDoll, transform.position, Quaternion.identity);
                
                GameObject temp = PoolScript.instance.GetObjectsFromPool("MonsterRagDoll");
                Debug.Log("RagdOll form Pool");
                GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>().Score(10);
                if (temp != null)
                {
                    
                    temp.SetActive(true);
                    Debug.Log("Making to true");
                   // StartCoroutine("DeathAfterDelay");
                    //temp.transform.position = new Vector3(UnityEngine.Random.Range(-11f, 15f), 0, UnityEngine.Random.Range(-12f, -11f));
                    temp.transform.position = this.transform.position;
                }

                particleSystem.Play();
              //  temp.SetActive(false);
               // Debug.Log("Monster Went to Pool");
                break;
            default:
                break;
        }

    }
    /*
    IEnumerator DeathAfterDelay()
    {
        yield return new WaitForSeconds(15);
    }*/
    public void TurnOfAllAnim()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", false);
    }
    public bool SeeThePlayer()
    {
        if (DistanceToPlayer() < 10f)
            return true;
        else
            return false;
    }

    private float DistanceToPlayer()
    {

        return Vector3.Distance(target.transform.position, this.transform.position);
    }
    public bool CannotSeePlayer()
    {
        if(DistanceToPlayer() > 20f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DeadEnemy()
    {
        Debug.Log("Dead State");
        state = STATE.DEAD;
        //animator.SetBool("isDead", true);

    }
    int damageAmount = 5;
    public void DamagePlayer()
    {
        if(target != null)
        {
            Debug.Log("Damage AMount method is called");

            target.GetComponent<PlayerController>().TakeHit(damageAmount);
        }
    }


}
