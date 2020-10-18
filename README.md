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
- Create/Add;
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

### Development
#### Add SQL Server connection string
- In Web app, add connection string in Web.config
- In Forms app, add to app.config

### NuGet Package Manager Console 
Before incrementally change DB by Migration, you need to enable it in NuGet Package Manager Console first:
> PM> Enable-Migrations

* A new folder "Migrations" will be created automatically in project, and the plumbing of EF has been established.

> PM> add-migration AddStatus
* So far, a Migration.cs file has been added to Migrations folder;
* Migration.cs file contains Up() and Down() methods;

> PM> update-database

#### Step by step to implement EF
1. (Once) Intall EF package;
2. (Once) Create app-specific DBContext that inherits from DBContext;
3. (Once) Add a connection string to bring DB and App;
4. (Once) Enable migration;
5. Create classes in app (code first);
6. Register in-app classes to DBContext as properties, using DbSet\<T\>;
7. Add migration to update class;
8. Update DB;

### Seed Data
1. Add an empty "Migration";
2. Update new Up() method to insert data to DB;  
>public override void Up()
 {  
    Sql("INSERT INTO Status (Name) VALUES ('To Do');");  
    Sql("INSERT INTO Status (Name) VALUES ('In Progress');");  
    Sql("INSERT INTO Status (Name) VALUES ('Done');");  
}  

### Deleting, Overwriting & Rolling-back
- Check migrations that have been ran against DB:
> PM> get-migrations

- Delete: right-click to delete from "Migrations" folder;

- Overwrite: If hasn't be ran against DB, add migration using the same migration name, marked with "-Force"
> PM> add-migration "previous migration name" -Force

- Roll-back:
1. Get the list of migrations that have been applied;
2. Find the "Target migration" point, and Update database to that point:
> PM> update-database -targetmigration "previous migration name"  

## CRUD
#### Reading data from DB
1. Create an instanc of DBContext class (get an abstraction of DB);
2. Get a "List" of Status from DB (read data into app class from DB abstraction);
3. Populate the combo box (binding to UI)

#### Creating data to DB
1. Construct new instance of specific Class;
2. Add to DBContext DataSet\<Class\> property;
3. DBContext save changes, and push to DB;

#### Data Binding from UI to DBContext
1. Using BindingSource:
DataGridView.DataSource -> BindSource.DataSource -> DBContext.DataSetProperty
- 1) Create an instance of BindingSource;
- 2) Query data from DBContext DataSet\<T\> property, using Linq sytax (working on IEnumerable);
- 3) Set qery result from DBContext to BindingSource datasource;
- 4) Set UI datasource to BindingSource;

2. Binding UI directly to DBContext:
DataGridView.DataSource -> DBContext.DataSetProperty

#### Deleting data