using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discord
{
    public static class SubscriptionApiV2Extensions
    {
        #region SKUs
        /// <summary>
        /// Gets the SKUs for an application (Subscription API v2).
        /// </summary>
        public static async Task<IReadOnlyList<DiscordSku>> GetSkusAsync(this DiscordClient client, ulong applicationId)
            => (await client.HttpClient.GetAsync($"/applications/{applicationId}/skus")).Deserialize<List<DiscordSku>>().SetClientsInList(client);

        public static IReadOnlyList<DiscordSku> GetSkus(this DiscordClient client, ulong applicationId)
            => client.GetSkusAsync(applicationId).GetAwaiter().GetResult();

        public static async Task<DiscordSku> GetSkuAsync(this DiscordClient client, ulong skuId)
            => (await client.HttpClient.GetAsync($"/skus/{skuId}")).Deserialize<DiscordSku>().SetClient(client);

        public static DiscordSku GetSku(this DiscordClient client, ulong skuId)
            => client.GetSkuAsync(skuId).GetAwaiter().GetResult();
        #endregion

        #region Subscription plans
        /// <summary>
        /// Gets the subscription plans for a SKU (Subscription API v2).
        /// </summary>
        public static async Task<IReadOnlyList<DiscordSubscriptionPlan>> GetSubscriptionPlansAsync(this DiscordClient client, ulong skuId)
            => (await client.HttpClient.GetAsync($"/skus/{skuId}/subscription-plans")).Deserialize<List<DiscordSubscriptionPlan>>().SetClientsInList(client);

        public static IReadOnlyList<DiscordSubscriptionPlan> GetSubscriptionPlans(this DiscordClient client, ulong skuId)
            => client.GetSubscriptionPlansAsync(skuId).GetAwaiter().GetResult();

        public static async Task<DiscordSubscriptionPlan> GetSubscriptionPlanAsync(this DiscordClient client, ulong planId)
            => (await client.HttpClient.GetAsync($"/subscription-plans/{planId}")).Deserialize<DiscordSubscriptionPlan>().SetClient(client);

        public static DiscordSubscriptionPlan GetSubscriptionPlan(this DiscordClient client, ulong planId)
            => client.GetSubscriptionPlanAsync(planId).GetAwaiter().GetResult();
        #endregion

        #region Entitlements (v2)
        /// <summary>
        /// Gets entitlements for an application (Subscription API v2 shape).
        /// </summary>
        public static async Task<IReadOnlyList<DiscordEntitlementV2>> GetEntitlementsAsync(this DiscordClient client, ulong applicationId, EntitlementQuery query = null)
        {
            string qs = "";
            if (query != null)
            {
                var parts = new List<string>();
                if (query.SkuIds != null) parts.Add("sku_ids=" + query.SkuIds);
                if (query.GuildId.HasValue) parts.Add("guild_id=" + query.GuildId.Value);
                if (query.UserId.HasValue) parts.Add("user_id=" + query.UserId.Value);
                if (query.Limit.HasValue) parts.Add("limit=" + query.Limit.Value);
                if (query.ExcludedEnded.HasValue) parts.Add("excluded_ended=" + (query.ExcludedEnded.Value ? "true" : "false"));
                if (query.InProgress.HasValue) parts.Add("in_progress=" + (query.InProgress.Value ? "true" : "false"));
                if (parts.Count > 0) qs = "?" + string.Join("&", parts);
            }
            return (await client.HttpClient.GetAsync($"/applications/{applicationId}/entitlements{qs}")).Deserialize<List<DiscordEntitlementV2>>().SetClientsInList(client);
        }

        public static IReadOnlyList<DiscordEntitlementV2> GetEntitlements(this DiscordClient client, ulong applicationId, EntitlementQuery query = null)
            => client.GetEntitlementsAsync(applicationId, query).GetAwaiter().GetResult();

        /// <summary>
        /// Consumes a consumable entitlement (Subscription API v2).
        /// </summary>
        public static async Task<DiscordEntitlementV2> ConsumeEntitlementAsync(this DiscordClient client, ulong applicationId, ulong entitlementId)
            => (await client.HttpClient.PostAsync($"/applications/{applicationId}/entitlements/{entitlementId}/consume", null)).Deserialize<DiscordEntitlementV2>().SetClient(client);

        public static DiscordEntitlementV2 ConsumeEntitlement(this DiscordClient client, ulong applicationId, ulong entitlementId)
            => client.ConsumeEntitlementAsync(applicationId, entitlementId).GetAwaiter().GetResult();
        #endregion

        #region Subscriptions (v2, user account)
        /// <summary>
        /// Gets a user's subscription by id (Subscription API v2 shape).
        /// </summary>
        public static async Task<DiscordSubscriptionV2> GetUserSubscriptionAsync(this DiscordClient client, ulong subscriptionId)
            => (await client.HttpClient.GetAsync($"/users/@me/subscriptions/{subscriptionId}")).Deserialize<DiscordSubscriptionV2>().SetClient(client);

        public static DiscordSubscriptionV2 GetUserSubscription(this DiscordClient client, ulong subscriptionId)
            => client.GetUserSubscriptionAsync(subscriptionId).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the current user's subscriptions (Subscription API v2 shape).
        /// </summary>
        public static async Task<IReadOnlyList<DiscordSubscriptionV2>> GetUserSubscriptionsAsync(this DiscordClient client)
            => (await client.HttpClient.GetAsync("/users/@me/subscriptions")).Deserialize<List<DiscordSubscriptionV2>>().SetClientsInList(client);

        public static IReadOnlyList<DiscordSubscriptionV2> GetUserSubscriptions(this DiscordClient client)
            => client.GetUserSubscriptionsAsync().GetAwaiter().GetResult();
        #endregion
    }
}
