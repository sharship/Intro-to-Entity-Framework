# Introduction to Entity Framework
0. Persistent data migration, Object Relation Mapper (ORM)

1. Code-first migration
- Compare code-first vs. database-first migration approach

2. LINQ
- Query SQL DB

3. DBContext
- An abstraction of Database in .Net application:
- DBContext: Database
- DBSet: Table

4. CRUD operation
- Add;
- Read/Get;
- Update/Edit;
- Delete;

## Entity Framework:
A layer of abstraction between application and database.

#### Database First
Start with DB, and "generate" code class from DB schema.

#### Code First
Start with code class, and then by means of "migration" to create DB schema;
- Incremental DB changes via multiple migrations;
- Can then be replicated across evironments (e.g. Dev, Qa, Prod etc);

#### LINQ vs. EF
- LINQ: Language Integrated Query, a ubiquitous way to query data from within your code (LINQ itself is NOT a persistence framework);
- LINQ to SQL: Another Persistence Framework;
- LINQ free us from data source (e.g. relational DB, XML etc) and query languages (e.g. SQL, XQuery etc);