﻿using System;
using System.Collections.Generic;

public class Pool<T>
{

    
    private List<T> _uninstantiated = new List<T>();

    
    private Func<T> _factoryMethod;

    
    private Action<T> _turnOn;

    
    private Action<T> _turnOff;


    
    public Pool(Func<T> factoryMethod, Action<T> turnOn, Action<T> turnOff, int initialAmount)
    {
        _factoryMethod = factoryMethod;
        _turnOn = turnOn;
        _turnOff = turnOff;

        
        for (var i = 0; i < initialAmount; i++)
        {
            var obj = factoryMethod();
            _turnOff(obj);

            _uninstantiated.Add(obj);
        }
    }

    
    public T Get()
    {
        T obj;

        if (_uninstantiated.Count > 0)
        {
            
            obj = _uninstantiated[0];
            _uninstantiated.Remove(obj);
        }
        else
        {
            
            obj = _factoryMethod();
        }

        
        _turnOn(obj);

        return obj;
    }

   
    public void ReturnToPool(T obj)
    {
        
        _uninstantiated.Add(obj);
        
        _turnOff(obj);
    }

}