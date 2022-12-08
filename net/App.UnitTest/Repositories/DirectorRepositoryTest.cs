using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using Xunit;

namespace App.Test.Repositories;

public class DirectorRepositoryTest
{
    #region GetSingle

    [Fact]
    public void GetSingle_ValidId_RecordWithGivenIdReturned()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Director> contents = new()
        {
            new Director() { Id = 1, FirstName = "Jeden" },
            new Director() { Id = 2, FirstName = "Dwa" },
            new Director() { Id = 3, FirstName = "Trzy" },
        };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();
        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        Director? result;

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
        List<Director> contents = new()
        {
            new Director() { Id = 1, FirstName = "Jeden" },
            new Director() { Id = 2, FirstName = "Dwa" },
            new Director() { Id = 3, FirstName = "Trzy" },
        };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();
        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        Director? result;

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
        List<Director> contents = new()
        {
            new Director() { Id = 1, FirstName = "Jeden" },
            new Director() { Id = 2, FirstName = "Dwa" },
            new Director() { Id = 3, FirstName = "Trzy" },
        };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);
        List<Director>? result;

        // Act
        result = repo.GetAllAsync().ToList();

        // Assert
        Assert.Equal(contents, result);
    }

    [Fact]
    public void GetAllAsync_EmptyTable_EmptyListReturned()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Director> contents = new()
        { };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);
        List<Director>? result;

        // Act
        result = repo.GetAllAsync().ToList();

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
        List<Director> contents = new()
        { };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToAdd; i++)
        {
            repo.Add(new Director { Id = i });
        }

        // Assert
        dbContext.Verify(x => x.Set<Director, int>().Add(It.IsAny<Director>()), Times.Exactly(recordsToAdd));
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
        List<Director> contents = new()
        {
            new Director() { Id = 1, FirstName = "Jeden" },
            new Director() { Id = 2, FirstName = "Dwa" },
            new Director() { Id = 3, FirstName = "Trzy" },
        };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Director() { Id = 0 };

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToRemove; i++)
        {
            repo.Remove(contents[0]);
        }

        // Assert
        dbContext.Verify(x => x.Set<Director, int>().Remove(It.IsAny<Director>()), Times.Exactly(recordsToRemove));
    }

    [Fact]
    public void Remove_InvalidRecord_DbSetRemoveCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Director> contents = new()
        {
            new Director() { Id = 1, FirstName = "Jeden" },
            new Director() { Id = 2, FirstName = "Dwa" },
            new Director() { Id = 3, FirstName = "Trzy" },
        };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Director() { Id = 0 };

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        // Act
        repo.Remove(new Director { Id = 4 });

        // Assert
        dbContext.Verify(x => x.Set<Director, int>().Remove(It.IsAny<Director>()), Times.Once);
    }

    [Fact]
    public void Remove_FromEmptyTable_DbSetRemoveCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Director> contents = new()
        { };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        // Act
        repo.Remove(new Director() { Id = 1 });

        // Assert
        dbContext.Verify(x => x.Set<Director, int>().Remove(It.IsAny<Director>()), Times.Once);
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
        List<Director> contents = new()
        {
            new Director() { Id = 1, FirstName = "Jeden" },
            new Director() { Id = 2, FirstName = "Dwa" },
            new Director() { Id = 3, FirstName = "Trzy" },
        };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Director() { Id = 0 };

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToRemove; i++)
        {
            repo.Update(contents[0]);
        }

        // Assert
        dbContext.Verify(x => x.Set<Director, int>().Update(It.IsAny<Director>()), Times.Exactly(recordsToRemove));
    }

    [Fact]
    public void Update_InvalidRecord_DbSetUpdateCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Director> contents = new()
        {
            new Director() { Id = 1, FirstName = "Jeden" },
            new Director() { Id = 2, FirstName = "Dwa" },
            new Director() { Id = 3, FirstName = "Trzy" },
        };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        var added = new Director() { Id = 0 };

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        // Act
        repo.Update(new Director { Id = 4 });

        // Assert
        dbContext.Verify(x => x.Set<Director, int>().Update(It.IsAny<Director>()), Times.Once);
    }

    [Fact]
    public void Update_FromEmptyTable_DbSetUpdateCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Director> contents = new()
        { };
        Mock<DbSet<Director>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Director, int>()).Returns(dbSet.Object);

        DirectorRepository repo = new(dbContext.Object);

        // Act
        repo.Update(new Director() { Id = 1 });

        // Assert
        dbContext.Verify(x => x.Set<Director, int>().Update(It.IsAny<Director>()), Times.Once);
    }

    #endregion Update
}