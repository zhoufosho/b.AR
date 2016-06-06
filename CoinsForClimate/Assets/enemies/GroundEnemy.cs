using UnityEngine;
using System.Collections;

public class GroundEnemy : MonoBehaviour
{
    public float delay = 4.0f;
    public GameObject groundEnemy;
    public Transform target;

    public float spawnRadius = 15f;

    private GameObject enemyInstance;

    // Use this for initialization
    void Awake()
    {
        GameFramework.OnStartTreeGrowth += Activate;
        GameFramework.OnTreeLose += Deactivate;
        GameFramework.OnTreeWin += Deactivate;
    }

    void Activate()
    {
        enabled = true;
        InvokeRepeating("Spawn", delay, delay);
    }

    void Deactivate()
    {
        CancelInvoke("Spawn");
        enabled = false;
    }

    void Spawn()
    {
        Vector3 tpos = target.position;
        enemyInstance = Instantiate(groundEnemy, new Vector3(Random.Range(tpos.x - spawnRadius, tpos.x + spawnRadius), tpos.y, Random.Range(tpos.z - spawnRadius, tpos.z + spawnRadius)), Quaternion.identity) as GameObject;
        enemyInstance.transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyInstance != null && enemyInstance.transform.GetChild(0).localScale.x < 1.0f)
        {
            enemyInstance.transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == groundEnemy.name + "(Clone)") Destroy(hit.collider.gameObject);
            }
        }
    }
}