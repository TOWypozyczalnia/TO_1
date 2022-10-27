using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using App.Data;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using FakeItEasy;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

using Moq;

using Xunit;
using Xunit.Sdk;

namespace App.Test.Repositories;

public class ActorRepositoryTest
{

	#region GetSingle
	[Fact]
	public void GetSingle_Returns_1Actor()
	{
		// Arange
		Actor element = new Actor() { Id = 1 };
		List<Actor> elements = new List<Actor>() { element }; 
		Mock<DbSet<Actor>> dbSet = GetQueryableMockDbSet(elements);

		Mock<IAppDbContext> context = new Mock<IAppDbContext>();
		context.Setup(x => x.Set<Actor, int>()).Returns(dbSet.Object);

		ActorRepository repository = new ActorRepository(context.Object);

		// Act
		Actor result = repository.GetSingle(1, CancellationToken.None).Result;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(elements[0], result);
	}
	// Czy GetSingle ma zwracać pierwszy element z tabeli, czy z określonym ID?

	//[Fact]
	//public void GetSingleByID_Returns_ActorWithGivenId()
	//{
	//	// Arange
	//	Mock<IAppDbContext> dbContext = new Mock<IAppDbContext>();
	//	List<Actor> elements = new List<Actor> { new Actor { FirstName = "Name", Id = 0 }, new Actor { FirstName = "Name2", Id = 1 }, };
	//	Mock<DbSet<Actor>> dbSet = GetQueryableMockDbSet(elements);

	//	dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

	//	ActorRepository repo = new ActorRepository(dbContext.Object);
	//	Actor? result;

	//	// Arange
	//	result = repo.GetSingle(1, CancellationToken.None).Result;

	//	// Assert
	//	Assert.NotNull(result);
	//	Assert.IsAssignableFrom<Actor>(result);
	//	Assert.Equal("Name2", result.FirstName);
	//}

	//[Fact]
	//public void GetSingleByWrongID_Returns_Null()
	//{
	//	// Arange
	//	Mock<IAppDbContext> dbContext = new Mock<IAppDbContext>();
	//	List<Actor> elements = new List<Actor> { new Actor { FirstName = "Name", Id = 0 }, new Actor { FirstName = "Name2", Id = 1 }, };
	//	Mock<DbSet<Actor>> dbSet = GetQueryableMockDbSet(elements);

	//	dbContext.Setup(s => s.Set<Actor, int>()).Returns(dbSet.Object);

	//	ActorRepository repo = new ActorRepository(dbContext.Object);
	//	Actor? result;

	//	// Arange
	//	result = repo.GetSingle(6, CancellationToken.None).Result;

	//	// Assert
	//	Assert.Null(result);
	//}
	#endregion

	#region GetAll
	[Fact]
	public void GetAllWhenContainsElements_Returns_AllElements()
	{
		// Arange
		Mock<AppDbContext> dbContext = new Mock<AppDbContext>(null, new DbContextOptions<AppDbContext>());
		List<Actor> elements = new List<Actor>{ new Actor { FirstName = "Name", Id = 0 }, new Actor { FirstName = "Name2", Id = 1 }, };
		Mock<DbSet<Actor>> dbSet = GetQueryableMockDbSet(elements);

		dbContext.Setup(s => s.Set<Actor>()).Returns(dbSet.Object);

		ActorRepository repo = new ActorRepository(dbContext.Object);
		List<Actor> result;

		// Arange
		result = (List<Actor>)repo.GetAllAsync().Result;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(elements, result);
	}

	[Fact]
	public void GetAllWhenEmpty_Returns_EmptyList()
	{
		// Arange
		Mock<AppDbContext> dbContext = new Mock<AppDbContext>(null, new DbContextOptions<AppDbContext>());
		List<Actor> elements = new List<Actor>();
		Mock<DbSet<Actor>> dbSet = GetQueryableMockDbSet(elements);

		dbContext.Setup(s => s.Set<Actor>()).Returns(dbSet.Object);

		ActorRepository repo = new ActorRepository(dbContext.Object);
		List<Actor> result;

		// Arange
		result = (List<Actor>)repo.GetAllAsync().Result;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(elements, result);
	}
	#endregion

	#region Add
	// Add jest nietestowalne (nie da się zmockować DbSet.Add()

	//[Fact]
	//public void Add_Returns_AddedElement()
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

	//	dbSet.Setup(s => s.Add(It.IsAny<Actor>())).Returns(element);

	//	ActorRepository repo = new ActorRepository(dbContext.Object);
	//	Actor? result;

	//	// Arange
	//	repo.Add(element);

	//	// Assert
	//	//Assert.NotNull(result);
	//	//Assert.Equal(element, result);
	//}
	#endregion

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
	#endregion
	#region Update
	// Update jest nietestowalne (nie da się zmockować DbSet.Update()
	#endregion


	private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
	{
		var queryable = sourceList.AsQueryable();

		var dbSet = new Mock<DbSet<T>>();

		dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
		dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
		dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
		dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator()); 


		return dbSet;
	}
}
