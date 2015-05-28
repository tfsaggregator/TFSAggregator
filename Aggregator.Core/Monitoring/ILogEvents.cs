﻿namespace Aggregator.Core
{
    using System;
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using System.Runtime.Caching;
    using System.Xml.Schema;

    /// <summary>
    /// Levels of logging.
    /// </summary>
    /// <remarks>While this enumerator is not used within Core, it is read by the configuration class <see cref="TFSAggregatorSettings"/>.</remarks>
    public enum LogLevel
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Information = 5,
        Normal = Information,
        Verbose = 10,
        Diagnostic = 99,
    }

    public interface ILogger
    {
        LogLevel Level { get; set; }
    }

    /// <summary>
    /// Core Clients will be called on this interface to log events and errors.
    /// </summary>
    /// <remarks>The methods must *not* raise exceptions.</remarks>
    public interface ILogEvents : ILogger
    {
        void ConfigurationLoaded(string policyFile);
        void StartingProcessing(IRequestContext context, INotification notification);
        void ProcessingCompleted(ProcessingResult result);
        void WorkItemWrapperTryOpenException(IWorkItem workItem, Exception e);
        void ResultsFromScriptRun(string scriptName, Collection<PSObject> results);
        void ResultsFromScriptRun(string scriptName, object result);
        void ScriptHasError(string scriptName, int line, int column, string errorCode, string errorText);
        void ScriptHasWarning(string scriptName, int line, int column, string errorCode, string errorText);
        void Saving(IWorkItem workItem, bool isValid);
        void InvalidConfiguration(XmlSeverityType severity, string message, int lineNumber, int linePosition);
        void UnreferencedRule(string ruleName);
        void ApplyingPolicy(string name);
        void ApplyingRule(string name);
        void BuildingScriptEngine(string scriptLanguage);
        void RunningRule(string name, IWorkItem workItem);
        void FailureLoadingScript(string scriptName);
        void AttemptingToMoveWorkItemToState(IWorkItem workItem, string orginalSourceState, string destState);
        void WorkItemIsValidToSave(IWorkItem workItem);
        void WorkItemIsInvalidInState(IWorkItem workItem, string destState);
        void LoadingConfiguration(string settingsPath);
        void UsingCachedConfiguration(string settingsPath);
        void ConfigurationChanged(string settingsPath, CacheEntryRemovedReason removedReason);
    }
}
