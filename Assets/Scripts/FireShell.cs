using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : MonoBehaviour
{
    [field: SerializeField] public GameObject Shell
    {  get; protected set; }
    [field: SerializeField] public GameObject FirePosition
    { get; protected set; }
    [field: SerializeField] public GameObject Enemy
    { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindWithTag("tank");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 aimAt = CalculateTrajectory();

            if (aimAt != Vector3.zero)
            {
                transform.forward = aimAt;
                CreateBullet();
            }
        }
    }

    Vector3 CalculateTrajectory()
    {
        Vector3 enemyRelativePosition = Enemy.transform.position - transform.position;
        Vector3 enemyVelocity = Enemy.transform.forward * Enemy.GetComponent<Drive>().speed;
        float shellSpeed = Shell.GetComponent<MoveShell>().Speed;

        float a = Vector3.Dot(enemyVelocity, enemyVelocity) - shellSpeed * shellSpeed;
        float b = Vector3.Dot(enemyRelativePosition, enemyVelocity);
        float c = Vector3.Dot(enemyRelativePosition, enemyRelativePosition);
        float d = b * b - a * c;

        if (d < 0.1f)
        {
            return Vector3.zero;
        }

        float squareRoot = Mathf.Sqrt(d);
        float timeOne = (-b - squareRoot) / c;
        float timeTwo = (-b + squareRoot) / c;

        float time = 0;
        if (timeOne < 0 && timeTwo < 0)
        {
            return Vector3.zero;
        }
        else if (timeOne < 0)
        {
            time = timeTwo;
        }
        else if (timeTwo < 0)
        {
            time = timeOne;
        }
        else
        {
            time = Mathf.Max(new float[] { timeOne, timeTwo});
        }

        return time * enemyRelativePosition + enemyVelocity;
    }

    void CreateBullet()
    {
        Instantiate(Shell, FirePosition.transform.position, FirePosition.transform.rotation);
    }
}
