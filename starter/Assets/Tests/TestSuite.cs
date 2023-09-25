using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    // 1
    private Game game;

    // 2
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        // 3
        GameObject gameGameObject =
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
        // 4
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // 5
        float initialYPos = asteroid.transform.position.y;
        // 6
        yield return new WaitForSeconds(0.1f);
        // 7
        Assert.Less(asteroid.transform.position.y, initialYPos);
        // 8
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject gameGameObject =
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.isGameOver);

        Object.Destroy(game.gameObject);
    }
}