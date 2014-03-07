using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;

namespace DotNetDoodle.OAuthServer
{
    internal static class DbModelBuilderExtensions
    {
        internal static void RemoveSuffixFromTheTableNames(this DbModelBuilder modelBuilder, string suffix)
        {
            if (string.IsNullOrWhiteSpace(suffix))
            {
                return;
            }

            EnglishPluralizationService pluralizer = new EnglishPluralizationService();
            modelBuilder.Types().Where(type => type.Name.EndsWith(suffix)).Configure(c =>
            {
                string clrTypeName = c.ClrType.Name;
                string entityName = clrTypeName.Substring(0, clrTypeName.Length - suffix.Length);
                c.ToTable(pluralizer.Pluralize(entityName));
            });
        }
    }
}
