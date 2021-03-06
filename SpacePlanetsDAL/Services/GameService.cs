﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using SpLib.Helpers;
using SpLib.Objects;

namespace SpacePlanetsDAL.Services
{
    public class GameService : IGameService
    {
        private readonly Repositories.IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;
        private readonly Random _random;

        public GameService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new Repositories.RepositoryWrapper(_mongoClient);
            _random = new Random();
        }

        public void GenerateGalaxy(string galaxyName)
        {
            Galaxy galaxy = new Galaxy();
            galaxy.SizeX = _random.Next(50, 200);
            galaxy.SizeY = galaxy.SizeX; //_random.Next(200, 350);
            galaxy.SizeZ = _random.Next(1, 25);
            if (!string.IsNullOrEmpty(galaxyName))
            {
                galaxy.Name = galaxyName;
            }
            else
            {
                galaxy.Name = "Galaxy " + GenerationHelper.CreateRandomString(true, true, false, 5);
            }
            int volume = galaxy.SizeX * galaxy.SizeY * galaxy.SizeZ;
            int lowerBoundX = (galaxy.SizeX / 2) * -1;
            int lowerBoundY = (galaxy.SizeY / 2) * -1;
            int lowerBoundZ = (galaxy.SizeZ / 2) * -1;
            int upperBoundX = lowerBoundX * -1;
            int upperBoundY = lowerBoundY * -1;
            int upperBoundZ = lowerBoundZ * -1;
            for (int Xctr = lowerBoundX; Xctr <= upperBoundX; Xctr++)
            {
                for (int Yctr = lowerBoundY; Yctr <= upperBoundY; Yctr++)
                {
                    for (int Zctr = lowerBoundZ; Zctr <= upperBoundZ; Zctr++)
                    {
                        // make random number to decide if this is a place a star should go.
                        int starChance = _random.Next(1, 1000);
                        if (starChance <= 2)
                        {
                            // create star
                            StarSystem starSystem = new StarSystem();
                            starSystem.Name = "System " + GenerationHelper.CreateRandomString(true, false, false, 6) + " " + GenerationHelper.CreateRandomString(false, true, false, 2);
                            starSystem.CreateRandomizedSpaceObjects(_random);
                            galaxy.StarSystems.Add(starSystem);
                        }
                    }
                }
            }
            _wrapper.GalaxyRepository.AddOne<Galaxy>(galaxy);
        }

        public Galaxy RetrieveGalaxyByName(string galaxyName)
        {
            Galaxy galaxy = _wrapper.GalaxyRepository.GetOne<Galaxy>(f => f.Name == galaxyName);
            return galaxy;
        }
    }
}
