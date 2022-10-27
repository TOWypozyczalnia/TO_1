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

	[Fact]
	public void Add_ValidRecords_DbSetAddCalled()
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
		repo.Add(added);

		// Assert
		dbContext.Verify(x => x.Set<Actor, int>().Add(added), Times.Once);
	}

	[Fact]
	public void Add_MultipleValidRecords_DbSetAddCalledOnceForEachAddedRecord()
	{
		// Arange
		Mock<IAppDbContext> dbContext = new();
		List<Actor> contents = new()
		{ };
		Mock<DbSet<Actor>> dbSet = contents.AsQueryable().BuildMockDbSet();

		List<Actor> added = new()
		{
			new Actor() { Id = 1, FirstName = "Jeden" },
			new Actor() { Id = 2, FirstName = "Dwa" },
			new Actor() { Id = 3, FirstName = "Trzy" },
		};

		dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repo = new(dbContext.Object);

		// Arange
		foreach (Actor actor in added)
		{
			repo.Add(actor);
		}

		// Assert
		foreach (Actor actor in added)
		{
			dbContext.Verify(x => x.Set<Actor, int>().Add(actor), Times.Once);
		}
	}

	#endregion Add

	#region Remove
	// Remove jest nietestowalne (nie da się zmockować DbSet.Remove()

	//[Fact]
	//public void Remove_Returns_RemovedElement()
	//{
	//	// Arange
	//	Mock<IAppDbContext> dbContext = new Mock<IAppDbContext>();
	//	List<Actor> elements = new List<Actor> { new Actor { FirstName = "Name", Id = 0 }, new Actor { FirstName = "Name2", Id = 1 }, };
	//	Actor element = new Actor()
	//	{
	//		FirstName = "Added",
	//		Id = 2,
	//	};
	//	Mock<DbSet<Actor>> dbSet = GetQueryableMockDbSet(elements);

	//	dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);
	//	dbContext.Setup(s => s.SaveChanges()).Returns(0);

	//	dbSet.Setup(s => s.Add(It.IsAny<Actor>())).Returns(element); // będzie .Remove zamiast .Add

	//	ActorRepository repo = new ActorRepository(dbContext.Object);
	//	Actor? result;

	//	// Arange
	//	result = repo.Add(element);

	//	// Assert
	//	Assert.NotNull(result);
	//	Assert.Equal(element, result);
	//}
	#endregion Remove
	#region Update
	// Update jest nietestowalne (nie da się zmockować DbSet.Update()
	#endregion Update

	//	private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
	//	{
	//		var queryable = sourceList.AsQueryable();

	//		var dbSet = new Mock<DbSet<T>>();

	//		dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
	//		dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
	//		dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
	//		dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
	//		dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

	//		return dbSet;
	//	}
}