# StockSim - Symulator Giełdy Papierów Wartościowych (Aplikacja Webowa z REST API)

## Tytuł Projektu

**StockSim**

## Autorzy

*   Jan Banasik

## Opis Projektu

StockSim to prosta aplikacja internetowa symulująca handel akcjami na wirtualnej giełdzie. Użytkownicy mogą zakładać konta, otrzymywać wirtualne saldo początkowe, przeglądać aktualne ceny i historię wybranych akcji (symbolizowanych przez firmy), a także dokonywać transakcji kupna i sprzedaży.

Aplikacja zbudowana jest w technologii ASP.NET Core i wykorzystuje wzorzec architektoniczny Model-View-Controller (MVC) dla interfejsu webowego oraz udostępnia interfejs REST API do interakcji z danymi i wykonywania operacji handlowych programowo. Dane aplikacji przechowywane są w bazie danych SQLite.

## Funkcjonalności Aplikacji

### Interfejs Webowy (MVC)

Dostępny pod adresem: http://localhost:5247

*   **Logowanie (/IO/Login):** Użytkownicy mogą logować się za pomocą nazwy użytkownika lub adresu email oraz hasła.
*   **Rejestracja (/IO/Register):** Nowi użytkownicy mogą zarejestrować konto, otrzymując początkowe saldo wirtualnych środków.
*   **Dashboard (/IO/Dashboard):** Po zalogowaniu, użytkownik widzi podsumowanie swojego konta:
    *   Aktualne saldo środków pieniężnych.
    *   Listę posiadanych akcji (Holdingów) wraz z ilością, łączną wartością inwestycji i bieżącą wartością rynkową.
    *   Wartość całego portfela (saldo + wartość posiadanych akcji).
*   **Rynek (/Market/Index):** Lista dostępnych firm notowanych na wirtualnej giełdzie wraz z ich aktualnymi cenami.
*   **Szczegóły Firmy / Wykres Historyczny (/Plot/Index):** Widok historycznych cen dla wybranej firmy (dostępny po kliknięciu w firmę na liście rynku).
*   **Transakcje Kupna/Sprzedaży (/Transaction/Buy, /Transaction/Sell):** Strony umożliwiające manualne składanie zleceń kupna lub sprzedaży akcji wybranej firmy. Logika transakcji uwzględnia saldo użytkownika i posiadane akcje.
*   **Scoreboard (/Scoreboard/Index):** Ranking użytkowników według całkowitej wartości portfela (saldo + wartość holdingów).
*   **Panel Administracyjny (/Admin/Index):** Dostępny tylko dla użytkowników z rolą administratora. Umożliwia zarządzanie użytkownikami (przeglądanie, dodawanie, edycja, usuwanie).

### REST API

Dostępne pod adresem bazowym: http://localhost:5247]

Dostęp do endpointów API wymaga uwierzytelnienia za pomocą unikalnego **API Key**, przesyłanego w nagłówku HTTP `X-Api-Key`. Każdy nowy użytkownik, któremu konto założył admin w systemie posiada swój własny API Key.

*   **Endpointy publiczne (wymagają tylko API Key, uprawnienia zwykłego użytkownika):**
    *   `GET /api/companies`: Pobiera listę wszystkich dostępnych firm.
    *   `GET /api/companies/{id}`: Pobiera szczegóły firmy o podanym ID.
    *   `GET /api/companies/history/{id}`: Pobiera historyczne ceny dla firmy o podanym ID.
    *   `GET /api/holdings`: Pobiera listę posiadanych akcji (holdingów) **dla użytkownika, którego API Key użyto do uwierzytelnienia**.
    *   `GET /api/holdings/{id}`: Pobiera szczegóły konkretnego holdingu **należącego do użytkownika, którego API Key użyto**.
    *   `GET /api/transactions`: Pobiera listę transakcji **dla użytkownika, którego API Key użyto**.
    *   `GET /api/transactions/{id}`: Pobiera szczegóły konkretnej transakcji **należącej do użytkownika, którego API Key użyto**.
    *   `POST /api/Trade/Buy`: Wykonuje transakcję kupna akcji **dla użytkownika, którego API Key użyto**. Wymaga ciała JSON z `{"ticker": "SYMBOL", "quantity": LICZBA}`.
    *   `POST /api/Trade/Sell`: Wykonuje transakcję sprzedaży akcji **dla użytkownika, którego API Key użyto**. Wymaga ciała JSON z `{"ticker": "SYMBOL", "quantity": LICZBA}`.

*   **Endpointy administracyjne (wymagają API Key użytkownika z rolą Admin):**
    *   `GET /api/users`: Pobiera listę wszystkich użytkowników w systemie.
    *   `GET /api/users/{id}`: Pobiera szczegóły użytkownika o podanym ID.
    *   `POST /api/users`: Tworzy nowego użytkownika. Wymaga ciała JSON z `{"username": "...", "email": "...", "password": "...", "currentBalance": ... (opcjonalnie)}`. Nowi użytkownicy tworzeni przez ten endpoint domyślnie nie są adminami.
    *   `PUT /api/users/{id}`: Aktualizuje dane użytkownika o podanym ID. Wymaga ciała JSON z polami do zmiany (`username`, `email`, `currentBalance` - saldo tylko przez admina).
    *   `DELETE /api/users/{id}`: Usuwa użytkownika o podanym ID. Administrator nie może usunąć swojego własnego konta.

