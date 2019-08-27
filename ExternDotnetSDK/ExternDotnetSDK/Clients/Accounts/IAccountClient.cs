﻿using System;
using System.Threading.Tasks;
using KeApiOpenSdk.Models.Accounts;
using KeApiOpenSdk.Models.Certificates;
using KeApiOpenSdk.Models.Warrants;

namespace KeApiOpenSdk.Clients.Accounts
{
    public interface IAccountClient
    {
        Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue, TimeSpan? timeout = null);
        Task<Account> GetAccountAsync(Guid accountId, TimeSpan? timeout = null);
        Task DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null);
        Task<Account> CreateAccountAsync(string inn, string kpp, string organizationName, TimeSpan? timeout = null);

        Task<CertificateList> GetAccountCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false,
            TimeSpan? timeout = null);

        Task<WarrantList> GetAccountWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false,
            TimeSpan? timeout = null);
    }
}