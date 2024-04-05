# TagManage
Projekt korzysta z autentyfikacji JWT. Aby przetestować działanie nalezy zalogować sie na konto jednego z użytkowników admin/admin lub standard/standard oraz przekleic izyskany token w swaggerze dodając przedrostek `Bearer`
Cały system pobierania tagów ze StackOverflowAPI znajduje sie w folderze ExternalApp
Obiekty w folderze Query odpowiedzialne są za operacje pobierania z bazy danych
Obiekty w folderze Command odpowiedzialne są za operacji tworzena lub edycji w bazie danych
Sortowanie metod pobierających podzielone jest oddzielnie na sortowanie po właściwościach Name i Count, z wyborem kierunku w parametrze
