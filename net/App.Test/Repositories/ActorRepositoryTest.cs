using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using Xunit;

namespace App.Test.Repositories;

public class ActorRepositoryTest
{
	#region GetSingle

	[Fact]
	public void GetSingle_ValidId_RecordWithGivenIdReturned()
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{
			new Actor() { Id = 1, FirstName = "Jeden" },
			new Actor() { Id = 2, FirstName = "Dwa" },
			new Actor() { Id = 3, FirstName = "Trzy" },
		};
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();
		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		Actor added = new()
		{
			FirstName = "Added",
			Id = 2,
		};

		ActorRepository repo = new(dbContext.Object);

		Actor? result;

		// Act
		result = repo.GetSingle(1, CancellationToken.None).Result;

		// Assert
		Assert.Equal(contents[0], result);
	}

	[Theory]
	[InlineData(4)]
	[InlineData(-1)]
	public void GetSingle_InvalidId_NullReturned(int checkedId)
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{
			new Actor() { Id = 1, FirstName = "Jeden" },
			new Actor() { Id = 2, FirstName = "Dwa" },
			new Actor() { Id = 3, FirstName = "Trzy" },
		};
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();
		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		Actor? result;

		// Act
		result = repo.GetSingle(checkedId, CancellationToken.None).Result;

		// Assert
		Assert.Null(result);
	}

	#endregion GetSingle

	#region GetAll

	[Fact]
	public void GetAllAsync_PopulatedTable_PopulatedListReturned()
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{
			new Actor() { Id = 1, FirstName = "Jeden" },
			new Actor() { Id = 2, FirstName = "Dwa" },
			new Actor() { Id = 3, FirstName = "Trzy" },
		};
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);
		List<Actor>? result;

		// Act
		result = (List<Actor>)repo.GetAllAsync().Result;

		// Assert
		Assert.Equal(contents, result);
	}

	[Fact]
	public void GetAllAsync_EmptyTable_EmptyListReturned()
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{ };
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);
		List<Actor>? result;

		// Act
		result = (List<Actor>)repo.GetAllAsync().Result;

		// Assert
		Assert.Equal(contents, result);
	}

	#endregion GetAll

	#region Add

	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	public void Add_ValidRecord_DbSetAddCalledOnce(int recordsToAdd)
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{ };
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();

		var added = new Actor() { Id = 0 };

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		// Arange
		for (int i = 0; i < recordsToAdd; i++)
		{
			repo.Add(added);
		}

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Add(added), Times.Exactly(recordsToAdd));
	}

	#endregion Add

	
}