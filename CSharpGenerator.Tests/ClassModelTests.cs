using System.Collections.Generic;
using System.IO;
using CSharpGenerator.Enums;
using NUnit.Framework;

namespace CSharpGenerator.Tests
{
    [TestFixture]
    internal class ClassModelTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Outputs"), true);
        }

        [Test]
        public void Verify_Class_GetsCreated()
        {
            var klass = "MyClass";
            var model = new ClassModel(klass)
            {
                DefaultConstructor = { IsVisible = true },
                BaseClass = "MyBaseClass",
                AccessModifier = AccessModifier.Public
            };

            var primaryConstructor = new Constructor(klass)
            {
                BaseParameters = "logger"
            };
            primaryConstructor.Parameters.Add(new Parameter("ILogger", "logger"));
            model.Constructors.Add(primaryConstructor);

            model.Fields.Add(new Field(BuiltInDataType.Int, "field1"));
            model.Fields.Add(new Field(BuiltInDataType.Int, "field2"));
            model.Fields.Add(new Field(AccessModifier.Public, BuiltInDataType.Bool, "somePublicField"));

            model.Properties.Add(new Property(BuiltInDataType.Int, "Prop1"));
            model.Properties.Add(new Property(BuiltInDataType.Int, "Prop2"));
            model.Methods.Add(new Method(BuiltInDataType.Void, "Start")
            {
                Parameters = new List<Parameter>
                {
                    new Parameter("SomeType", "param1"),
                    new Parameter("SomeType", "param2")
                },
                BodyLines = new List<string>
                {
                    "Execute(param1, param2)"
                }
            });
            model.Methods.Add(new Method(BuiltInDataType.Void, "Stop")
            {
                BodyLines = new List<string>
                {
                    "Stop1()",
                    "Stop2()"
                }
            });

            var fileModel = CreateFileModel(model.Name);
            fileModel.Classes.Add(model);

            var generator = new FileGenerator
            {
                RootPath = Directory.GetCurrentDirectory(),
                OutputDirectory = "Outputs"
            };

            generator.Files.Add(fileModel);
            generator.CreateFiles();

            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "Outputs");
            var filePath = Path.Combine(dirPath, $"{klass}.cs");

            var expectedContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "ExpectedMyClass.txt"));
            var fileContents = File.ReadAllText(filePath);
            
            Assert.AreEqual(expectedContents, fileContents);
        }

        private static FileModel CreateFileModel(string name) => new FileModel(name)
        {
            Namespace = "CSharpGenerator.Tests"
        };
    }
}
