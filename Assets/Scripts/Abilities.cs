using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Abilities : MonoBehaviour
{
    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown = false;
    public KeyCode ability1;
    public float shootSpeed = 10f;
    public GameObject projectile;
    public Transform shootpoint;
    private float distance;
    public float timePassed = 0f;
    [SerializeField] AudioSource feedBack;

    // Ability 1 inputs
    Vector3 position;
    public Canvas ability1Canvas;
    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Transform player;
    public float maxAbility1Distance;
    
    private Vector3 posUP;
    private Vector3 bomb;
    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;

        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability Inputs
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject != this.gameObject)
            {
                posUP = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
                if (Input.GetMouseButtonUp(0))
                {
                    bomb = hit.point;
                    Debug.Log(bomb);
                }
            }
        }

        //Ability Canvas input
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbility1Distance);

        var newHitPos = transform.position + hitPosDir * distance;
        ability1Canvas.transform.position = (newHitPos);
    }
    void Ability1()
    {
        if(Input.GetKey(ability1) && isCooldown == false)
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;

        }
        if(targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCooldown = true;
            abilityImage1.fillAmount = 1;
            Shoot();
            feedBack.Play();
             
        }

        if (isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
    void Shoot()
    {
        
        GameObject currentBullet = Instantiate(projectile, shootpoint.position, transform.rotation);
        Rigidbody rig = currentBullet.GetComponent<Rigidbody>();
        timePassed += Time.deltaTime;
        if (timePassed > 5f)
        {
            //do something
            rig.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);
        }
        rig.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);
    }
}
