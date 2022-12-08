using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using Xunit;

namespace App.Test.Repositories;

public class ReservationRepositoryTest
{
    #region GetSingle

    [Fact]
    public void GetSingle_ValidId_RecordWithGivenIdReturned()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Reservation> contents = new()
        {
            new Reservation() { Id = 1, UserId = 1 },
            new Reservation() { Id = 2, UserId = 2 },
            new Reservation() { Id = 3, UserId = 3 },
        };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();
        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        Reservation? result;

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
        List<Reservation> contents = new()
        {
            new Reservation() { Id = 1, UserId = 1 },
            new Reservation() { Id = 2, UserId = 2 },
            new Reservation() { Id = 3, UserId = 3 },
        };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();
        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        Reservation? result;

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
        List<Reservation> contents = new()
        {
            new Reservation() { Id = 1, UserId = 1 },
            new Reservation() { Id = 2, UserId = 2 },
            new Reservation() { Id = 3, UserId = 3 },
        };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);
        List<Reservation>? result;

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
        List<Reservation> contents = new()
        { };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);
        List<Reservation>? result;

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
        List<Reservation> contents = new()
        { };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToAdd; i++)
        {
            repo.Add(new Reservation { Id = i });
        }

        // Assert
        dbContext.Verify(x => x.Set<Reservation, int>().Add(It.IsAny<Reservation>()), Times.Exactly(recordsToAdd));
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
        List<Reservation> contents = new()
        {
            new Reservation() { Id = 1, UserId = 1 },
            new Reservation() { Id = 2, UserId = 2 },
            new Reservation() { Id = 3, UserId = 3 },
        };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToRemove; i++)
        {
            repo.Remove(contents[0]);
        }

        // Assert
        dbContext.Verify(x => x.Set<Reservation, int>().Remove(It.IsAny<Reservation>()), Times.Exactly(recordsToRemove));
    }

    [Fact]
    public void Remove_InvalidRecord_DbSetRemoveCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Reservation> contents = new()
        {
            new Reservation() { Id = 1, UserId = 1 },
            new Reservation() { Id = 2, UserId = 2 },
            new Reservation() { Id = 3, UserId = 3 },
        };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        // Act
        repo.Remove(new Reservation { Id = 4 });

        // Assert
        dbContext.Verify(x => x.Set<Reservation, int>().Remove(It.IsAny<Reservation>()), Times.Once);
    }

    [Fact]
    public void Remove_FromEmptyTable_DbSetRemoveCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Reservation> contents = new()
        { };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        // Act
        repo.Remove(new Reservation() { Id = 1 });

        // Assert
        dbContext.Verify(x => x.Set<Reservation, int>().Remove(It.IsAny<Reservation>()), Times.Once);
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
        List<Reservation> contents = new()
        {
            new Reservation() { Id = 1, UserId = 1 },
            new Reservation() { Id = 2, UserId = 2 },
            new Reservation() { Id = 3, UserId = 3 },
        };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        // Act
        for (int i = 0; i < recordsToRemove; i++)
        {
            repo.Update(contents[0]);
        }

        // Assert
        dbContext.Verify(x => x.Set<Reservation, int>().Update(It.IsAny<Reservation>()), Times.Exactly(recordsToRemove));
    }

    [Fact]
    public void Update_InvalidRecord_DbSetUpdateCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Reservation> contents = new()
        {
            new Reservation() { Id = 1, UserId = 1 },
            new Reservation() { Id = 2, UserId = 2 },
            new Reservation() { Id = 3, UserId = 3 },
        };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        // Act
        repo.Update(new Reservation { Id = 4 });

        // Assert
        dbContext.Verify(x => x.Set<Reservation, int>().Update(It.IsAny<Reservation>()), Times.Once);
    }

    [Fact]
    public void Update_FromEmptyTable_DbSetUpdateCalledOnce()
    {
        // Arange
        Mock<IAppDbContext> dbContext = new();
        List<Reservation> contents = new()
        { };
        Mock<DbSet<Reservation>> dbSet = contents.AsQueryable().BuildMockDbSet();

        dbContext.Setup(s => s.Set<Reservation, int>()).Returns(dbSet.Object);

        ReservationRepository repo = new(dbContext.Object);

        // Act
        repo.Update(new Reservation() { Id = 1 });

        // Assert
        dbContext.Verify(x => x.Set<Reservation, int>().Update(It.IsAny<Reservation>()), Times.Once);
    }

    #endregion Update
}