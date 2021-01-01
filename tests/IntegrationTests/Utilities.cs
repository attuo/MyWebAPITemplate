﻿using System.Collections.Generic;
using MyWebAPITemplate.Core.Entities;
using MyWebAPITemplate.Infrastructure.Database;

namespace MyWebAPITemplate.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Todos.AddRange(CreateTestTodos());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.Todos.RemoveRange(db.Todos);
            InitializeDbForTests(db);
        }

        public static List<TodoEntity> CreateTestTodos()
        {
            return new List<TodoEntity>()
            {
                new TodoEntity { Description = "Test Todo 1", IsDone = false },
                new TodoEntity { Description = "Test Todo 2", IsDone = false },
            };
        }
    }
}
