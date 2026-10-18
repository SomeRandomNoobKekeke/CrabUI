Another new half assed library for unit testing

Key class is UTest, it's just a wrapper around expected and real value

UTestPack is a collection of UTest
UTestRunner is a class for running UTestPacks

## How to use it

run UTestCommands.AddCommands(); to add `utest` command

Create class derived from UTestPack, add UTest generating methods, Either:
- override CreateTests() and add UTests manually to UTestPack.Tests 

Or add methods with specific signatures:
- public UTest Method() should return one UTest
- public List<UTest> Method() if you want to return multiple UTests
- public void CreateBlaBlaBla() Should add tests to UTestPack.Tests manually without overriding CreateTests

You can derive one UTestPack from another, when you run test pack it also runs all derived packs

Then run utest command with class name  
utest command is tabable, without args it prints whole testpack tree

Also you can run test packs manually with UTestRunner

UTest is comparing 2 values as objects, you can also use UDictTest, UListTest, USetTest if you want to compare values as dicts, lists, sets

### for examples check Test folder on BaroJunk 
https://github.com/SomeRandomNoobKekeke/Baro-Junk/tree/main/CSharp/Shared/Test