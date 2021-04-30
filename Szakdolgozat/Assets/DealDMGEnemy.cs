using UnityEngine;

public class DealDMGEnemy : MonoBehaviour
{

    public EnemyClass enemy;
    private float dmg;
    void Start()
    {
        dmg = enemy.Dmg;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float hp = other.gameObject.GetComponent<PlayerClass>().Hp;
            float newHp = hp - dmg;
           
            if (newHp <= 0f)
            {                
                other.gameObject.GetComponent<PlayerClass>().Die();
            }
            else
                other.gameObject.GetComponent<PlayerClass>().TakeDmg(newHp);

        }else if (other.CompareTag("Nexus"))
        {
            other.gameObject.transform.parent.gameObject.GetComponent<Nexus>().TakeDmg(dmg);
        }

    }

}
