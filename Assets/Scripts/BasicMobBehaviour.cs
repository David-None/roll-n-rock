using UnityEngine;

public class BasicMobBehaviour : MonoBehaviour
{
    public float mobMaxHealth;    
    public float mobSpeed;
    public float mobAttack;
    public float mobFireRate;
    public float sightRange;

    protected float mobHealth;
    protected float mobHealthSegment;
    protected int mobHealthIndex;

    protected float fireTimer;

    protected Vector3 toPlayer;
    protected float distToPlayer;

    protected bool onSight;

    public GameObject player;
    public GameObject quaver;
    public SpriteRenderer mobHealthbar;    
    public Score score; 
    public Sprite[] mobHealthStates;    

    protected Animator mobAnimator;
    protected AudioSource mobAudio;

    private void Start()
    {
        mobHealth = mobMaxHealth;
        mobHealthSegment = mobMaxHealth / mobHealthStates.Length;
        mobHealthIndex = 3;

        mobAnimator = GetComponentInChildren<Animator>();
        mobAudio = GetComponent<AudioSource>();

        mobHealthbar.sprite = mobHealthStates[mobHealthIndex];
        mobHealthbar.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("playerQuaver"))
        {
            //Receive damage and set score when mob is hit by the player
            QuaverBehaviour attackingQuaver;  
            attackingQuaver=collision.gameObject.GetComponent<QuaverBehaviour>();
            //Add score
            score.RewriteScore(((int)attackingQuaver.quaverDamage));

            //Take damage
            mobHealth -= attackingQuaver.quaverDamage;
            mobHealthSegment -= attackingQuaver.quaverDamage;

            CheckHealth();  //Changes healthbar appearance and destroys the mob if health is 0

            //Destroy(attackingQuaver.gameObject);
            attackingQuaver.gameObject.SetActive(false);

            //mobAudio.Play();
        }
    }

    protected void CheckSight()
    {
        if (distToPlayer < sightRange)
        {
            onSight = true;
        }
        else
        {
            onSight = false;
        }
    }

    protected void CheckHealth()
    {
        //Changes healthbar appearance and destroys the mob if health is 0
        if (mobHealth <= 0)
        {
            //Destroy mob when health reaches 0
            Destroy(this.gameObject);
        }
        else
        {
            if (mobHealth < mobMaxHealth)
            {
                //Healthbar is only shower after the mod takes first damage
                mobHealthbar.enabled = true;
            }
            if (mobHealthSegment <= 0)
            {
                //Change appearance of the healthbar to reflect inflicted damage
                float overDamage;
                overDamage = mobHealthSegment;
                mobHealthIndex--;
                mobHealthbar.sprite = mobHealthStates[mobHealthIndex];
                mobHealthSegment = (mobMaxHealth / mobHealthStates.Length) - overDamage;
            }
        }
    }
}
