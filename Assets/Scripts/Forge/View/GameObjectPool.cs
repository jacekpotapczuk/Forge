﻿using UnityEngine;

namespace Forge.View
{
    /// <summary>
    /// *Should* Provides pooling functionality
    /// todo: proper pooling, disable game objects only
    /// </summary>
    public static class GameObjectPool
    {
        public static T Get<T>(T gameObject) where T : Object
        {
            return Object.Instantiate(gameObject);
        }
        
        public static T Get<T>(T gameObject, Transform parent) where T : Object
        {
            return Object.Instantiate(gameObject, parent);
        }
        
        public static T Get<T>(T gameObject, Transform parent, bool worldPositionStays) where T : Object
        {
            return Object.Instantiate(gameObject, parent, worldPositionStays);
        }

        public static void Destroy(GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }
    }
}