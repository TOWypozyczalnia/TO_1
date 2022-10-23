[![.NET](https://github.com/bastyje/TO_1/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bastyje/TO_1/actions/workflows/dotnet.yml)
# Testowanie Oprogramowania - Projekt 1

## Uruchomienie testów

Aby uruchomić testy jednostkowe aplikacji należy w korzeniu projektu uruchomić komendę

```
dotnet test --verbosity normal net
```
Do wykonania powyższego polecenia niezbędne jest oprogramowanie [.NET SDK](https://learn.microsoft.com/en-us/dotnet/core/sdk).

## Uruchomienie aplikacji

W celu uruchomienia aplikacji należy się, w zależności od posiadanego systemu operacyjnego, zastosować do poniższych instrukcji:
- [Windows](#windows)
- [Unix (Linux, macOS)](#unix-linux-macos)

W obu przypadkach do uruchomienia aplikacji niezbędnym oprogramowaniem jest [Docker Compose](https://docs.docker.com/compose/install/). Użytkownik nie powinien się przejmować instalacją, ponieważ skrypty inicjalizujące same instalują niezbędne zależności.

### Windows
Przed pierwszym uruchomieniem aplikacji należy za pomocą powłoki PowerShell wykonać inicjalizujący skrypt

```
tools/windows/setup.ps
```

Aby uruchomić aplikację wystarczy uruchmić skrypt

```
tools/windows/start-application.ps1
```

Aby wyczyścić maszynę z artefaktów wytworzonych przez aplikację należy uruchomić skrypt

```
tools/windows/cleanup.ps1
```

### Unix (Linux, macOS)
Przed pierwszym uruchomieniem projektu należy w korzeniu projektu wykonać komendę

```
chmod +x tools/unix/setup.sh && tools/unix/setup.sh
```

Aby uruchomić aplikację wystarczy uruchmić skrypt

```
tools/unix/start-application.sh
```

Aby wyczyścić maszynę z artefaktów wytworzonych przez aplikację należy uruchomić skrypt

```
tools/unix/cleanup.sh
```


## Tematyka

API udostępnia możliwość:
- dodania filmu - dbo.Movie(Id, Name, ProductionYear, BoxOffice)
- dodania reżysera - dbo.Director(Id, FirstName, LastName, DateOfBirth)
- dodania aktora - dbo.Actor(Id, FirstName, LastName, DateOfBirth)
- dodania relacji aktora z filemem - dbo.ActorMovie(ActorId, MovieId)
- dodania relacji reżysera z fimem - dbo.DirectorMovie(DirectorId, MovieId)
- pobrania listy filmów wraz z reżyserami i aktorami
