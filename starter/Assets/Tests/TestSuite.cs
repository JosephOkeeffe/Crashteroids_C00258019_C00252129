using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    // 1
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

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

    //1
    [Test]
    public void NewGameRestartsGame()
    {
        //2
        game.isGameOver = true;
        game.NewGame();
        //3
        Assert.False(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator StartingANewGameSetsTheScoreToZero()
    {
        // 1
        game.NewGame();
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 0);
    }

    [UnityTest]
    public IEnumerator CheckIfAsteroidGetsDestroyed()
    {
        
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        bool result = false;
        yield return new WaitForSeconds(5f);

        if(asteroid == null) 
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

    [UnityTest]
    public IEnumerator ShipMovesUp()
    {
        float initZPos = game.GetShip().transform.position.z;

        game.GetShip().MoveUp();

       
        yield return new WaitForSeconds(1f);
        Assert.Less(game.GetShip().transform.localPosition.z, initZPos);
    }

    [UnityTest]
    public IEnumerator ShipMovesDown()
    {
        float initZPos = game.GetShip().transform.position.z;

        game.GetShip().MoveDown();


        yield return new WaitForSeconds(1f);
        Assert.Greater(game.GetShip().transform.localPosition.z, initZPos);
    }

    [UnityTest]

    public IEnumerator WrapAroundTest()
    {

        Vector3 currentPosition = game.GetShip().transform.position;

        bool result = false;

        game.GetShip().MoveLeft();
        
        if(currentPosition.x < 8f)
        {
            result = true;
        }

        yield return new WaitForSeconds(5.0f);

        Assert.IsTrue(result);
    }

}