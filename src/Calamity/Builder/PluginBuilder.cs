using Calamity.Core;
using Calamity.Logging;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Calamity.Builder
{
    public class PluginBuilder : IPluginBuilder
    {
        private readonly ILogger<PluginBuilder> _logger =
            LogProvider.Create<PluginBuilder>();

        private string _assemblyPath;

        private readonly List<object> _constructorParameters =
            new List<object>();

        public IPluginContext Build()
        {
            try
            {
                _logger.Log(LogConstants.ENTER);

                var assembly = LoadAssembly();

                return new PluginContext(
                    assembly,
                    _constructorParameters);
            }
            catch (Exception ex)
            {
                _logger.Log(
                    LogLevel.Error,
                    "",
                    ex);

                throw;
            }
            finally
            {
                _logger.Log(LogConstants.LEAVE);
            }
        }

        public IPluginBuilder DefineConstructorParameters(
            params object[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException();

            _constructorParameters.AddRange(
                parameters);

            return this;
        }

        internal PluginBuilder DefineAssembly(string assemblyPath)
        {
            _assemblyPath = assemblyPath;

            return this;
        }

        private Assembly LoadAssembly()
        {
            try
            {
                _logger.Log(LogConstants.ENTER);

                var assemblyLoadContext = new
                    PluginLoadContext(_assemblyPath);

                var assembly = assemblyLoadContext
                    .LoadFromAssemblyPath(_assemblyPath);

                _logger.Log($"Loaded assembly: '{assembly.FullName}'");

                return assembly;
            }
            finally
            {
                _logger.Log(LogConstants.LEAVE);
            }
        }
    }
}
