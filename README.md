# NewCleanArchitecture

גרסה מסודרת של [`rivkamos/TeachDotNet/Clean`](https://github.com/rivkamos/TeachDotNet/tree/main/Clean)
על .NET 10.

הפרויקט בנוי בכוונה **רק מהנושאים שנלמדו עד כה**. בלי DTO, בלי Entity Framework
ובלי async — שלושת אלה ייכנסו בהמשך, והקוד מסודר כך שכל אחד מהם ייכנס בנקודה אחת
ולא יפזר שינויים בכל הפרויקט.

## המבנה

ארבעת הפרויקטים יושבים ליד קובץ ה-solution, בלי תיקיית `src` ביניהם:

```
NewCleanArchitecture.slnx
Clean.Core/
Clean.Data/
Clean.Service/
Clean.Api/
```

## ארבעת הפרויקטים

```
Clean.Api  ──▶  Clean.Service  ──┐
    │                            ├──▶  Clean.Core
    └────────▶  Clean.Data   ────┘
```

| הפרויקט | התבנית ב-Visual Studio | מה יש בו |
|---|---|---|
| `Clean.Core` | Class Library | `User`, ו-interfaces: `IUserRepository`, `IUserService` |
| `Clean.Data` | Class Library | `UserRepository` — הנתונים ברשימה בזיכרון |
| `Clean.Service` | Class Library | `UserService` — הלוגיקה |
| `Clean.Api` | ASP.NET Core Web API | `UserController`, `Program.cs` |

הכלל היחיד שחשוב: **`Clean.Core` לא מכיר אף אחד.** קובץ הפרויקט שלו ריק מ-references,
וכל השאר מצביעים אליו. אם נוסיף שם אי פעם reference לפרויקט אחר — החץ התהפך.

## איפה ה-API

```
Clean.Api/Controllers/UserController.cs
```

בדיוק באותו מקום שבו הוא היה במקור. Controller רגיל שיורש מ-`ControllerBase`,
עם `[HttpGet]` ו-`[HttpPost]`.

| פעולה | הנתיב | מה מחזיר |
|---|---|---|
| `GET` | `/User` | כל המשתמשים |
| `GET` | `/User/1` | משתמש אחד, או `404` |
| `POST` | `/User` | `201` והמשתמש שנוסף |

## הרצה

```bash
dotnet run --project Clean.Api
```

ואז בדפדפן: <http://localhost:5080/swagger>

## מה שונה מהמקור

דברים שהוסרו כי הם לא עשו כלום:

- **AutoMapper** — היה מותקן ב-`Clean.Data.csproj` ואף שורת קוד לא קראה לו.
- **בלוק JWT מוער** ב-`Program.cs`, שהתייחס למשתנה `Configuration` שלא קיים.
  יחד איתו הוסר גם `"Key": "your_secret_key_here"` שהיה כתוב בתוך `appsettings.json`.
- **`MyService`** — קלאס ב-`Clean.Service` שקרא הגדרות ולא נקרא מאף מקום.
- **`[HttpGet(Name = "GetWeatherForecast")]`** — שם שנשאר מתבנית הפרויקט.
- **`app.UseAuthorization()`** — בלי authentication מוגדר הוא לא עושה כלום.

דבר אחד תוקן:

- ב-`UserRepository` המקורי ה-namespace היה `Clean.Core.Repositories`, למרות שהקובץ
  יושב ב-`Clean.Data`. כאן ה-namespace תואם לפרויקט.

## מה ייכנס בהמשך, ואיפה

**Entity Framework** — ב-`Clean.Data/Repositories/UserRepository.cs` בלבד. הרשימה
תוחלף ב-`DbContext`. אף שורה ב-`Clean.Core`, ב-`Clean.Service` או ב-`Clean.Api` לא
תשתנה בגלל זה, וזה כל הרווח מהמבנה הזה.

שימי לב לנקודה אחת שכדאי לשים לב אליה כשמגיעים לשם: במקור, ה-interface
`IDataContext` שישב ב-`Clean.Core` חשף `DbSet<User>`, ולכן `Clean.Core` נאלץ
להתקין את החבילה של EF. זה מה שמחזיר את התלות לליבה. ה-`DbContext` צריך להישאר
ב-`Clean.Data`, ו-`Clean.Core` ימשיך להכיר רק את `IUserRepository`.

**DTO** — ה-controller מחזיר כרגע את `User` עצמו, ולכן גם את `Password`. זו הנקודה
שבה DTO נכנס: קלאס תשובה נפרד שאין בו שדה סיסמה.

**async** — החתימות ב-`IUserRepository` ו-`IUserService` יהפכו ל-`Task<...>`
עם `await`. הזמן הנכון לזה הוא יחד עם EF, כי שם יש באמת המתנה ל-I/O.
