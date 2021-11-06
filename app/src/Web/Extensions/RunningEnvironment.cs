﻿using System.Collections.Generic;

namespace MyWebAPITemplate.Source.Web.Extensions
{
    public sealed class RunningEnvironment
    {
        // this must appear before other static instance types.
        public static List<RunningEnvironment> AllEnvironments { get; } = new List<RunningEnvironment>();

        public static RunningEnvironment Development { get; } = new RunningEnvironment(0, nameof(Development));
        public static RunningEnvironment LocalDocker { get; } = new RunningEnvironment(1, nameof(LocalDocker));
        public static RunningEnvironment QA { get; } = new RunningEnvironment(2, nameof(QA));
        public static RunningEnvironment Production { get; } = new RunningEnvironment(3, nameof(Production));

        public int Order { get; }
        public string Name { get; }

        private RunningEnvironment(int order, string name)
        {
            Order = order;
            Name = name;
            AllEnvironments.Add(this);
        }
        // other methods

        public static bool Exists(string name) => AllEnvironments.Exists(c => c.Name.Equals(name));
    }
}
