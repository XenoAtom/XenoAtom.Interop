namespace XenoAtom.Interop.Tests;

using static sqlite;

[TestClass]
public class BasicTests
{
    private const string DbFile = "test.db";
    
    [TestMethod]
    public unsafe void TestMethod1()
    {
        if (File.Exists(DbFile))
        {
            File.Delete(DbFile);
        }

        // Create sqlite3 database
        var rc = sqlite3_open16(DbFile, out var db);
        Assert.AreEqual(SQLITE_OK, rc);

        var sql = """
                  CREATE TABLE COMPANY(
                  ID INT PRIMARY KEY     NOT NULL,
                  NAME           TEXT    NOT NULL,
                  AGE            INT     NOT NULL,
                  ADDRESS        CHAR(50),
                  SALARY         REAL );
                  """;

        rc = sqlite3_exec(db, sql, null, null, out _);
        Assert.AreEqual(SQLITE_OK, rc);

        // Insert SQL statement
        sql = """
              INSERT INTO COMPANY (ID,NAME,AGE,ADDRESS,SALARY)
              VALUES (1, 'Paul', 32, 'California', 20000.00 );
              INSERT INTO COMPANY (ID,NAME,AGE,ADDRESS,SALARY)
              VALUES (2, 'Allen', 25, 'Texas', 15000.00 );
              """;
        rc = sqlite3_exec(db, sql, null, null, out _);
        Assert.AreEqual(SQLITE_OK, rc);

        // Select SQL statement
        sql = "SELECT * from COMPANY";
        var writer = new StringWriter();
        rc = sqlite3_exec(db, sql, columns =>
        {
            for (var i = 0; i < columns.Count; i++)
            {
                var column = columns.GetColumnName(i);
                var value = columns.GetColumnText(i);
                writer.WriteLine($"{column} = {value}");
            }
            return 0;
        });
        Assert.AreEqual(SQLITE_OK, rc);

        var result = writer.ToString().ReplaceLineEndings("\n");
        Assert.AreEqual("ID = 1\nNAME = Paul\nAGE = 32\nADDRESS = California\nSALARY = 20000.0\nID = 2\nNAME = Allen\nAGE = 25\nADDRESS = Texas\nSALARY = 15000.0\n", result);

        // Drop table
        sql = "DROP TABLE COMPANY";
        rc = sqlite3_exec(db, sql, null, null, out _);
        Assert.AreEqual(SQLITE_OK, rc);

        // Close database
        rc = sqlite3_close(db);
        Assert.AreEqual(SQLITE_OK, rc);
    }
}