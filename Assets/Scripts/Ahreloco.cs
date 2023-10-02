using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
 
    private Transform enemy;
 
    private GameObject Enemy;
 
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
 
    }
 
    public void OnTriggernEnter(string enemytag, GameObject other)
    {
        if (enemytag == "Enemy" && other.tag == "Player")
        {
            other.GetComponent<PlayerMoveScript>().TakeHit();
        }
    }
 
    public void StopAttack()
    {
        animator.SetBool("Attack", false);
    }
    public void Attack()
    {
        GameObject go = Instantiate(Enemy, enemy.position, Quaternion.identity);
        Vector3 direction = new Vector3(transform.localScale.x, 0);  
 
    }
 
}