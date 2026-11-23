# Clean Architecture – .NET 8 Web API (דוגמה לסטודנטים)

פרויקט מינימלי שמדגים חלוקה לשכבות לפי Clean Architecture, עם שתי קריאות בלבד:

| פעולה | כתובת | תשובה |
|---|---|---|
| הוספת מוצר | `POST /api/products` | `201 Created` + המוצר החדש |
| שליפת מוצר | `GET /api/products/{id}` | `200 OK` או `404 Not Found` |

## מבנה השכבות

```
src/
├── CleanArchitectureDemo.Domain           ← ישויות (Product). לא תלוי באף אחד
├── CleanArchitectureDemo.Application      ← לוגיקה עסקית: Service, DTOs, ממשקים
├── CleanArchitectureDemo.Infrastructure   ← מימוש טכני: Repository (בזיכרון)
└── CleanArchitectureDemo.Api              ← Controller, Program.cs, Swagger
```

## כיוון התלויות (הכלל החשוב ביותר)

```
Api  ──►  Application  ──►  Domain
 │             ▲
 └──►  Infrastructure
```

- **Domain** לא מכיר אף שכבה.
- **Application** מכיר רק את Domain, ומגדיר ממשק `IProductRepository`.
- **Infrastructure** מממש את `IProductRepository` (תלוי ב-Application).
- **Api** מחבר הכול יחד דרך Dependency Injection ב-`Program.cs`.

> התלויות תמיד מצביעות **פנימה** – אל הלוגיקה העסקית.
> לכן אפשר להחליף את ה-Repository מזיכרון ל-SQL Server בלי לגעת ב-Application או ב-Api.

## זרימת בקשה

```
HTTP ► ProductsController ► IProductService (ProductService) ► IProductRepository (InMemoryProductRepository)
```

## הרצה

```bash
dotnet run --project src/CleanArchitectureDemo.Api
```

ואז לפתוח: http://localhost:5000/swagger

אפשר גם להשתמש בקובץ `src/CleanArchitectureDemo.Api/CleanArchitectureDemo.Api.http` (ב-Visual Studio / VS Code).

## דוגמה

```bash
curl -X POST http://localhost:5000/api/products -H "Content-Type: application/json" -d '{"name":"Laptop","price":3500}'
```

```bash
curl http://localhost:5000/api/products/1
```

> הנתונים נשמרים בזיכרון בלבד ונמחקים כשהשרת עוצר – בכוונה, כדי שהדוגמה תהיה פשוטה.
