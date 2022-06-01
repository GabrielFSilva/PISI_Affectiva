using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    PlayerEmotions playerEmotions;
    bool isAttacking;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        playerEmotions = player.GetComponent<PlayerEmotions> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        if (enemyHealth.currentHealth <= 0)
            nav.enabled = false;
        else if (playerHealth.currentHealth <= 0)
            nav.enabled = false;
        else if (enemyHealth.currentHealth != enemyHealth.startingHealth)
        {
            nav.SetDestination(player.position);
            isAttacking = true;
        }
        else if (zenState(playerEmotions))
            nav.SetDestination(new Vector3(-10f, 0f, 21f));
        else if (isAttacking)
            nav.SetDestination(player.position);
        else if (playerEmotions != null && playerEmotions.currentContempt > 20)
        {
            nav.SetDestination(player.position);
            isAttacking = true;
        }
        else if (playerEmotions != null && playerEmotions.currentAnger > 20)
        {
            nav.SetDestination(player.position);
            isAttacking = true;
        }
        else if (playerEmotions != null && playerEmotions.currentDisgust > 20)
        {
            nav.SetDestination(player.position);
            isAttacking = true;
        }
        else
        {
            float x = Random.Range(-34, 34);
            float z = Random.Range(-34, 34);
            nav.SetDestination(new Vector3(x, 0f, z));
        }
    }

    private bool zenState(PlayerEmotions playerEmotions)
    {
        if (playerEmotions == null)
            return false;
        else if (playerEmotions.currentValence > 80)
            return true;
        // Make it easy on QA to enter zen state!
        else if (Debug.isDebugBuild && playerEmotions.currentValence > 40)
            return true;
        else return false;
    }
}
