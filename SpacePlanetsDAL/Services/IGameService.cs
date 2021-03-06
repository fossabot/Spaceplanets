﻿using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Persist a galaxy to the database.
        /// </summary>
        /// <param name="galaxyName">The name of the galaxy to save.</param>
        void GenerateGalaxy(string galaxyName);

        /// <summary>
        /// Retrieve a galaxy based on its name.
        /// </summary>
        /// <param name="galaxyName">The name of the galaxy.</param>
        /// <returns>A galaxy</returns>
        Galaxy RetrieveGalaxyByName(string galaxyName);
    }
}
