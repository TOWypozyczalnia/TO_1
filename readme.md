[![.NET](https://github.com/bastyje/TO_1/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bastyje/TO_1/actions/workflows/dotnet.yml)
# Testowanie Oprogramowania - Projekt 1

## Tematyka

API udostępnia możliwość:
- dodania filmu - dbo.Movie(Id, Name, ProductionYear, BoxOffice)
- dodania reżysera - dbo.Director(Id, FirstName, LastName, DateOfBirth)
- dodania aktora - dbo.Actor(Id, FirstName, LastName, DateOfBirth)
- dodania relacji aktora z filemem - dbo.ActorMovie(ActorId, MovieId)
- dodania relacji reżysera z fimem - dbo.DirectorMovie(DirectorId, MovieId)
- pobrania listy filmów wraz z reżyserami i aktorami
