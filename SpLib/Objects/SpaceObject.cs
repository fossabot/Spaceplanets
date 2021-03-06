﻿using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class SpaceObject : Document
    {
        /// <summary>
        /// Should be one of: Star, Planet, Moon, Asteroid, Comet
        /// </summary>
        public string ObjectType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set;  }
        public string Name { get; set; }
        public Guid StarSystemId { get; set; }
        public SpaceObject(Guid starSystemId, string objectType, string name = "Unnamed")
        {
            this.StarSystemId = starSystemId;
            ObjectType = objectType;
            Version = 1;
            Name = name;
        }
    }
}
