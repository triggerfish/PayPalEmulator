using Triggerfish.NHibernate;
using System.Collections.Generic;

namespace PayPalEmulator
{
	public class SqliteSchemaExporter : ISchemaExporter
	{
		public string Name
		{
			get { return "Sqlite"; }
		}

		public IList<string> ParameterNames 
		{
			get { return new List<string> { "Path" }; }
		}

		public void GenerateScript(System.Action<string> scriptAction, IList<string> parameterValues)
		{
			Configuration config = new Configuration(new SqliteDatabase(parameterValues[0]));
			config.Create<Transaction>();
			ExportSchema.FromConfiguration(config, scriptAction);
		}
	}
}
