using System.Collections;
using System.Collections.Generic;
using MazeMyTD;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ConstructionTest
    {
        private PlayerData CreatePlayerData()
        {
            PlayerData playerData = ScriptableObject.CreateInstance("PlayerData") as PlayerData;
            playerData.availableBuildings = new ABuilding[1];
            playerData.availableBuildings[0] = null;
            return playerData;
        }

        private PlayerData CreatePlayerData(int health, int ressources)
        {
            PlayerData playerData = ScriptableObject.CreateInstance("PlayerData") as PlayerData;
            playerData.health = health;
            playerData.ressources = ressources;
            playerData.availableBuildings = new ABuilding[2];
            playerData.availableBuildings[0] = MonoBehaviour.Instantiate(Resources.Load<ABuilding>("Prefabs/Buildings/DefaultTower"));
            playerData.availableBuildings[1] = MonoBehaviour.Instantiate(Resources.Load<ABuilding>("Prefabs/Buildings/Wall"));
            return playerData;
        }

        private ConstructionController CreateConstructionController(PlayerData playerData)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<ConstructionController>();
            gameObject.GetComponent<ConstructionController>().SetPlayerData(playerData);
            return gameObject.GetComponent<ConstructionController>();
        }

        [Test]
        public void PlayerDataIsSuccesfullyCreated()
        {
            int health = Random.Range(1, 500);
            int ressources = Random.Range(0, 5000);
            PlayerData playerData = CreatePlayerData(health, ressources);

            Assert.AreEqual(health, playerData.health);
            Assert.AreEqual(ressources, playerData.ressources);
        }

        [Test]
        public void ConstructionControllerIsSuccesfullyCreated()
        {
            ConstructionController constructionController = CreateConstructionController(CreatePlayerData());
            Assert.AreNotEqual(constructionController, null);
            GameObject.DestroyImmediate(constructionController);

            constructionController = CreateConstructionController(CreatePlayerData(1, 1));
            Assert.AreNotEqual(constructionController, null);
            GameObject.DestroyImmediate(constructionController);
        }


        [Test]
        public void InvalidBuildingConstruction()
        {
            ConstructionController constructionController = CreateConstructionController(CreatePlayerData());

            Assert.AreEqual(constructionController.CheckBuildingValidity((int)Random.Range(1, 10)), false);
            GameObject.DestroyImmediate(constructionController);
        }

        [Test]
        public void NotEnoughRessourceToBuild()
        {
            int ressources = Random.Range(0, 4);
            ConstructionController constructionController = CreateConstructionController(CreatePlayerData(0, ressources));

            Assert.AreEqual(constructionController.CheckRessourcesAvailability(Random.Range(0, 1)), false);
            GameObject.DestroyImmediate(constructionController);
        }
    }
}
