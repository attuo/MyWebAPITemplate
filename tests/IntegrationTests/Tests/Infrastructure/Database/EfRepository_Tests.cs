using System;
using System.Threading.Tasks;
using FluentAssertions;
using MyWebAPITemplate.Tests.IntegrationTests.Utils;
using MyWebAPITemplate.Tests.Shared.Builders.Entities;
using MyWebAPITemplate.Tests.UnitTests.Shared.Ids;
using Xunit;

namespace MyWebAPITemplate.Tests.IntegrationTests.Tests.Infrastructure.Database
{
    public class EfRepository_Tests : TestFixture
    {
        [Fact]
        public async Task ListAllAsync_Ok()
        {
            var repository = GetTodoRepository();
            var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
            await repository.AddAsync(todo);

            var items = await repository.ListAllAsync();

            items.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdAsync_Ok()
        {
            var repository = GetTodoRepository();
            var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
            await repository.AddAsync(todo);

            var item = await repository.GetByIdAsync(todo.Id);

            item.Should().NotBeNull();
            item.Id.Should().Be(todo.Id);
        }

        [Fact]
        public async Task AddAsync_Ok()
        {
            var repository = GetTodoRepository();
            var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
            await repository.AddAsync(todo);

            var item = await repository.GetByIdAsync(todo.Id);

            item.Should().NotBeNull();
            item.Id.Should().Be(todo.Id);
        }

        [Fact]
        public async Task UpdateAsync_Ok()
        {
            var repository = GetTodoRepository();
            var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
            await repository.AddAsync(todo);

            todo.Description = "test1";
            await repository.UpdateAsync(todo);

            var result = await repository.GetByIdAsync(todo.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(todo.Id);
            result.Description.Should().Be(todo.Description);
        }

        [Fact]
        public async Task DeleteAsync_Ok()
        {
            var repository = GetTodoRepository();
            var todo = TodoEntityBuilder.CreateValid(TestIds.NormalUsageId);
            await repository.AddAsync(todo);

            var firstResult = await repository.GetByIdAsync(todo.Id);

            await repository.DeleteAsync(todo);

            var secondResult = await repository.GetByIdAsync(todo.Id);

            firstResult.Should().NotBeNull();
            secondResult.Should().BeNull();
        }
    }
}