## Sposób Użycia

### Uruchomienie Aplikacji Serwerowej

1.  Upewnij się, że masz zainstalowany .NET SDK ([https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)).
2.  Otwórz terminal lub wiersz poleceń w katalogu głównym projektu `StockSim` (gdzie znajduje się plik `.csproj` projektu webowego).
3.  Uruchom aplikację za pomocą komendy: `dotnet run`
4.  Aplikacja powinna uruchomić się i nasłuchiwać na adresie i porcie podanym w konfiguracji (domyślnie `http://localhost:5247`).

### Pierwsze Uruchomienie / Inicjalizacja Bazy Danych

Przy pierwszym uruchomieniu, aplikacja automatycznie utworzy bazę danych SQLite (jeśli nie istnieje) i zainicjuje ją, dodając przykładowe firmy, historyczne dane cen oraz **domyślnego użytkownika administratora**. Hasło administratora jest hashowane w bazie danych. API Key dla administratora jest generowany i zapisywany.

### Korzystanie z Interfejsu Webowego (MVC)

Otwórz przeglądarkę internetową i przejdź do adresu aplikacji `http://localhost:5247`. Możesz:
*   Zarejestrować nowe konto.
*   Zalogować się na swoje konto lub na konto administratora.
*   Przeglądać rynek, wykresy, swoje holdingi i transakcje.
*   Dokonywać transakcji kupna/sprzedaży poprzez dedykowane strony.
*   (Jako admin) Zarządzać użytkownikami w panelu administracyjnym (`/Admin`).

### Korzystanie z REST API

Możesz użyć dowolnego klienta HTTP (np. Postman, curl, klienta konsolowego dołączonego do projektu) do interakcji z API.

1.  **Pobierz API Key:** Zaloguj się do aplikacji webowej jako użytkownik (lub admin), a następnie odczytaj swój API Key.
    *   Dla administratora: API Key jest generowany przy pierwszym uruchomieniu. Możesz go znaleźć w bazie danych (tabela `Users`, pole `ApiKey`).
    *   Dla użytkowników tworzonych przez Admin UI: API Key jest generowany w akcji `AdminController.CreateUser` i zapisywany w bazie. Admin może go zobaczyć/skopiować w Admin UI.
    *   Dla użytkowników tworzonych przez API (POST /api/users): API Key jest generowany i zapisywany w bazie. Admin może go odczytać z bazy lub zaktualizować/wyświetlić w Admin UI.
2.  **Wykonuj żądania HTTP:** Używaj adresu bazowego API (`http://localhost:5247/api/`) i dodaj odpowiedni endpoint. W zależności od tego, jakie żądania chcesz wykonywać, używaj api-key usera lub admina w zmiennej apiKey

**Przykład użycia klienta konsolowego:**

Otwórz terminal w katalogu projektu `ApiClient` i uruchom `dotnet run`.
Zmień wartość `const string apiKey = "YOUR_API_KEY_HERE";` w pliku `Program.cs` klienta na API Key użytkownika, którego chcesz testować.
Wpisuj komendy w formacie `METHOD /path`, a dla POST/PUT/PATCH podaj ciało JSON.

## 📦 Przykładowe komendy w kliencie konsolowym

### 🔐 Jako **Admin**

#### ➕ Dodawanie użytkownika
```
POST /api/Users
{
  "username": "testowyUser",
  "email": "testtttt@example.com",
  "password": "haslotestowe",
  "currentBalance": 80000.00
}
```

#### ✏️ Aktualizacja użytkownika (ID: 18)
```
PUT /api/Users/18
{
  "username": "testowyUser2",
  "email": "testowyUser2@example.com",
  "password": "testoweHaslo2",
  "currentBalance": 2000000
}
```

#### 🔍 Pobieranie danych użytkownika (ID: 18)
```
GET /api/Users/18
```

#### 📋 Lista wszystkich użytkowników
```
GET /api/Users
```

#### 🏢 Lista wszystkich firm
```
GET /api/Companies
```

#### 📈 Historia firmy (ID: 1)
```
GET /api/Companies/history/1
```

#### ❌ Usuwanie użytkownika (ID: 18)
```
DELETE /api/Users/18
```

---

### 👤 Jako **testowy User**

#### 🛒 Kupno akcji
```
POST /api/Trade/Buy
{
  "ticker": "AAPL",
  "quantity": "5"
}
```

#### 💰 Sprzedaż akcji
```
POST /api/Trade/Sell
{
  "ticker": "AAPL",
  "quantity": "7"
}
```

#### 💰 Sprzedaż akcji (ponownie)
```
POST /api/Trade/Sell
{
  "ticker": "AAPL",
  "quantity": "3"
}
```

#### 📊 Lista wszystkich firm
```
GET /api/Companies
```
