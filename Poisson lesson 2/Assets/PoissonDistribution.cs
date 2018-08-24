using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoissonDistribution : MonoBehaviour {

    public GameObject circlePrefab;
    public Vector2 bottomLeft, topRight;
    public float minimumDistance;
    public int maximumNumberOfTries;
    List<GameObject> circleList;
    public int numCirclesToSpawn;

	// Use this for initialization
	void Start ()
    {
        circleList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject circle in circleList)
            {
                Destroy(circle);
            }

            circleList.Clear();

            for (int i = 0; i < numCirclesToSpawn; i++)
            {
                SpawnCircle();
            }
        }
	}

    // Step One: Generate a random location
    Vector2 RandomLocation()
    {
        Vector2 location;
        float x = Random.Range(bottomLeft.x, topRight.x);
        float y = Random.Range(bottomLeft.y, topRight.y);

        location = new Vector2(x, y);

        return location;
    }

    // Step Two: Validate the random location
    // (To validate, other generated objects must not be near its vicinity)

    bool validateLocation(Vector2 location)
    {
        foreach (GameObject circle in circleList)
        {
            float distance = Vector2.Distance(circle.transform.position, location);

            if (distance < minimumDistance)
            {
                return false;
            }
        }

        return true;
    }

    // Step Three: Create the object at the specified location
    void SpawnCircle()
    {
        Vector2 circleLocation = GenerateValidLocation();

        if (circleLocation != Vector2.zero)
        {
            GameObject circle = Instantiate(circlePrefab);
            circle.transform.position = circleLocation;

            // Step Four: Keep track of the object generated to check against it
            // in our validation step
            circleList.Add(circle);
        }
    }

    Vector2 GenerateValidLocation()
    {
        for (int i = 0; i < maximumNumberOfTries; i++)
        {
           Vector2 randomLocation = RandomLocation();

            if (validateLocation(randomLocation))
            {
                return randomLocation;
            }
        }

        return Vector2.zero;
    }
}
