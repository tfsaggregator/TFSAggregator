﻿namespace UnitTests.Core
{
    using System;
    using System.IO;
    using System.Reflection;

    using Aggregator.Core;
    using Aggregator.Core.Configuration;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    internal static class TestHelpers
    {
        public static string LoadTextFromEmbeddedResource(string resourceName)
        {
            try
            {
                var thisAssembly = Assembly.GetAssembly(typeof(TestHelpers));
                var stream = thisAssembly.GetManifestResourceStream("UnitTests.Core.ConfigurationsForTests." + resourceName);
                var textStream = new StreamReader(stream);
                return textStream.ReadToEnd();
            }
            catch
            {
                Assert.Fail("Couldn't load embedded resource " + resourceName);
                return "";
            }
        }

        public static TFSAggregatorSettings LoadConfigFromResourceFile(string fileName, ILogEvents logger)
        {
            var configXml = LoadTextFromEmbeddedResource(fileName);
            return TFSAggregatorSettings.LoadXml(configXml, logger);
        }

        public static void LoadAndRun(this ScriptEngine engine, string scriptName, string script, IWorkItem workItem)
        {
            engine.Load(scriptName, script);
            engine.LoadCompleted();
            engine.Run(scriptName, workItem);
        }
    }
}
