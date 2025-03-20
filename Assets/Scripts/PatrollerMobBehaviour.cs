using UnityEngine;

public class PatrollerMobBehaviour : BasicMobBehaviour
{
    public float nextPointDist;

    private Vector3 toPatrol;
    private float distToPatrol;
    private int currentPointIndex;

    public GameObject[] patrolPoints;

    void Start()
    {
        mobHealth = mobMaxHealth;
        mobHealthSegment = mobMaxHealth / mobHealthStates.Length;
        mobHealthIndex = 3;

        fireTimer = mobFireRate;

        currentPointIndex = 0;  

        mobAnimator = GetComponentInChildren<Animator>();
        mobAudio = GetComponent<AudioSource>();

        mobHealthbar.sprite = mobHealthStates[mobHealthIndex];
        mobHealthbar.enabled = false;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime; // firing cooldown

        //Get the vector from mob to player and its distance
        toPlayer = player.transform.position - this.transform.position;
        distToPlayer = toPlayer.magnitude;

        //Check if player is on sight or not
        CheckSight();

        if (!onSight)
        {
            //When the player is not on sight radius, patrol between the set points

            //Set Animator's transitions
            mobAnimator.SetBool("IsMoving", true);
            mobAnimator.SetBool("IsAttacking", false);

            //Trace the vector from mob to patrol point and get its magnitude
            toPatrol = (patrolPoints[currentPointIndex].transform.position - this.transform.position);
            distToPatrol = toPatrol.magnitude;

            //Move the mob toward the patrol point
            this.transform.Translate(toPatrol.normalized * mobSpeed * Time.deltaTime);

            if (distToPatrol < nextPointDist)
            {
                //Change to next patrol point when near enough
                currentPointIndex++;
                if (currentPointIndex > patrolPoints.Length - 1)
                {
                    currentPointIndex = 0;
                }
            }
        }
        else
        {
            if (fireTimer < 0)
            {
                //When the player is on sight and cooldown has passed, fire a Quaver
                GameObject newQuaver;
                QuaverBehaviour newQuaverBehaviour;

                //Set Animator's transitions
                mobAnimator.SetBool("IsMoving", false);
                mobAnimator.SetBool("IsAttacking", true);

                //Instantiate new Quaver aimed at player
                newQuaver = Instantiate(quaver, this.transform.position, Quaternion.identity);
                newQuaverBehaviour = newQuaver.GetComponent<QuaverBehaviour>();
                newQuaverBehaviour.direction = toPlayer;
                newQuaverBehaviour.quaverDamage = mobAttack;
                fireTimer = mobFireRate;
            }
        }
    }
}
