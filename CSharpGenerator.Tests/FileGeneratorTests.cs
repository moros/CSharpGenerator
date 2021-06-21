using System.IO;
using CSharpGenerator.Enums;
using NUnit.Framework;

namespace CSharpGenerator.Tests
{
    [TestFixture]
    internal class FileGeneratorTests
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
        public void FileGenerator_CreatesOutputDirectory_WhenDoesNotExist()
        {
            var model = CreateInterfaceModel("MyInterface");
            var fileModel = CreateFileModel(model.Name);
            fileModel.Interfaces.Add(model);

            var generator = new FileGenerator
            {
                RootPath = Directory.GetCurrentDirectory(),
                OutputDirectory = "Outputs"
            };
            
            generator.Files.Add(fileModel);
            generator.CreateFiles();

            Assert.AreEqual(true, Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Outputs")));
        }

        [Test]
        public void FileGenerator_CreatesInnerDirectory_WhenDirectoryPrependedToFileName()
        {
            var model = CreateInterfaceModel("MyInterface");
            var fileModel = CreateFileModel(model.Name);
            fileModel.OutputDirectory = "Nested";
            fileModel.Interfaces.Add(model);

            var generator = new FileGenerator
            {
                RootPath = Directory.GetCurrentDirectory(),
                OutputDirectory = "Outputs"
            };

            generator.Files.Add(fileModel);
            generator.CreateFiles();

            Assert.AreEqual(true, Directory.Exists(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Outputs"), "Nested")));
        }

        private static InterfaceModel CreateInterfaceModel(string name)
        {
            var model = new InterfaceModel(name);
            model.Properties.Add(new Property(BuiltInDataType.Int, "Prop1"));
            model.Properties.Add(new Property(BuiltInDataType.Int, "Prop2"));
            model.Methods.Add(new Method(BuiltInDataType.Void, "Method1"));

            return model;
        }

        private static FileModel CreateFileModel(string name) => new FileModel(name)
        {
            Namespace = "CSharpGenerator.Tests"
        };
    }
}
