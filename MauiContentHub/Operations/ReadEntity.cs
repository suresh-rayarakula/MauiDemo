using Stylelabs.M.Base.Querying;
using Stylelabs.M.Base.Querying.Linq;
using Stylelabs.M.Framework.Essentials.LoadConfigurations;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Contracts.Querying;

namespace MauiContentHub.Operations
{
    public static class ReadEntity
    {
        public static async Task<IEntity> GetEntityById(long entityId)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.Id == entityId
                                                      select e);

            return await MConnector.Client().Querying.SingleAsync(query, EntityLoadConfiguration.Full);
        }

        public static async Task<IEntity> GetEntityByIdAndDefination(long entityId, string definitionName)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName
                                                      && e.Id == entityId
                                                      select e);

            return await MConnector.Client().Querying.SingleAsync(query, EntityLoadConfiguration.Full);
        }

        public static async Task<IEntityQueryResult> GetEntitiesByDefinition(string definitionName, int take = 150)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName
                                                      orderby e.CreatedOn ascending
                                                      select e);
            query.Take = take;

            return await MConnector.Client().Querying.QueryAsync(query, EntityLoadConfiguration.Full);
        }

        public static async Task<string> GetAssetURLByEntityId(long? entityId, string renditionName = "preview")
        {
            string url = string.Empty;

            if (entityId != null)
            {
                var entity = await ReadEntity.GetEntityById(entityId.Value);
                if (entity != null)
                {
                    var rendition = entity.GetRendition(renditionName);
                    if (rendition != null && rendition.Items.Count > 0)
                    {
                        url = ((rendition.Items[0].Href?.OriginalString) ?? string.Empty);
                    }
                }
            }
            return url;
        }

        public static async Task<string> GetProductFamilyNameById(long? entityId)
        {
            string productFamilyName = string.Empty;
            if (entityId != null)
            {
                var entity = await ReadEntity.GetEntityByIdAndDefination(entityId.Value, "M.PCM.ProductFamily");
                if (entity != null)
                {
                    productFamilyName = entity.GetPropertyValue<String>("ProductFamilyName");
                }
            }
            return productFamilyName;
        }

        public static async Task<IDataSource> GetDataSourceByName(string datasourceName)
        {
            return await MConnector.Client().DataSources.GetAsync(datasourceName);
        }
    }
}
