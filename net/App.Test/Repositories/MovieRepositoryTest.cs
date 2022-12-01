using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using Xunit;

namespace App.Test.Repositories;

public class MovieRepositoryTest
{
    #region GetSingle

    [Fact]
    public void GetSingle_ValidId_RecordWithGivenIdReturned()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Movie> contents = new()
        {
            new Movie() { Id = 1, Name = "Jeden" },
            new Movie() { Id = 2, Name = "Dwa" },
            new Movie() { Id = 3, Name = "Trzy" },
        };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();
        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        Movie? result;

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
        List<Movie> contents = new()
        {
            new Movie() { Id = 1, Name = "Jeden" },
            new Movie() { Id = 2, Name = "Dwa" },
            new Movie() { Id = 3, Name = "Trzy" },
        };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();
        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        Movie? result;

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
        List<Movie> contents = new()
        {
            new Movie() { Id = 1, Name = "Jeden" },
            new Movie() { Id = 2, Name = "Dwa" },
            new Movie() { Id = 3, Name = "Trzy" },
        };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);
        List<Movie>? result;

        // Act
        result = (List<Movie>)repo.GetAllAsync().Result;

        // Assert
        Assert.Equal(contents, result);
    }

    [Fact]
    public void GetAllAsync_EmptyTable_EmptyListReturned()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Movie> contents = new()
        { };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);
        List<Movie>? result;

        // Act
        result = (List<Movie>)repo.GetAllAsync().Result;

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
        List<Movie> contents = new()
        { };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToAdd; i++)
        {
            repo.Add(new Movie { Id = i });
        }

        // Assert
        dbContext.Verify(x => x.Set<Movie, int>().Add(It.IsAny<Movie>()), Times.Exactly(recordsToAdd));
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
        List<Movie> contents = new()
        {
            new Movie() { Id = 1, Name = "Jeden" },
            new Movie() { Id = 2, Name = "Dwa" },
            new Movie() { Id = 3, Name = "Trzy" },
        };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Movie() { Id = 0 };

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToRemove; i++)
        {
            repo.Remove(contents[0]);
        }

        // Assert
        dbContext.Verify(x => x.Set<Movie, int>().Remove(It.IsAny<Movie>()), Times.Exactly(recordsToRemove));
    }

    [Fact]
    public void Remove_InvalidRecord_DbSetRemoveCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Movie> contents = new()
        {
            new Movie() { Id = 1, Name = "Jeden" },
            new Movie() { Id = 2, Name = "Dwa" },
            new Movie() { Id = 3, Name = "Trzy" },
        };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Movie() { Id = 0 };

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        // Act
        repo.Remove(new Movie { Id = 4 });

        // Assert
        dbContext.Verify(x => x.Set<Movie, int>().Remove(It.IsAny<Movie>()), Times.Once);
    }

    [Fact]
    public void Remove_FromEmptyTable_DbSetRemoveCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Movie> contents = new()
        { };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        // Act
        repo.Remove(new Movie() { Id = 1 });

        // Assert
        dbContext.Verify(x => x.Set<Movie, int>().Remove(It.IsAny<Movie>()), Times.Once);
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
        List<Movie> contents = new()
        {
            new Movie() { Id = 1, Name = "Jeden" },
            new Movie() { Id = 2, Name = "Dwa" },
            new Movie() { Id = 3, Name = "Trzy" },
        };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Movie() { Id = 0 };

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToRemove; i++)
        {
            repo.Update(contents[0]);
        }

        // Assert
        dbContext.Verify(x => x.Set<Movie, int>().Update(It.IsAny<Movie>()), Times.Exactly(recordsToRemove));
    }

    [Fact]
    public void Update_InvalidRecord_DbSetUpdateCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Movie> contents = new()
        {
            new Movie() { Id = 1, Name = "Jeden" },
            new Movie() { Id = 2, Name = "Dwa" },
            new Movie() { Id = 3, Name = "Trzy" },
        };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Movie() { Id = 0 };

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        // Act
        repo.Update(new Movie { Id = 4 });

        // Assert
        dbContext.Verify(x => x.Set<Movie, int>().Update(It.IsAny<Movie>()), Times.Once);
    }

    [Fact]
    public void Update_FromEmptyTable_DbSetUpdateCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Movie> contents = new()
        { };
        Mock<DbSet<Movie>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Movie, int>()).Returns(dbSet.Object);

        MovieRepository repo = new(dbContext.Object);

        // Act
        repo.Update(new Movie() { Id = 1 });

        // Assert
        dbContext.Verify(x => x.Set<Movie, int>().Update(It.IsAny<Movie>()), Times.Once);
    }

    #endregion Update
}