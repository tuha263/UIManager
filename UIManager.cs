using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.UIManager
{
    public abstract class UIManager<I> where I : IOpenCloseAble
    {
        private readonly Dictionary<Type, I> _uiMap = new Dictionary<Type, I>();
        private readonly Stack<I> _uIStack = new Stack<I>();

        public void Open<T>(Transform parent) where T : Object, I
        {
            _uiMap.TryGetValue(typeof(T), out I uiScreen);
            if (uiScreen == null)
            {
                uiScreen = CreateScreen<T>(parent);
                _uiMap.Add(typeof(T), uiScreen);
            }
            
            if (_uIStack.Count > 0)
            {
                _uIStack.Peek().close(() => uiScreen.open());
            }
            else
            {
                uiScreen.open();
            }
            
            _uIStack.Push(uiScreen);
        }

        public void Back()
        {
            if (_uIStack.Count == 0)
            {
                return;
            }
            _uIStack.Pop().close(() => _uIStack.Peek()?.open());
        }

        private static I CreateScreen<T>(Transform parent) where T : Object, I
        {
            ResourcePrefab attribute = typeof(T).GetCustomAttribute<ResourcePrefab>();
            T prefab = Resources.Load<T>(attribute.path);
            T uiScreen = Object.Instantiate(prefab, parent);
            return uiScreen;
        }
    }
}