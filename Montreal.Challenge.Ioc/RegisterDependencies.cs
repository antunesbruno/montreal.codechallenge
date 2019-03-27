﻿using System;

namespace Montreal.Challenge.Ioc
{
    public static class RegisterDependencies
    {
        public static void BuildDependencies(Action InjectDependencies = null)
        {
            //create container
            Injector.CreateContainer();

            //inject external dependencies
            InjectDependencies.Invoke();

            //build container
            Injector.BuildContainer();
        }
    }
}
