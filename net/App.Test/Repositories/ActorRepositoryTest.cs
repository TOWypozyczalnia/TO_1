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
	public void Add_NValidRecords_DbSetAddCalledNTimes(int recordsToAdd)
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{ };
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		// Arange
		for (int i = 0; i < recordsToAdd; i++)
		{
			repo.Add(new Actor { Id = i });
		}

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Add(It.IsAny<Actor>()), Times.Exactly(recordsToAdd));
	}

	#endregion Add

	#region Remove

	[Theory]
	[InlineData(1)]
	[InlineData(3)]
	public void Remove_NValidRecords_DbSetRemoveCalledNTimes(int recordsToRemove)
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

		var added = new Actor() { Id = 0 };

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		// Arange
		for (int i = 0; i < recordsToRemove; i++)
		{
			repo.Remove(contents[0]);
		}

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Remove(It.IsAny<Actor>()), Times.Exactly(recordsToRemove));
	}

	[Fact]
	public void Remove_InvalidRecord_DbSetRemoveCalledOnce()
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

		var added = new Actor() { Id = 0 };

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		// Arange
		repo.Remove(new Actor { Id = 4 });

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Remove(It.IsAny<Actor>()), Times.Once);
	}

	[Fact]
	public void Remove_FromEmptyTable_DbSetRemoveCalledOnce()
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{ };
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		repo.Remove(new Actor() { Id = 1 });

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Remove(It.IsAny<Actor>()), Times.Once);
	}

	#endregion Remove
	#region Update

	[Theory]
	[InlineData(1)]
	[InlineData(3)]
	public void Update_NValidRecords_DbSetUpdateCalledNTimes(int recordsToRemove)
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

		var added = new Actor() { Id = 0 };

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		// Arange
		for (int i = 0; i < recordsToRemove; i++)
		{
			repo.Update(contents[0]);
		}

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Update(It.IsAny<Actor>()), Times.Exactly(recordsToRemove));
	}

	[Fact]
	public void Update_InvalidRecord_DbSetUpdateCalledOnce()
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

		var added = new Actor() { Id = 0 };

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		// Arange
		repo.Update(new Actor { Id = 4 });

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Update(It.IsAny<Actor>()), Times.Once);
	}

	[Fact]
	public void Update_FromEmptyTable_DbSetUpdateCalledOnce()
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{ };
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		repo.Update(new Actor() { Id = 1 });

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Update(It.IsAny<Actor>()), Times.Once);
	}

	#endregion Update
}