using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{

    [SerializeField] public int TotalHit;
    public int HitCount;
    public float ImpulseSpeed;
    public GameObject Tree;
    public Transform TreePosition;
    public Rigidbody Player;
    public bool IsTreeDestroyed;


    // Start is called before the first frame update
    void Start()
    {
        Tree = FindObjectOfType<GameObject>();
        TreePosition.position = transform.position;
        TreePosition.rotation = transform.rotation;
        HitCount = 0;
        IsTreeDestroyed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HitCount++;
            Debug.Log("Hit" + HitCount);
            Player.AddForce(Vector3.back * ImpulseSpeed, ForceMode.Impulse);
            if (HitCount == TotalHit)
            {
                
                Destroy(this.gameObject);
                HitCount = 0;
                IsTreeDestroyed = true;
                //StartCoroutine(TreeSpawn());
            }

        }
    }
    /*IEnumerator TreeSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(Tree, TreePosition.transform.position, TreePosition.transform.rotation);
        IsTreeDestroyed = false;
    }*/
}
