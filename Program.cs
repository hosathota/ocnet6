using IBM.Data.Db2;
using System.IO;
using System.Diagnostics;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("Using DB2 .NET provider");

string path = Directory.GetCurrentDirectory();
Console.WriteLine("The current directory is {0}", path);        

Console.WriteLine("LD Library path is "+   Environment.GetEnvironmentVariable("LD_LIBRARY_PATH"));
Console.WriteLine("   Switch on the trace \n");
Console.Readline();

string connString = "Database=sample;UserID=newton;Server=Waldevdbclnxtst06.dev.rocketsoftware.com:60000;pwd=A2m8test;";
DB2Connection conn = new DB2Connection(connString);
conn.Open();
Console.WriteLine("Connection Open");
Console.WriteLine(conn.InternalOperation1());
conn.Close();
Console.WriteLine("Connection Close");
conn.Open();
Console.WriteLine("Connection Open");
conn.Close();
Console.WriteLine("Connection Close");
conn.Open();
Console.WriteLine("Connection Open");
conn.Close();
Console.WriteLine("Connection Close");
conn.Open();
Console.WriteLine("Connection Open");

DB2Command cmd = null;
int count = 0;
cmd = conn.CreateCommand();

// Create a table 'EMPBOOL1' in the SAMPLE database      
Console.WriteLine("  CREATE TABLE EMPBOOL1 WITH ATTRIBUTES:\n" +
                  "    ID SMALLINT NOT NULL,\n" +
                  "    ISMGR BOOLEAN,\n" +
                  "    NAME VARCHAR(9),\n" +
                  "    JOB CHAR(5),\n" +
                  "    PRIMARY KEY(ID)\n");

cmd.CommandText = "CREATE TABLE EMPBOOL1 (" +
                  "  ID SMALLINT NOT NULL," +
                  "  ISMGR BOOLEAN," +
                  "  NAME VARCHAR(9)," +
                  "  JOB CHAR(5)," +
                  "  PRIMARY KEY(ID))";
cmd.ExecuteNonQuery();
Console.WriteLine("\n   Table EMPBOOL1 creation Done\n");

// Insert some rows in the table 'EMPBOOL1'
Console.WriteLine(
                  "  INSERT THE FOLLOWING ROWS IN EMPBOOL1:\n" +
                  "    (500, 'TRUE', 'EMP1', 'CLERK'),\n" +
                  "    (510, 'FALSE', 'EMP2', 'MGR'),\n" +
                  "    (520, 'TRUE', 'EMP3', 'SALES'),\n" +
                  "    (530, 'FALSE', 'EMP4', 'MKT')\n");
cmd.CommandText = "INSERT INTO EMPBOOL1(id, ismgr, name, job)" +
                  "  VALUES (500, 'TRUE', 'EMP1', 'CLERK')," +
                  "         (510, 'FALSE', 'EMP2', 'MGR')," +
                  "         (520, 'TRUE', 'EMP3', 'SALES')," +
                  "         (530, 'FALSE', 'EMP4', 'MKT')";
Console.WriteLine();
cmd.ExecuteNonQuery();

Console.WriteLine("   Table EMPBOOL1 insertaion Done\n");

// Select some rows in the table 'EMPBOOL1'
Console.WriteLine("  SELECT id, ismgr, name, job \n" +
                  "    FROM  EMPBOOL1 WHERE ismgr = 1\n");

cmd.CommandText = "SELECT id, ismgr, name, job " +
                  "   FROM  EMPBOOL1 WHERE ismgr = 1";

DB2DataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    count++;
    Console.WriteLine("    -------------");
    Console.WriteLine("    Record = {0}", count.ToString());
    Console.WriteLine("    -------------");
    Console.WriteLine("    ID = {0}", reader.GetInt16(0).ToString());
    Console.WriteLine("    ISMGR = {0}", reader.GetInt16(1).ToString());
    Console.WriteLine("    NAME = {0}", reader.GetString(2));
    Console.WriteLine("    JOB = {0}", reader.GetString(3));
}
reader.Close();

// Update some rows in the table 'EMPBOOL1'
Console.WriteLine("\n   UPDTAE EMPBOOL1 SET ismgr = 1 \n" +
                  "    WHERE ID >=520\n");

cmd.CommandText = "update empbool1 set ismgr =1 " +
                  "   where ID >= 520";

Console.WriteLine();
cmd.ExecuteNonQuery();
Console.WriteLine("   Table EMPBOOL1 Updation Done\n");

// Select some rows from the table 'EMPBOOL1'
Console.WriteLine("  SELECT id, ismgr, name, job \n" +
                  "    FROM  EMPBOOL1 WHERE ismgr = 1\n");

cmd.CommandText = "SELECT id, ismgr, name, job " +
                  "   FROM  EMPBOOL1 WHERE ismgr = 1";


reader = cmd.ExecuteReader();
count = 0;
while (reader.Read())
{
    count++;
    Console.WriteLine("    -------------");
    Console.WriteLine("    Record = {0}", count.ToString());
    Console.WriteLine("    -------------");
    Console.WriteLine("    ID = {0}", reader.GetInt16(0).ToString());
    Console.WriteLine("    ISMGR = {0}", reader.GetInt16(1).ToString());
    Console.WriteLine("    NAME = {0}", reader.GetString(2));
    Console.WriteLine("    JOB = {0}", reader.GetString(3));
}
reader.Close();

// Delete some rows from the table 'EMPBOOL1'
Console.WriteLine("\n   DELETE FROM EMPBOOL1 WHERE ID >=520\n");

cmd.CommandText = "DELETE FROM EMPBOOL1 WHERE ID >=520";

Console.WriteLine();
cmd.ExecuteNonQuery();
Console.WriteLine("   Table EMPBOOL1 Deletion Done for few records\n");

// Select some rows from the table 'EMPBOOL1'
Console.WriteLine("  SELECT id, ismgr, name, job \n" +
                  "    FROM  EMPBOOL1 \n");

cmd.CommandText = "SELECT id, ismgr, name, job " +
                  "   FROM  EMPBOOL1";

reader = cmd.ExecuteReader();
count = 0;
while (reader.Read())
{
    count++;
    Console.WriteLine("    -------------");
    Console.WriteLine("    Record = {0}", count.ToString());
    Console.WriteLine("    -------------");
    Console.WriteLine("    ID = {0}", reader.GetInt16(0).ToString());
    Console.WriteLine("    ISMGR = {0}", reader.GetInt16(1).ToString());
    Console.WriteLine("    NAME = {0}", reader.GetString(2));
    Console.WriteLine("    JOB = {0}", reader.GetString(3));
}
reader.Close();


// Delete the table 'EMPBOOL1'
cmd.CommandText = "DROP TABLE EMPBOOL1\n";
cmd.ExecuteNonQuery();
Console.WriteLine("   Table EMPBOOL1 Deletetion Done\n");

// Disconnect from the database
Console.WriteLine("\n  Disconnect from the database");
conn.Close();
Console.WriteLine("   Switch off the trace \n");
Console.Readline();

